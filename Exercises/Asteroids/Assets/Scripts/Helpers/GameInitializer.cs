using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Initializes the game
/// </summary>
public class GameInitializer : MonoBehaviour 
{
    AsteroidSpawner aSpawn;
    Timer gameTimer;

    /// <summary>
    /// Awake is called before Start
    /// </summary>
	void Awake()
    {
        // initialize screen utils
        ScreenUtils.Initialize();
        if (!GameManager.Initialized)
        {
            GameManager.Initialize();
        }
    }

    private void Start()
    {
        aSpawn = gameObject.GetComponent<AsteroidSpawner>();
        gameTimer = gameObject.AddComponent<Timer>();

    }

    private void Update()
    {
        if (GameManager.SpawnFlag == true && DateTime.Now > GameManager.SpawnTime)
        {
            GameManager.SpawnFlag = false;
            aSpawn.SpawnAsteroids();
        }
    }

}
