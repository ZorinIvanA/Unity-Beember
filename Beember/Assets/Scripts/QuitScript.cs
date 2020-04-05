using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuitScript : MonoBehaviour
{
    public UnityEvent QuitEvent;

    // Start is called before the first frame update
    void Start()
    {        
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
