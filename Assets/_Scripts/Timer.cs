using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI seconds, miliseconds;

    private int currentSecond;
    
    private float currentMilisecond;

    private void Start()
    {
        seconds = GameObject.Find("Seconds").GetComponent<TextMeshProUGUI>();
        miliseconds = GameObject.Find("MiliSeconds").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        currentMilisecond += Time.deltaTime*100;

        if (currentMilisecond >= 100)
        {
            currentSecond++;
            currentMilisecond = 0;
        }
        
        seconds.text = currentSecond + ".";
        miliseconds.text = ((int)currentMilisecond).ToString();
    }

    public void  StopTimer()
    {
        //TODO: Animation for Time to get Bigger and advance to the center of the screen
        
        this.enabled = false;
    }
}
