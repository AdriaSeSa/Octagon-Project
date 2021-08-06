using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioController : MonoBehaviour
{
    private MenuManager _menuManager;
    private int[] _lastDirectionCounter = new int[4];

    public AudioSource selectOptionSFX;
    public AudioSource backSFX;

    private void Start()
    {
        _menuManager = FindObjectOfType<MenuManager>();
    }


    private void Update()
    {
        
        for (int i = 0; i < _menuManager.directionCounter.Length; i++)
        {
            if (_menuManager.directionCounter[i] / 20 > 0 &&
                _menuManager.directionCounter[i] != _lastDirectionCounter[i] &&
                !selectOptionSFX.isPlaying)
            {
                selectOptionSFX.Play();
            }

            if (_lastDirectionCounter[i] > _menuManager.directionCounter[i])
            {
                selectOptionSFX.Stop();
            }
            _lastDirectionCounter[i] = _menuManager.directionCounter[i];
        }
    }

    public void PlayBackSFX()
    {
        if (!backSFX.isPlaying)
        {
            backSFX.Play();
        }
    }
}
