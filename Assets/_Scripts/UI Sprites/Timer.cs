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
    private bool isSecondsEnhanced;


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

        if (_currentSecond == 100)
        {
            EnhanceSeconds();
        }

        if (isSecondsEnhanced)
        {
            _seconds.text = _currentSecond.ToString();
        }
        else
        {
            _seconds.text = _currentSecond + ".";
        }
    
        _miliseconds.text = ((int)_currentMilisecond).ToString();
    }

    public void  StopTimer()
    {
        //TODO: Animation for Time to get Bigger and advance to the center of the screen
        _miliseconds.canvas.sortingLayerName = "GameLayer";
        _seconds.canvas.sortingLayerName = "GameLayer";
        this.enabled = false;
    }

    private void EnhanceSeconds()
    {
        _miliseconds.gameObject.SetActive(false);
        if (!isSecondsEnhanced)
        {
            isSecondsEnhanced = true;
            _seconds.transform.Translate(5,0,0);
        }
        
    }
}
