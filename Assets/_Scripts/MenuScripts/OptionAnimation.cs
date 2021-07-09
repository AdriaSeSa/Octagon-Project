using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionAnimation : MonoBehaviour
{

    private MenuManager _menuManager;
    private SpriteRenderer _spriteRenderer;
    public Sprite[] animationArray = new Sprite[9];
    public int optionDir;
    
    // Start is called before the first frame update
    void Start()
    {
        _menuManager = FindObjectOfType<MenuManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            UpdateOption();
        }
    }

    public void  UpdateOption()
    {
        //Gets 1 out of 9 options based on / of 20
        // Example: 15 / 20 returns sprite number 0
        // 65 / 20 returns sprite number 3...

        _spriteRenderer.sprite = animationArray[_menuManager.directionCounter[optionDir] / 20];

    }
}
