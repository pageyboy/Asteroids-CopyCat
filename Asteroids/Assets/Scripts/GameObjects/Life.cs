using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to manage the behaviour of lives that are deposited when the Asteroids die
/// </summary>
public class Life : MonoBehaviour
{
    // Expose useful fields
    Timer lifeTimer;
    Color lifeColor;
    SpriteRenderer spriteRenderer;

    // These fields are used for the lifetime and alpha fade of the Asteroid
    const int lifeTime = 10;
    float lifeAlphaStep;

    /// <summary>
    /// Start is run on the gameobject being created
    /// </summary>
    private void Start()
    {
        // Set up the life timer
        lifeTimer = gameObject.AddComponent<Timer>();
        lifeTimer.Duration = lifeTime;
        lifeTimer.Run();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        // Assign colors to manage the alpha fade
        lifeColor = spriteRenderer.color;
        lifeAlphaStep = 1 / (float)lifeTime;
    }

    private void Update()
    {
        // If the life timer finishes then the object should be destroyed
        if (lifeTimer.Finished)
        {
            Destroy(gameObject);
        } else
        // Otherwise fade the alpha by the required step for the life time of the life
        {
            lifeColor.a -= Time.deltaTime * lifeAlphaStep;
            spriteRenderer.color = lifeColor;
        }

    }

    // If a collision between the ship and the life is detected then award an increase in life to the ship
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ship")
        {
            GameManager.IncreaseHealth();
            Destroy(gameObject);
        }
    }
}
