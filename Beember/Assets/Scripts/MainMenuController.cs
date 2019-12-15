using System.Collections;
using System.Collections.Generic;
using Beember.Beember.Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Text DiffValueText;
    public AudioSource MenuSource;
    public AudioClip MenuClip;

    public void Start()
    {
        MenuSource.clip = MenuClip;
    }

    public void Update()
    {
        if (!MenuSource.isPlaying)
            MenuSource.Play();
    }

    public void UpdateDifficult(float value)
    {
        PlayerPrefs.SetInt("difficult", (int)value);
        DiffValueText.text = ((int)value).ToString();
    }

    public void StartGame()
    {
        print($"input difficult {PlayerPrefs.GetInt("difficult")}");
        SceneManager.LoadScene("LevelScene");
    }

    public void LoadManual()
    {
        SceneManager.LoadScene("ManualScene");
    }

    public void LoadAbout()
    {
        SceneManager.LoadScene("AboutScene");
    }
}
