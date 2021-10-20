using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour
{
    public UnityEvent<int> onDestroyed;
    
    public int PointValue;

    void Start()
    {
        var renderer = GetComponentInChildren<SpriteRenderer>();

        switch (PointValue)
        {
            case 1 :
                renderer.color = Color.green;
                break;
            case 2:
                renderer.color = Color.yellow;
                break;
            case 5:
                renderer.color = Color.blue;
                break;
            default:
                renderer.color = Color.red;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        onDestroyed.Invoke(PointValue);
        
        //slight delay to be sure the ball have time to bounce
        Destroy(gameObject, 0.2f);
    }
}
