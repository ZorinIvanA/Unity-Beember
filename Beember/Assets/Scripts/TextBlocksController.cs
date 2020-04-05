using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Beember.Beember.Assets.Scripts
{
    public class TextBlocksController : MonoBehaviour
    {
        private Text _scoresData;
        private float positionY;
        void Start()
        {
            Canvas c = GetComponent<Canvas>();
            _scoresData = c.GetComponentInChildren<Text>();
            _scoresData.transform.position=new Vector3(163f, 330f);
        }

        // Update is called once per frame
        void Update()
        {
            //print($"Destroyed: {PlayerPrefs.GetString("DestroyedBlocks")}");
            //_scoresData.text = PlayerPrefs.GetString("DestroyedBlocks");
        }
    }
}
