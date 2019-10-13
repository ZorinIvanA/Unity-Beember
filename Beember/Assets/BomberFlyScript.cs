using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberFlyScript : MonoBehaviour
{
    private const string BOMBER_NAME = "Bomber";
    private GameObject Bomber;

    public float Velocity { get; set; }
    public int CurrentLayer { get; set; }
    
    
    // Start is called before the first frame update
    void Start()
    {
        CurrentLayer = 0;
        Velocity = 0.2f;
        Bomber = GameObject.Find(BOMBER_NAME);
    }

    // Update is called once per frame
    void Update()
    {
        //var renderer = GetComponent<Renderer>();
        //if (renderer.bounds.size.x+Bomber.transform.ri)

        //Bomber.transform.Translate(new Vector3(Velocity, 0));
        
    }
}
