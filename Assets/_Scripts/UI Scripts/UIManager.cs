using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Animator _uiAnimator;

    public TextMeshProUGUI mode, difficulty, time;

    public TextMeshProUGUI modeUI, difficultyUI;

    private void Start()
    {
        switch (PlayerPrefs.GetInt("gameMode"))
        {
            case 0:
                modeUI.text = "Standard";
                break;
            case 1:
                modeUI.text = "Rush";
                break;
        }

        switch (PlayerPrefs.GetInt("difficulty"))
        {
            case 0:
                difficultyUI.text = "Easy";
                break;
            case 1:
                difficultyUI.text = "Normal";
                break;
            case 2:
                difficultyUI.text = "Hard";
                break;
            case 3:
                difficultyUI.text = "8-Way";
                break;
        }
    }

    public void ToggleUI(bool isUIOn)
    {
        if (isUIOn)
        {
            _uiAnimator.SetTrigger("ResumeGame");
        }
        else
        {
            _uiAnimator.SetTrigger("PauseGame");
        }
    }

    public void SetGameOverUI(int _mode, int _difficulty, int _time, bool isHighScore)
    {
        Color tempColor = Color.white;
        
        switch (_mode)
        {
            case 0:
                mode.text = "Standard";
                break;
            case 1:
                mode.text = "Rush";
                break;
        }

        switch (_difficulty)
        {
            case 0:
                difficulty.text = "Easy";
                tempColor = Color.cyan;
                break;
            case 1:
                difficulty.text = "Normal";
                tempColor = Color.yellow;
                break;
            case 2:
                difficulty.text = "Hard";
                tempColor = Color.red;
                break;
            case 3: 
                difficulty.text = "8-Way";
                tempColor = Color.magenta;
                break;
        }

        time.text = _time.ToString();

        if (isHighScore)
        {
            time.color = tempColor;
        }
    }
}
