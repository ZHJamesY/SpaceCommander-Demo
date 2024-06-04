using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool isPause = false;
    GameObject pauseMenuUI;

    void Start()
    {
        pauseMenuUI= gameObject.transform.Find("PauseMenuUI").gameObject;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPause)
            {
                Time.timeScale = 0;
                pauseMenuUI.SetActive(true);
                isPause = true;
            }
            else if(isPause)
            {
                Time.timeScale = 1;
                pauseMenuUI.SetActive(false);
                isPause = false;
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        isPause = false;
    }
}
