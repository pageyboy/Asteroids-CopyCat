﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;

/// <summary>
/// This class handles the ships behaviour
/// </summary>

public class Ship : MonoBehaviour
{

    // Serialize fields

    [SerializeField]
    Sprite spriteShip;
    [SerializeField]
    Sprite spriteShipThrust;

    // Fields to reduce clutter

    private new Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;
    Vector2 thrustDirection;
    Text gameVitalsText;

    // Constants associated with movement

    const float ThrustForce = 2;
    const float RotateDegreesPerSecond = 90;
    float collCircleRadius;
    const float maxSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure start position is 0,0 and load various components
        Vector3 startPosition = new Vector3(0, 0, 10);
        gameObject.transform.position = startPosition;
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        collCircleRadius = gameObject.GetComponent<CircleCollider2D>().radius;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gameVitalsText = GameObject.Find("GameVitals").GetComponent<Text>();
    }

    private void FixedUpdate()
        // Fixed update used to reduce fps load
    {
        // Handle rotation
        float rotationInput = Input.GetAxis("Rotate");

        if(rotationInput != 0)
        {
            float rotationAmount = RotateDegreesPerSecond * Time.deltaTime;
            if (rotationInput < 0)
            {
                rotationAmount *= -1;
            }
            gameObject.transform.Rotate(Vector3.forward, rotationAmount);
        }

        // Store direction the ship is now facing following rotation

        float currentRotationDegrees = gameObject.transform.eulerAngles.z;
        float currentRotationRadians = Mathf.Deg2Rad * currentRotationDegrees;
        thrustDirection = new Vector2(Mathf.Cos(currentRotationRadians), Mathf.Sin(currentRotationRadians));

        // Handle ship thrusting

        if (Input.GetAxis("Thrust") > 0)
        {
            if (rigidbody2D.velocity.magnitude > maxSpeed)
            {
                rigidbody2D.velocity = rigidbody2D.velocity.normalized * maxSpeed; 
            } else
            {
                rigidbody2D.AddForce(thrustDirection * ThrustForce, ForceMode2D.Force);
                Vector3 currentPosition = gameObject.transform.position;
                spriteRenderer.sprite = spriteShipThrust;
            }

        } else
        {
            spriteRenderer.sprite = spriteShip;
        }

        GameVitals.UpdateVitals(rigidbody2D.velocity.x,
    rigidbody2D.velocity.y,
    currentRotationDegrees);
        gameVitalsText.text = GameVitals.VitalsString;

    }

    /// <summary>
    /// Use OnBecameInvisible method for screen wrapping
    /// </summary>
    private void OnBecameInvisible()
    {
        Vector3 currentPosition = gameObject.transform.position;
        Vector2 currentDirection = rigidbody2D.velocity;
        print(currentDirection);

        if (currentPosition.x < ScreenUtils.ScreenLeft)
        {
            currentPosition.x = ScreenUtils.ScreenRight - collCircleRadius;
        } else if (currentPosition.x > ScreenUtils.ScreenRight)
        {
            currentPosition.x = ScreenUtils.ScreenLeft + collCircleRadius;
        }
        if (currentPosition.y < ScreenUtils.ScreenBottom)
        {
            currentPosition.y = ScreenUtils.ScreenTop - collCircleRadius;
        }
        else if (currentPosition.y > ScreenUtils.ScreenTop)
        {
            currentPosition.y = ScreenUtils.ScreenBottom + collCircleRadius;
        }

        gameObject.transform.position = currentPosition;
    }

}