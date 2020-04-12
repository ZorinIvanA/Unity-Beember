using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class BackgroundScript : MonoBehaviour
{
    private Sprite[] backgroundSptites;

    // Start is called before the first frame update
    void Start()
    {
        backgroundSptites = new Sprite[4];
        var sprites = Resources.LoadAll<Sprite>("Backgrounds");
        var sprite = GetComponent<SpriteRenderer>();
        Random r = new Random(DateTime.Now.Millisecond);
        sprite.sprite = sprites[r.Next(0, 3)];
        sprite.drawMode = SpriteDrawMode.Sliced;
        sprite.size = new Vector2(16f, 12f);

        PlayerPrefs.SetFloat("SceneLeft", sprite.transform.position.x);
        PlayerPrefs.SetFloat("SceneTop", sprite.transform.position.y);
        Debug.Log($"Sprite position {sprite.transform.position.x}:{sprite.transform.position.y}");
        PlayerPrefs.SetFloat("SceneWidth", sprite.size.x);
        PlayerPrefs.SetFloat("SceneHeight", sprite.size.y);
        Debug.Log($"Sprite size {sprite.size.x}:{sprite.size.y}");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
