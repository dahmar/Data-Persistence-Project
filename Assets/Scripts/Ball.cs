using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        var velocity = m_Rigidbody.velocity;
        
        //after a collision we accelerate a bit
        velocity += velocity.normalized * 0.01f;
        
        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector2.Dot(velocity.normalized, Vector2.up) < 0.1f)
        {
            velocity += velocity.y > 0 ? Vector2.up * 0.5f : Vector2.down * 0.5f;
        }
        
        if (Vector2.Dot(velocity.normalized, Vector2.left) < 0.1f ||
            Vector2.Dot(velocity.normalized, Vector2.left) < 0.1f)
        {
            velocity += velocity.x > 0 ? Vector2.left * 0.5f : Vector2.right * 0.5f;
        }

        //max velocity
        if (velocity.magnitude > 3.0f)
        {
            velocity = velocity.normalized * 3.0f;
        }

        m_Rigidbody.velocity = velocity;
    }
}
