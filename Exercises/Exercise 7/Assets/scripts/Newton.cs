﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Newton (the guy or the cookie)
/// </summary>
public class Newton : MonoBehaviour
{
    const float MoveUnitsPerSecond = 20;
    int health = 0;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{

	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
		// move as appropriate
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            Vector2 position = transform.position;
            position.x += horizontalInput * MoveUnitsPerSecond *
                Time.deltaTime;
            transform.position = position;
        }
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Apple collApple = collision.gameObject.GetComponent<Apple>();
		int appleHealth = collApple.Health;
		health += appleHealth;
		print(health);
	}

}