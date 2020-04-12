using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoresController : MonoBehaviour
{
    private const int BUILDINGS_COUNT = 13; //Количество зданий по горизонтали
    private const float BLOCK_SIZE = 1f;    //Размер блока

    // Start is called before the first frame update
    private Text _scoresData;
    void Start()
    {
        Canvas c = GetComponent<Canvas>();
        _scoresData = c.GetComponentInChildren<Text>();
        var middlePosition = new Vector2(c.pixelRect.width / 2, c.pixelRect.height / 2);
        _scoresData.transform.position = new Vector2(middlePosition.x + 80, c.pixelRect.height - 20);


        var scoresLabel = c.GetComponentsInChildren<Text>().Last();
        scoresLabel.transform.position = new Vector2(middlePosition.x - 80, c.pixelRect.height - 20);
    }

    // Update is called once per frame
    void Update()
    {
        _scoresData.text = PlayerPrefs.GetString("DestroyedBlocks");
    }
}
