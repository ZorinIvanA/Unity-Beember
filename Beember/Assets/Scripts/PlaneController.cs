using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using System;

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
    public GameObject BuildingPrefab;
    private GameObject[][] Buildings;
    public GameObject ExplosionPrefab;
    public float PlaneSpeed;

    private const int MAX_BUILDING_HEIGHT = 6;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        planeSpeed = new Vector2(PlaneSpeed, 0);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        var buildingsCount = (int)screenBounds.x * 2 - 3;
        print(buildingsCount);
        var buildingHeight = new System.Random(DateTime.Now.Millisecond);
        Buildings = new GameObject[buildingsCount][];
        for (int i = 0; i < buildingsCount; i++)
        {
            var height = buildingHeight.Next(MAX_BUILDING_HEIGHT);
            Buildings[i] = new GameObject[height];
            for (int j = 0; j < height; j++)
            {
                Vector2 position = new Vector2(i - (int)screenBounds.x + 2.5f, j - screenBounds.y + 0.5f);
                Buildings[i][j] = Instantiate(BuildingPrefab, position, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = rigidbody.position + planeSpeed;
        rigidbody.MovePosition(newPosition);

        float bombTrhow = Input.GetAxis(BOMB_AXIS_NAME);
        if (bombTrhow != 0 && !_isBombFalling)
        {
            var size = transform.lossyScale;
            _isBombFalling = true;
            Vector3 bombCoords = new Vector3(transform.position.x + size.x / 2,
                transform.position.y - size.y / 2 - 0.1f, transform.position.z);
            _bombInstantinated = Instantiate(BombPrefab, bombCoords, transform.rotation);
        }

        if (_bombInstantinated == null)
            _isBombFalling = false;
    }

    void LateUpdate()
    {
        var newPosition = transform.position;
        if (newPosition.x >= screenBounds.x + 1.5f)
        {
            newPosition.x = -7f;
            newPosition.y -= 1;
        }
        transform.position = newPosition;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(ExplosionPrefab, collision.transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(collision.gameObject);
    }
}
