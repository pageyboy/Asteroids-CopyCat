using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Timer bulletTimer;
    Rigidbody2D rg2d;
    float collBox;
    const int bulletSpeed = 7;

    // Start is called before the first frame update
    void Start()
    {
        rg2d = gameObject.GetComponent<Rigidbody2D>();
        float currentRotationRadians = gameObject.transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(currentRotationRadians), Mathf.Sin(currentRotationRadians));
        rg2d.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);

        bulletTimer = gameObject.AddComponent<Timer>();
        bulletTimer.Duration = 2;
        bulletTimer.Run();

        collBox = gameObject.GetComponent<BoxCollider2D>().size.x;

    }

    // Update is called once per frame
    void Update()
    {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }


}
