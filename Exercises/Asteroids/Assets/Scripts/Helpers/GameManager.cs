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
    #endregion

    #region Properties
    public static bool Initialized
    {
        get { return initialized; }
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

    #endregion

    #region Methods
    public static void Initialize()
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
        GameVitals.UpdateVitals(score: score, health: health, level: level);

    }

    public static void IncreaseLevel()
    {
        level++;
        asteroidPoints *= 2;
        halfAsteroidPoints *= 2;
        asteroidMagnitudeModifier *= 1.2f;
        halfAsteroidMagnitudeModifier *= 1.2f;
        angleModifier *= 1.05f;
        spawnFlag = true;
        spawnTime = DateTime.Now.AddSeconds(2);
        IncreaseHealth();
        GameVitals.UpdateVitals(score: score, health: health, level: level);
    }

    public static void IncreaseHealth()
    {
        if (health < 10)
        {
            health++;
        }
        GameVitals.UpdateVitals(score: score, health: health, level: level);
    }

    public static void DecreaseHealth()
    {
        if (health > 0)
        {
            health--;
        }
        GameVitals.UpdateVitals(score: score, health: health, level: level);
    }

    public static void HitAsteroid()
    {
        score += asteroidPoints;
        GameVitals.UpdateVitals(score: score, health: health, level: level);
    }

    public static void HitHalfAsteroid()
    {
        score += halfAsteroidPoints;
        GameVitals.UpdateVitals(score: score, health: health, level: level);
    }

    #endregion
}
