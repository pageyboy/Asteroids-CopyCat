using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to handle the bullet behaviour
/// </summary>
public class Bullet : MonoBehaviour
{
    // Expose useful fields
    Timer bulletTimer;
    Rigidbody2D rg2d;
    float collBox;
    const int bulletSpeed = 7;

    // Start is called before the first frame update
    void Start()
    {
        // Assign useful fields
        rg2d = gameObject.GetComponent<Rigidbody2D>();
        float currentRotationRadians = gameObject.transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(currentRotationRadians), Mathf.Sin(currentRotationRadians));
        rg2d.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);

        // Add a timer for the lifetime of the bullet
        bulletTimer = gameObject.AddComponent<Timer>();
        bulletTimer.Duration = 2;
        bulletTimer.Run();

        // Determine the length of the bullet for aiding the screenwrapping
        collBox = gameObject.GetComponent<BoxCollider2D>().size.x;

    }

    // Update is called once per frame
    void Update()
    {
        // If the bullet timer is finished then destroy the bullet
        if (bulletTimer.Finished)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Use OnBecameInvisible method for screen wrapping
    /// </summary>
    private void OnBecameInvisible()
    {
        ScreenWrapper.AdjustPosition(gameObject, collBox);
    }

    // Destroy the bullet on it making contact with something
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }


}
