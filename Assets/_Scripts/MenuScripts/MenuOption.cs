using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOption : MonoBehaviour
{
    
    public SpriteRenderer[] optionsRenderers = new SpriteRenderer[4]; 
    
    private MenuManager _menuManager;

    private void Start()
    {
        _menuManager = FindObjectOfType<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOptions()
    {
        switch (_menuManager._currentPanel)
        {
            case MenuManager.CurrentPanel.MAIN_MENU:
                optionsRenderers[0].gameObject.SetActive(true);
                optionsRenderers[1].gameObject.SetActive(true);
                optionsRenderers[2].gameObject.SetActive(true);
                optionsRenderers[3].gameObject.SetActive(false);
                break;
            case MenuManager.CurrentPanel.PLAY:
                optionsRenderers[0].gameObject.SetActive(true);
                optionsRenderers[1].gameObject.SetActive(true);
                optionsRenderers[2].gameObject.SetActive(false);
                optionsRenderers[3].gameObject.SetActive(false);
                break;
            case MenuManager.CurrentPanel.DIFFICULTY:
                optionsRenderers[0].gameObject.SetActive(true);
                optionsRenderers[1].gameObject.SetActive(true);
                optionsRenderers[2].gameObject.SetActive(true);
                optionsRenderers[3].gameObject.SetActive(true);
                break;
            case MenuManager.CurrentPanel.CREDITS:
                optionsRenderers[0].gameObject.SetActive(false);
                optionsRenderers[1].gameObject.SetActive(false);
                optionsRenderers[2].gameObject.SetActive(false);
                optionsRenderers[3].gameObject.SetActive(false);
                break;
            case MenuManager.CurrentPanel.OPTIONS:
                optionsRenderers[0].gameObject.SetActive(true);
                optionsRenderers[1].gameObject.SetActive(true);
                optionsRenderers[2].gameObject.SetActive(true);
                optionsRenderers[3].gameObject.SetActive(false);
                break;
        }
    }
    
}
