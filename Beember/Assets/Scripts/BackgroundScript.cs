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
        print($"Loading sprites");
        backgroundSptites = new Sprite[4];
        var sprites = Resources.LoadAll<Sprite>("Backgrounds");
        //print($"Sprites loaded {sprites.Length}");
        var sprite = GetComponent<SpriteRenderer>();
        Random r = new Random(DateTime.Now.Millisecond);
        sprite.sprite = sprites[r.Next(0, 3)];
        sprite.drawMode = SpriteDrawMode.Sliced;
        sprite.size = new Vector2(16, 12);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
