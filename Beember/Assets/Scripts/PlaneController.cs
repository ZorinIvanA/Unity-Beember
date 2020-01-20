using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Beember.Beember.Assets.Scripts;
using UnityEngine.SceneManagement;

public class PlaneController : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Vector2 planeSpeed;
    private Vector2 screenBounds;
    private bool _isBombFalling;
    private const string BOMB_AXIS_NAME = "Fire1";
    private const string BOMB_AXIS_NAME_ALT = "Jump";
    public GameObject BombPrefab;
    private GameObject _bombInstantinated;
    public GameObject BuildingPrefab;
    private GameObject[][] Buildings;
    public GameObject ExplosionPrefab;
    public float PlaneSpeed;
    public AudioSource PlaneAudioSource;
    public AudioClip PlaneClip;

    private bool isDefeated = false;
    // Start is called before the first frame update
    void Start()
    {
        var maxBuildingHeight = PlayerPrefs.GetInt("difficult");
        print($"difficult {maxBuildingHeight}");
        rigidbody = GetComponent<Rigidbody2D>();
        planeSpeed = new Vector2(PlaneSpeed, 0);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        var buildingsCount = (int)screenBounds.x * 2 - 3;
        var buildingHeight = new System.Random(DateTime.Now.Millisecond);
        Buildings = new GameObject[buildingsCount][];
        for (int i = 0; i < buildingsCount; i++)
        {
            var height = buildingHeight.Next(maxBuildingHeight) + 1;
            Buildings[i] = new GameObject[height];
            for (int j = 0; j < height; j++)
            {
                Vector2 position = new Vector2(i - (int)screenBounds.x + 2.5f, j - screenBounds.y + 0.5f);
                Buildings[i][j] = Instantiate(BuildingPrefab, position, Quaternion.identity);
            }
        }

        PlaneAudioSource.clip = PlaneClip;
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

        if (!PlaneAudioSource.isPlaying)
            PlaneAudioSource.Play();
    }

    void LateUpdate()
    {
        //print($"Buildings number is {Buildings?.Count(x => x.Any(y => y != null))}, plane coords is {transform.position.x}, {transform.position.y}");

        if (Buildings.Count(x => x.Any(y => y != null)) == 0)
        {
            Land();
            return;
        }

        var newPosition = transform.position;
        if (newPosition.x >= screenBounds.x + 1.5f)
        {
            newPosition.x = -7f;
            newPosition.y -= 1;
        }

        transform.position = newPosition;
    }

    private void Land()
    {
        print("Landing!");
        var newPosition = transform.position;
        if (newPosition.x >= screenBounds.x + 1.5f)
        {
            var landLevel = -screenBounds.y + transform.lossyScale.y / 2;
            if (newPosition.y == landLevel)
                SceneManager.LoadScene("WinScene");

            newPosition.x = -7f;
            newPosition.y = landLevel;
        }

        transform.position = newPosition;
    }

    void OnCollisionEnter2D(Collision2D collisionObject)
    {
        Instantiate(ExplosionPrefab, collisionObject.transform.position, Quaternion.identity);
        if (!isDefeated)
            StartCoroutine(LoadLevelAfterDelay(2.0f, collisionObject));
    }

    IEnumerator LoadLevelAfterDelay(float delay, Collision2D collisionObject)
    {
        isDefeated = true;
        Destroy(collisionObject.gameObject);
        Destroy(transform.gameObject);
        //gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(delay);
        print("defeat");
        SceneManager.LoadScene("DefeatScene");
    }

}
