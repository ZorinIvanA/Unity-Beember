using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    private Vector3 screenBounds;
    private bool _isAlreadyBombed;
    public AudioSource BombAudioSource;
    public AudioClip BombFall;
    public AudioClip Explosion;
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        BombAudioSource.clip = BombFall;
    }

    void Update()
    {
        if (!BombAudioSource.isPlaying)
            BombAudioSource.Play();
    }

    void LateUpdate()
    {
        var size = transform.lossyScale;
        if (transform.position.y - size.y <= -screenBounds.y)
        {
            var rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.gravityScale = 0;
            rigidBody.velocity = new Vector2(0, 0);
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isAlreadyBombed)
        {
            _isAlreadyBombed = true;
            var blastPosition = collision.transform.position;
            blastPosition.z = -1;
            var explosion = Instantiate(ExplosionPrefab, blastPosition,
                Quaternion.identity);
            print($"transform position is {blastPosition.x}:{blastPosition.y}:{blastPosition.z}");
            explosion.layer = collision.transform.gameObject.layer;
            Destroy(gameObject);
            Destroy(collision.gameObject);
            BombAudioSource.Stop();
        }
    }
}
