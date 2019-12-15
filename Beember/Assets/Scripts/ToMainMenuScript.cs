using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource MenuSource;
    public AudioClip DefeatClip;
    public AudioClip WinClip;
    public AudioClip MenuClip;
    public bool isWin;
    public bool isDefeat;
    public bool isMenu;
    void Start()
    {
        if (isWin)
            MenuSource.clip = WinClip;
        if (isDefeat)
            MenuSource.clip = DefeatClip;
        if (isMenu)
            MenuSource.clip = MenuClip;
    }

    // Update is called once per frame
    void Update()
    {
        if (!MenuSource.isPlaying)
            MenuSource.Play();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
