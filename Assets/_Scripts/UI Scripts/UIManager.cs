using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Animator _uiAnimator;
    
    
    public void ToggleUI(bool isUIOn)
    {
        if (isUIOn)
        {
            _uiAnimator.SetTrigger("ResumeGame");
        }
        else
        {
            _uiAnimator.SetTrigger("PauseGame");
        }
    }
}
