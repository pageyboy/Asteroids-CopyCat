using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    const int maxHealth = 10;
    static DateTime nextDamage;
    static bool gameOver = false;
    static GameDifficulty gameDifficulty = GameDifficulty.Easy;
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

    #endregion

    #region Methods
    public static void Initialize(GameDifficulty gameDifficulty)
    {
        initialized = true;
        level = 1;
        asteroidPoints = 10;
        halfAsteroidPoints = 20;
        asteroidMagnitudeModifier = 1;
        halfAsteroidMagnitudeModifier = 1.25f;
        angleModifier = 1.1f;
        health = 5;
        score = 0;
        spawnFlag = true;
        spawnTime = DateTime.Now.AddSeconds(2);
        
    }

    public static void IncreaseLevel()
    {
        level++;
        if (level < 10)
        {
            asteroidMagnitudeModifier *= 1.2f;
            halfAsteroidMagnitudeModifier *= 1.2f;
        }
        angleModifier *= 1.01f;
        spawnFlag = true;
        spawnTime = DateTime.Now.AddSeconds(2);
        IncreaseHealth();
    }

    public static void IncreaseHealth()
    {
        if (health < 10)
        {
            health++;
        }
    }

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

    public static void HitAsteroid()
    {
        score += asteroidPoints;
    }

    public static void HitHalfAsteroid()
    {
        score += halfAsteroidPoints;
    }

    #endregion

}

public enum GameDifficulty
{
    Easy,
    Medium,
    Hard
}
