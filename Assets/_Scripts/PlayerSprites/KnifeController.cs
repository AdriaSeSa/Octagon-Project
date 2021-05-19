using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour
{

  private Transform _transform;
  private Vector3 direction;
  private SpriteRenderer _spriteRenderer;
    
  private bool _isBack, _isLaunched;  //  Booleans determine if knife is going back and if it's already launched
  
  [SerializeField]
  private float speed;    // Counts the speed to travel
  private float distance;  //  Counts the maximum distance to travel
  private float _distance;  //Counts the distance traveled
  
  public GameObject idleKnife;  //  Knife Sprite Following Player
  
  private void Start()
  {
    _transform = this.GetComponent<Transform>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
    
    _isLaunched = false;
    _isBack = false;
    _distance = 0;

  }

  private void Update()
  {
    if (!_isLaunched)
    {
      this.transform.position = idleKnife.transform.position;  //Knife Position is follows Sprite position
      this.transform.rotation = idleKnife.transform.rotation;
      
      return;
    }
    
    float travel = Time.deltaTime * speed;  //  Gets the travel based on speed and Time
    
    //Movement forward
    if (!_isBack)
    {
      _transform.position += direction * travel;
      _distance += travel;
      _isBack = _distance >= distance;
    }
    else //  Movement Backwards
    {
      _transform.position += direction * -travel;
      _distance -= travel;
      _isLaunched = _distance > 0;
      if (!_isLaunched) //If we have done the entire path, deactivate knife and set active Sprite
      {
        _spriteRenderer.enabled = false; 
        idleKnife.gameObject.SetActive(true);
        
      }
    }
  }

  public void LaunchKnife(float distanceF, Vector3 knifeDir)
  {
    if (_isLaunched) return;

    distance = distanceF;
    direction = knifeDir;
    _isLaunched = true;
    _distance = 0;
    _isBack = false;
  }
}
