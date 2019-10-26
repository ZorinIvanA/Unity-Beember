using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    private Vector3 screenBounds;
    private bool _isAlreadyBombed;
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
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
            Instantiate(ExplosionPrefab, collision.transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
