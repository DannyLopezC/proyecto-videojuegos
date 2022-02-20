using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_Decapricated : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

    private Vector2 _movement;

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        //getting keys values
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        //changing the face direction
        if (_movement.x < 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
        }
        else if (_movement.x > 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        }

        //applying the movement
        rb.MovePosition(rb.position + _movement * moveSpeed * Time.fixedDeltaTime);
    }
}