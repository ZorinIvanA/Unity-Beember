using UnityEngine;

public class PlaneController : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Vector2 planeSpeed;
    private Vector2 screenBounds;
    private Camera camera;
    private bool _isBombFalling;
    private const string BOMB_AXIS_NAME = "Submit";
    public GameObject BombPrefab;
    private GameObject _bombInstantinated;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        planeSpeed = new Vector2(0.05f, 0);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        print(screenBounds.x);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = rigidbody.position + planeSpeed;
        rigidbody.MovePosition(newPosition);

        float bombTrhow = Input.GetAxis(BOMB_AXIS_NAME);
        if (bombTrhow != 0 && !_isBombFalling)
        {
            _isBombFalling = true;
            _bombInstantinated = Instantiate(BombPrefab, rigidbody.position, transform.rotation);
        }
    }

    void LateUpdate()
    {
        var newPosition = transform.position;
        if (newPosition.x >= screenBounds.x * 2)
        {
            newPosition.x = 0;
            newPosition.y -= 1;
        }
        transform.position = newPosition;
    }
}
