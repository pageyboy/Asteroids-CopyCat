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
        ScreenUtils.Initialize();
    }

    private void Start()
    {
        aSpawn = gameObject.GetComponent<AsteroidSpawner>();
        gameTimer = gameObject.AddComponent<Timer>();
    }

    // Check whether Asteroids should be spawned
    private void Update()
    {
        if (GameManager.SpawnFlag == true && DateTime.Now > GameManager.SpawnTime && GameManager.IsGameStarted)
        {
            GameManager.SpawnFlag = false;
            aSpawn.SpawnAsteroids();
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.IsGameStarted)
        {
            // Check if all targets are destroyed. If so, raise the level.
            List<GameObject> allTargets = new List<GameObject>();
            GameObject[] targets = GameObject.FindGameObjectsWithTag("AsteroidHalf");
            allTargets.AddRange(targets);
            targets = GameObject.FindGameObjectsWithTag("AsteroidGreen");
            allTargets.AddRange(targets);
            targets = GameObject.FindGameObjectsWithTag("AsteroidMagenta");
            allTargets.AddRange(targets);
            targets = GameObject.FindGameObjectsWithTag("AsteroidWhite");
            allTargets.AddRange(targets);
            // All Targets Dead
            if (allTargets.Count == 0 && !GameManager.SpawnFlag && !GameManager.GameOver)
            {
                GameObject[] allBullets = GameObject.FindGameObjectsWithTag("Bullet");
                if (allBullets.Length > 0)
                {
                    foreach (GameObject bullet in allBullets)
                    {
                        Destroy(bullet);
                    }
                }
                AudioManager.Play(AudioClipName.LevelUp);
                GameManager.IncreaseLevel();
            }
        }
    }

}
