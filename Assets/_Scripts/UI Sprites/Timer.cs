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
    
    private float _currentMilisecond;

    private void Start()
    {
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
        
        _seconds.text = _currentSecond + ".";
        _miliseconds.text = ((int)_currentMilisecond).ToString();
    }

    public void  StopTimer()
    {
        //TODO: Animation for Time to get Bigger and advance to the center of the screen
        
        this.enabled = false;
    }
}
