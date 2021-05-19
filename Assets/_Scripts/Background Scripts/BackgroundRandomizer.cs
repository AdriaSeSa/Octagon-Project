using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class BackgroundRandomizer : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private float _colorR = 50f, _colorG = 50f, _colorB = 50f;
    private Color _tempColor;
    private readonly Color[] _lifesColor = {Color.red, Color.yellow, Color.cyan};
    private int _numOperator;
    private PlayerController _player;
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (_colorB >= 255.0f || _colorG >= 255.0f || _colorR >= 255.0f) { _numOperator = -1; }
        else if (_colorB <= 50f || _colorG <= 50f || _colorR <= 50f) {_numOperator = 1;}

        _colorR += Random.Range(0f, 0.5f) * _numOperator;
        _colorG += Random.Range(0f, 0.5f) * _numOperator;
        _colorB += Random.Range(0f, 0.5f) * _numOperator;

        _tempColor = new Color(_colorR/255.0f, _colorG/255.0f, _colorB/255.0f);

        _spriteRenderer.material.color = _tempColor * _lifesColor[_player.GETPlayerLifes()-1];
    }
}
