using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    TMP_Text scoreText;

    void Awake()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        scoreText.text = "Score: " + ScoreController.Score;
    }

}
