using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// Provides a game manager class.
/// This handles everything related to the game
/// </summary>
public static class GameManager
{
    #region Fields
    static bool initialized = false;
    static int level;
    static int asteroidPoints;
    static int halfAsteroidPoints;
    static float asteroidMagnitudeModifier;
    static float halfAsteroidMagnitudeModifier;
    static bool spawnFlag = false;
    static DateTime spawnTime;
    static int health;
    static int score;
    static float angleModifier;
    static int maxHealth;
    static DateTime nextDamage;
    static bool gameOver = false;
    static int healthRandom;
    static float asteroidMagnitudeModifierIncrease;
    static float angleModifierIncrease;
    static GameDifficulty currentGameDifficulty;
    static bool isGameStarted;
    #endregion

    #region Properties
    public static bool Initialized
    {
        get { return initialized; }
    }

    public static bool GameOver
    {
        get { return gameOver; }
    }

    public static int Level
    {
        get { return level; }
    }

    public static float AsteroidMagnitudeModifier
    {
        get { return asteroidMagnitudeModifier; }
    }

    public static float HalfAsteroidMagnitudeModifier
    {
        get { return halfAsteroidMagnitudeModifier; }
    }

    public static bool SpawnFlag
    {
        get { return spawnFlag; }
        set { spawnFlag = value; }
    }

    public static float AngleModifier
    {
        get { return angleModifier; }
    }

    public static DateTime SpawnTime
    {
        get { return spawnTime; }
    }

    public static int Health
    {
        get { return health; }
    }

    public static int Score
    {
        get { return score; }
    }

    public static int MaxHealth
    {
        get { return maxHealth; }
    }

    public static int HealthRandom
    {
        get { return healthRandom; }
    }

    public static GameDifficulty GameDifficulty
    {
        get { return currentGameDifficulty; }
    }

    public static bool IsGameStarted
    {
        get { return isGameStarted; }
        set { isGameStarted = value; }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Method to initialize the GameManager
    /// </summary>
    /// <param name="gameDifficulty"></param>
    public static void Initialize(GameDifficulty gameDifficulty)
    {
        initialized = true;
        ChangeDifficulty(gameDifficulty);
        asteroidPoints = 10;
        halfAsteroidPoints = 20;
        currentGameDifficulty = gameDifficulty;
    }
    /// <summary>
    /// Increase the level
    /// </summary>
    public static void IncreaseLevel()
    {
        level++;
        if (level < 10)
        {
            asteroidMagnitudeModifier *= asteroidMagnitudeModifierIncrease;
            halfAsteroidMagnitudeModifier *= asteroidMagnitudeModifierIncrease;
        }
        angleModifier *= angleModifierIncrease;
        spawnFlag = true;
        spawnTime = DateTime.Now.AddSeconds(2);
        IncreaseHealth();
    }
    /// <summary>
    /// Increase the ship's health
    /// </summary>
    public static void IncreaseHealth()
    {
        if (health < maxHealth)
        {
            health++;
        }
    }
    /// <summary>
    /// Decrease the ship's health
    /// </summary>
    public static void DecreaseHealth()
    {
        if (health > 0)
        {
            if (DateTime.Now > nextDamage)
            {
                health--;
                nextDamage = DateTime.Now.AddMilliseconds(500);
            }
        }
        if (health == 0)
        {
            gameOver = true;
        }
    }
    /// <summary>
    /// Register hitting an Asteroid and score appropriately
    /// </summary>
    public static void HitAsteroid()
    {
        score += asteroidPoints;
    }
    /// <summary>
    /// Register hitting a Half Asteroid and score appropriately
    /// </summary>
    public static void HitHalfAsteroid()
    {
        score += halfAsteroidPoints;
    }
    /// <summary>
    /// Start the Game
    /// </summary>
    public static void StartGame()
    {
        isGameStarted = true;
        spawnFlag = true;
        spawnTime = DateTime.Now.AddSeconds(2);
    }
    /// <summary>
    /// Change the difficulty if the GameManager has already been initialized
    /// </summary>
    /// <param name="gameDifficulty"></param>
    public static void ChangeDifficulty(GameDifficulty gameDifficulty)
    {
        level = 1;
        score = 0;
        asteroidMagnitudeModifier = 1;
        halfAsteroidMagnitudeModifier = 1.25f;
        angleModifier = 1.1f;
        spawnFlag = false;
        currentGameDifficulty = gameDifficulty;
        switch (gameDifficulty)
        {
            case GameDifficulty.Easy:
                healthRandom = 10;
                maxHealth = 10;
                health = 5;
                asteroidMagnitudeModifierIncrease = 1.2f;
                angleModifierIncrease = 1.01f;
                break;
            case GameDifficulty.Medium:
                healthRandom = 30;
                maxHealth = 7;
                health = 4;
                asteroidMagnitudeModifierIncrease = 1.25f;
                angleModifierIncrease = 1.03f;
                break;
            case GameDifficulty.Hard:
                healthRandom = 60;
                maxHealth = 5;
                health = 3;
                asteroidMagnitudeModifierIncrease = 1.3f;
                angleModifierIncrease = 1.10f;
                break;
            default:
                break;
        }
    }

    #endregion

}

/// <summary>
/// Enumeration for the game difficulty
/// </summary>
public enum GameDifficulty
{
    Easy,
    Medium,
    Hard
}
