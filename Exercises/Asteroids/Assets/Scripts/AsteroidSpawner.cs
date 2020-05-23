using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    [SerializeField]
    GameObject prefabAsteroid;

    const float coll = 0.32f;

    // Start is called before the first frame update
    void Start()
    {
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
