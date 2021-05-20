using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI _seconds, _miliseconds;

    private int _currentSecond;
    private int _lastSecond;
    
    private GameManager _gameManager;
    
    private float _currentMilisecond;


    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        
        _seconds = GameObject.Find("Seconds").GetComponent<TextMeshProUGUI>();
        _miliseconds = GameObject.Find("MiliSeconds").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _currentMilisecond += Time.deltaTime*100;

        if (_currentMilisecond >= 100)
        {
            _currentSecond++;
            _currentMilisecond = 0;
        }

        if (_currentSecond % 20 == 0 && _currentSecond != 0 && _currentSecond != _lastSecond)
        {
            _lastSecond = _currentSecond;
            _gameManager.ChangeEnemiesSpeed();
        }


        _seconds.text = _currentSecond + ".";
        _miliseconds.text = ((int)_currentMilisecond).ToString();
    }

    public void  StopTimer()
    {
        //TODO: Animation for Time to get Bigger and advance to the center of the screen
        
        this.enabled = false;
    }
}
