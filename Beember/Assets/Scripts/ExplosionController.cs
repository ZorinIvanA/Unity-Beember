using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public AudioSource BombAudioSource;

    public AudioClip Explosion;
    // Start is called before the first frame update
    void Start()
    {
        BombAudioSource.clip = Explosion;
        GetComponent<Renderer>().sortingLayerName = "blast";
        GetComponent<Renderer>().sortingOrder = 10;
        print($"blast layer is {GetComponent<Renderer>().sortingLayerID}");
    }

    // Update is called once per frame
    void Update()
    {
        if (!BombAudioSource.isPlaying)
            BombAudioSource.Play();
    }
}
