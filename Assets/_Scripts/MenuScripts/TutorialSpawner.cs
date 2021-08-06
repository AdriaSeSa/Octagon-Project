using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TutorialSpawner : MonoBehaviour
{
    private bool isTutorialOn = false;


    // Update is called once per frame
    void Update()
    {
        if (!isTutorialOn) return;
    }
    
    
    public void StartTutorial()
    {
        isTutorialOn = true;
    }

    public void StopTutorial()
    {
        isTutorialOn = false;
    }
}
