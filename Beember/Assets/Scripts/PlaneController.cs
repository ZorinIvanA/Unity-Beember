using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Beember.Beember.Assets.Scripts;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaneController : MonoBehaviour
{
    private const int BUILDINGS_COUNT = 13; //Количество зданий по горизонтали
    private const float SCREEN_WIDTH = 16;  //Ширина экрана в блоках (два слева и один справа отсутствуют)
    private const float SCREEN_HEIGHT = 10; //Высота экрана в блоках. 6 блоков под здания, три под самолёт, один - под служебную информацию.
    private const float BLOCK_SIZE = 1f;    //Размер блока

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
    private int totalBlocks = 0;
    private int remainsBlocks = 0;
    private Text scoresText;

    private bool isDefeated = false;
    // Start is called before the first frame update
    void Start()
    {
        var leftPosition = -BUILDINGS_COUNT * BLOCK_SIZE / 2 + 1;   //2 блока от левого края
        var maxBuildingHeight = PlayerPrefs.GetInt("difficult");
        isDefeated = false;

        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.position = new Vector2(leftPosition - 1.5f, -4 * BLOCK_SIZE);
        planeSpeed = new Vector2(PlaneSpeed, 0);
        Debug.Log($"Экран {Screen.width}:{Screen.height}");
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight));
        //screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        InitializePlane();


        var buildingHeightRandom = new System.Random(DateTime.Now.Millisecond);
        var positionFromBottom = screenBounds.y - BLOCK_SIZE / 2;
        Buildings = new GameObject[BUILDINGS_COUNT][];
        for (int i = 0; i < BUILDINGS_COUNT; i++)
        {
            var height = buildingHeightRandom.Next(maxBuildingHeight) + 1;
            Buildings[i] = new GameObject[height];
            var currentX = leftPosition + BLOCK_SIZE * i;
            for (int j = 0; j < height; j++)
            {
                //Расчитываем положение от середины экрана
                Vector2 position = new Vector2(currentX, j - positionFromBottom);
                Buildings[i][j] = Instantiate(BuildingPrefab, position, Quaternion.identity);
            }

            totalBlocks += height;
        }

        PlaneAudioSource.clip = PlaneClip;
    }

    private void InitializePlane()
    {
        transform.position = new Vector2(-BUILDINGS_COUNT * BLOCK_SIZE / 2 - BLOCK_SIZE, screenBounds.y - 1.8f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Canvas>() == null)
        {
            Vector2 newPosition = rigidbody.position + planeSpeed;
            rigidbody.MovePosition(newPosition);
        }

        float bombThrow = Input.GetAxis(BOMB_AXIS_NAME);
        if (bombThrow != 0 && !_isBombFalling)
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

        PlayerPrefs.SetString("DestroyedBlocks", $"{remainsBlocks}/{totalBlocks}");
    }

    void LateUpdate()
    {
        remainsBlocks = CountBuildings();

        if (Buildings.Count(x => x.Any(y => y != null)) == 0)
        {
            Land();
            return;
        }

        var newPosition = transform.position;
        if (newPosition.x >= BUILDINGS_COUNT * BLOCK_SIZE / 2 + BLOCK_SIZE * 0.75f)
        {
            newPosition.x = -BUILDINGS_COUNT * BLOCK_SIZE / 2 - BLOCK_SIZE;
            newPosition.y -= 1;
        }

        transform.position = newPosition;
    }

    private int CountBuildings()
    {
        int result = 0;
        foreach (var building in Buildings)
        {
            result += building.Count(x => x != null);
        }

        return result;
    }

    private void Land()
    {
        print("Landing!");
        var newPosition = transform.position;
        if (newPosition.x >= BUILDINGS_COUNT * BLOCK_SIZE / 2 + 0.5f)
        {
            var landLevel = -screenBounds.y + transform.lossyScale.y / 2;
            if (newPosition.y == landLevel)
                SceneManager.LoadScene("WinScene");

            newPosition.x = -BUILDINGS_COUNT * BLOCK_SIZE / 2 - 1.0f;
            newPosition.y = landLevel;
        }

        transform.position = newPosition;
    }

    void OnCollisionEnter2D(Collision2D collisionObject)
    {
        Instantiate(ExplosionPrefab, collisionObject.transform.position, Quaternion.identity);
        if (!isDefeated)
        {
            Destroy(collisionObject.gameObject);
            StartCoroutine(LoadLevelAfterDelay(2f));
            GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

    private IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("DefeatScene", LoadSceneMode.Single);
        Destroy(transform.gameObject);
    }
}
