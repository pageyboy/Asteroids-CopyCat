﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ship")
        {
            GameManager.IncreaseHealth();
            Destroy(gameObject);
        }
    }
}
