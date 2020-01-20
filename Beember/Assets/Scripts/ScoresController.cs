using UnityEngine;
using UnityEngine.UI;

public class ScoresController : MonoBehaviour
{
    // Start is called before the first frame update
    private Text _scoresData;
    void Start()
    {
        Canvas c = GetComponent<Canvas>();
        _scoresData = c.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        print($"Destroyed: {PlayerPrefs.GetString("DestroyedBlocks")}");
        _scoresData.text = PlayerPrefs.GetString("DestroyedBlocks");
    }
}
