using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Text DiffValueText;
    public AudioSource MenuSource;
    public AudioClip MenuClip;
    public Dropdown LanguageDropDown { get; set; }

    public void Start()
    {
        MenuSource.clip = MenuClip;
        PlayerPrefs.SetString("CurrentLanguage", "ru-RU");
    }

    public void DropdownValueChanged(Dropdown change)
    {
        switch (change.value)
        {
            case 1:
                {
                    Lean.Localization.LeanLocalization.CurrentLanguage = "English";
                }
                break;
            case 0:
                {
                    Lean.Localization.LeanLocalization.CurrentLanguage = "Russian";
                }
                break;
            case 2:
                {
                   Lean.Localization.LeanLocalization.CurrentLanguage = "Spanish";
                }
                break;
            default:
                {
                    Lean.Localization.LeanLocalization.CurrentLanguage = "Russian";
                }
                break;
        }
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
