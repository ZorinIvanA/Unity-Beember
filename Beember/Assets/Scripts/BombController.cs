using System.Collections;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    private Vector3 screenBounds;
    private bool _isAlreadyBombed;
    public AudioSource BombAudioSource;
    public AudioSource ExplosionAudioSource;
    public AudioClip BombFall;
    public AudioClip Explosion;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        BombAudioSource.clip = BombFall;
        if (!BombAudioSource.isPlaying)
            BombAudioSource.Play();
        //_explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        //var audioSource = _explosion.GetComponentInChildren<AudioSource>();
        //audioSource.clip.LoadAudioData();
    }

    void Update()
    {

    }

    void LateUpdate()
    {
        var size = transform.lossyScale;
        if (transform.position.y - size.y <= -screenBounds.y)
        {
            var rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.gravityScale = 0;
            rigidBody.velocity = new Vector2(0, 0);
            StartCoroutine(Anim(transform.position));
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isAlreadyBombed)
        {
            _isAlreadyBombed = true;

            StartCoroutine(Anim(collision.transform.position));

            GetComponent<Renderer>().enabled = false;
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    IEnumerator Anim(Vector2 position)
    {
        var explosion = Instantiate(ExplosionPrefab, position, Quaternion.identity);
        yield return null;
    }

}
