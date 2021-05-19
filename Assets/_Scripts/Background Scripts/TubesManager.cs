using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubesManager : MonoBehaviour
{
    private PlayerController _player;
    private SpriteRenderer _spriteRenderer;
    private IEnumerator _changeSprite;

    
    public Sprite[] tubeSprites = new Sprite[3];
    public Sprite standardTube;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        _spriteRenderer.sprite = standardTube;
    }


    public void ShootEnemyTube()
    {
        if (_changeSprite == null)
        {
            StartCoroutine(ChangeTubeSprite());
            _changeSprite = ChangeTubeSprite();
        }
    }

    private IEnumerator ChangeTubeSprite()
    {
        _spriteRenderer.sprite = tubeSprites[_player.GETPlayerLifes() - 1];
        
        yield return new WaitForSeconds(0.2f);
        
        _spriteRenderer.sprite = standardTube;
        _changeSprite = null;
    }
}
