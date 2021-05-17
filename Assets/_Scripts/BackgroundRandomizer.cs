using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class BackgroundRandomizer : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private float colorR = 50f, colorG = 50f, colorB = 50f;
    private Color tempColor;
    private Color stageColor = Color.cyan;
    private int numOperator;
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (colorB >= 255.0f || colorG >= 255.0f || colorR >= 255.0f) { numOperator = -1; }
        else if (colorB <= 50f || colorG <= 50f || colorR <= 50f) {numOperator = 1;}

        colorR += Random.Range(0f, 0.5f) * numOperator;
        colorG += Random.Range(0f, 0.5f) * numOperator;
        colorB += Random.Range(0f, 0.5f) * numOperator;

        tempColor = new Color(colorR/255.0f, colorG/255.0f, colorB/255.0f);

        _spriteRenderer.material.color = tempColor * stageColor;
    }
}
