using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelManagement : MonoBehaviour
{
    [SerializeField] TMP_Dropdown difficultyDropdown;
    List<int> difficultyList = new() {3, 5, 7, 9};
    int currentDifficultyIndex;
    bool gameStartStatus = false;

    public bool GetGameStartStatus
    {
        get{return gameStartStatus;}
    }

    public int GetFromDifficultyList(int difficultyIndex)
    {
        return difficultyList[difficultyIndex];
    }

    public int CurrentDifficultyIndex
    {
        get{return currentDifficultyIndex;}
        set{currentDifficultyIndex = value;}
    }

    void Awake()
    {
        currentDifficultyIndex = 0;
        ScoreController.Score = 0;
        Time.timeScale = 1;
    }

    public void ToEndGameResultScene()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void GameStartStatus_True()
    {
        gameStartStatus = true;
    }
    
}
