using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOption : MonoBehaviour
{

    public SpriteRenderer[] optionsRenderers = new SpriteRenderer[7];

    private MenuManager _menuManager;

    public IEnumerator triggerAnim;

    public Animator optionsAnimator;

    private void Start()
    {
        _menuManager = FindObjectOfType<MenuManager>();
        triggerAnim = TriggerAnim();
        ChangeOptions();
    }
    
    public void SetOptions()
    {
        if (triggerAnim != null)
        {
            StartCoroutine(triggerAnim);
        }
    }

    private IEnumerator TriggerAnim()
    {
        triggerAnim = null;
        
        optionsAnimator.SetTrigger("OptionsOut");
        yield return new WaitForSecondsRealtime(0.8f);
        ChangeOptions();
        
        triggerAnim = TriggerAnim();
    }

    private void ChangeOptions()
    {
        switch (_menuManager._currentPanel)
        {
            case MenuManager.CurrentPanel.MAIN_MENU:
                optionsRenderers[0].gameObject.SetActive(true);
                optionsRenderers[1].gameObject.SetActive(true);
                optionsRenderers[2].gameObject.SetActive(true);
                optionsRenderers[3].gameObject.SetActive(true);
                optionsRenderers[4].gameObject.SetActive(false);
                optionsRenderers[5].gameObject.SetActive(false);
                optionsRenderers[6].gameObject.SetActive(false);
                break;
            case MenuManager.CurrentPanel.PLAY:
                optionsRenderers[0].gameObject.SetActive(true);
                optionsRenderers[1].gameObject.SetActive(false);
                optionsRenderers[2].gameObject.SetActive(true);
                optionsRenderers[3].gameObject.SetActive(true);
                optionsRenderers[4].gameObject.SetActive(false);
                optionsRenderers[5].gameObject.SetActive(false);
                optionsRenderers[6].gameObject.SetActive(true);
                break;
            case MenuManager.CurrentPanel.DIFFICULTY:
                optionsRenderers[0].gameObject.SetActive(true);
                optionsRenderers[1].gameObject.SetActive(false);
                optionsRenderers[2].gameObject.SetActive(false);
                optionsRenderers[3].gameObject.SetActive(false);
                optionsRenderers[4].gameObject.SetActive(true);
                optionsRenderers[5].gameObject.SetActive(true);
                optionsRenderers[6].gameObject.SetActive(true);
                break;
            case MenuManager.CurrentPanel.CREDITS:
            case MenuManager.CurrentPanel.HOWTOPLAY:
                optionsRenderers[0].gameObject.SetActive(false);
                optionsRenderers[1].gameObject.SetActive(false);
                optionsRenderers[2].gameObject.SetActive(false);
                optionsRenderers[3].gameObject.SetActive(false);
                optionsRenderers[4].gameObject.SetActive(false);
                optionsRenderers[5].gameObject.SetActive(false);
                optionsRenderers[6].gameObject.SetActive(false);
                
                break;
            case MenuManager.CurrentPanel.OPTIONS:
                optionsRenderers[0].gameObject.SetActive(true);
                optionsRenderers[1].gameObject.SetActive(true);
                optionsRenderers[2].gameObject.SetActive(true);
                optionsRenderers[3].gameObject.SetActive(true);
                break;
        }
    }
    
}
