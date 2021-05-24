using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    private Text _text_Score;

    void Start()
    {
        _text_Score = transform.GetChild(0).GetComponent<Text>();
        if (_text_Score == null)
        {
            Debug.LogError("UI Manager could not locate Score Text.");
        }

        UpdateScore(0);
    }

    public void UpdateScore(int score)
    {
        _text_Score.text = score.ToString();
    }
}
