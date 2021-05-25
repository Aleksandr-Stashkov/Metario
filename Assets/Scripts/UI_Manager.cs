using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    private Text _txt_Score, _txt_Lives;

    void Start()
    {
        Transform child;
        for (int i = 0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i);
            switch (child.name)
            {
                case "Score Text":
                    _txt_Score = child.GetComponent<Text>();
                    break;
                case "Lives Text":
                     _txt_Lives = child.GetComponent<Text>();
                    break;
                default:
                    Debug.LogWarning("There is an unrecognized child of UI Canvas.");
                    break;
            }
        }

        
        if (_txt_Score == null)
        {
            Debug.LogError("UI Manager could not locate Score Text.");
        }       
        if (_txt_Lives == null)
        {
            Debug.LogError("UI Manager could not locate Lives Text.");
        }        
    }

    public void UpdateScore(int score)
    {
        _txt_Score.text = "Secrets: " + score.ToString();
    }

    public void UpdateLives(int lives)
    {
        _txt_Lives.text = "Lives: " + lives.ToString();
    }
}
