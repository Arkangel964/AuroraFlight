using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public bool paused;
    public GameObject pauseMenu;
    public GameObject inputField;
    public void togglePause()
    {
        if (!paused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            paused = true;
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            paused = false;
        }
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void startGame()
    {
        String nameText;
        try
        {
            nameText = inputField.GetComponent<Text>().text;
            if (nameText == null || nameText.Length == 0)
            {
                nameText = "Anonymous";
            }
            if (nameText.Length > 15)
            {
                nameText = nameText.Substring(0, 15);
            }
        }
        catch
        {
            nameText = "Anonymous";
        }
        Debug.Log("NameText = " + nameText);
        GlobalData.name = nameText;
        SceneManager.LoadScene("Level");
    }

    public void toTitleScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void toInfoScreen()
    {
        SceneManager.LoadScene("InfoScreen");
    }
}
