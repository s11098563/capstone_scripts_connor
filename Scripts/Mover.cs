﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected BoxCollider2D BoxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1.0f;

    protected virtual void Start()
    {
        BoxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        // Reset moveDelta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        // Swap sprite direction based on movement direction.
        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // Make sure we can move in this direction by casting a box there first, if box returns null free to move.
        hit = Physics2D.BoxCast(transform.position, BoxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // Movement
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, BoxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // Movement
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
