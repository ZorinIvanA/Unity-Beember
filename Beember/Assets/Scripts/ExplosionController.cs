using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public AudioSource BombAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().sortingLayerName = "blast";
        GetComponent<Renderer>().sortingOrder = 10;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
