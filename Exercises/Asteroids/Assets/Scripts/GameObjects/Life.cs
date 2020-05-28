using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    Timer lifeTimer;

    private void Start()
    {
        lifeTimer = gameObject.AddComponent<Timer>();
        lifeTimer.Duration = 5;
        lifeTimer.Run();
    }

    private void Update()
    {
        if (lifeTimer.Finished)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ship")
        {
            GameManager.IncreaseHealth();
            Destroy(gameObject);
        }
    }
}
