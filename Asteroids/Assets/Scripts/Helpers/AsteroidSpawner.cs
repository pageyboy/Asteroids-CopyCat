using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to spawn asteroids
/// </summary>
public class AsteroidSpawner : MonoBehaviour
{

    [SerializeField]
    GameObject prefabAsteroid;

    /// <summary>
    /// Spawn asteroids method
    /// </summary>
    public void SpawnAsteroids()
    {
        // Debug flag so asteroids can be turned off if need be. Normal running is true.
        bool spawnAsteroids = true;

        if (spawnAsteroids)
        {
            // Load four Asteroids and send messages to each of them dictating the directions
            GameObject asteroid = Instantiate<GameObject>(prefabAsteroid);
            asteroid.SendMessage("Initialize", Direction.Left);
            asteroid.name = "Left";
            
            asteroid = Instantiate<GameObject>(prefabAsteroid);
            asteroid.SendMessage("Initialize", Direction.Right);
            asteroid.name = "Right";

            asteroid = Instantiate<GameObject>(prefabAsteroid);
            asteroid.SendMessage("Initialize", Direction.Up);
            asteroid.name = "Up";

            asteroid = Instantiate<GameObject>(prefabAsteroid);
            asteroid.SendMessage("Initialize", Direction.Down);
            asteroid.name = "Down";
            
        }
        }

}
