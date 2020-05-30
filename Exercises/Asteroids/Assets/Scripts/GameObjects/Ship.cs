using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class handles the ships behaviour
/// </summary>

public class Ship : MonoBehaviour
{

    // Serialize fields

    [SerializeField]
    Sprite spriteShip;
    [SerializeField]
    Sprite spriteShipThrust;
    [SerializeField]
    GameObject prefabBullet;

    // Fields to reduce clutter

    private new Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;
    Vector2 thrustDirection;
    Text gameVitalsText;

    const int bulletsPerSecond = 5;
    Timer bulletTimer;

    // Constants associated with movement

    const float ThrustForce = 10;
    const float RotateDegreesPerSecond = 360;
    float collCircleRadius;
    const float maxSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure start position is 0,0 and load various components
        Vector3 startPosition = new Vector3(0, 0, 10);
        gameObject.transform.position = startPosition;
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        collCircleRadius = gameObject.GetComponent<CircleCollider2D>().radius * 2;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        bulletTimer = gameObject.AddComponent<Timer>();
        bulletTimer.Duration = 1f / bulletsPerSecond;
        bulletTimer.Run();

    }

    private void FixedUpdate()
        // Fixed update used to reduce fps load
    {
        // Handle rotation
        float rotationInput = Input.GetAxis("Rotate");

        if(rotationInput != 0)
        {
            float rotationAmount = RotateDegreesPerSecond * Time.deltaTime;
            if (rotationInput < 0)
            {
                rotationAmount *= -1;
            }
            gameObject.transform.Rotate(Vector3.forward, rotationAmount);
        }

        // Store direction the ship is now facing following rotation

        float currentRotationDegrees = gameObject.transform.eulerAngles.z;
        float currentRotationRadians = Mathf.Deg2Rad * currentRotationDegrees;
        thrustDirection = new Vector2(Mathf.Cos(currentRotationRadians), Mathf.Sin(currentRotationRadians));

        // Handle ship thrusting

        if (Input.GetAxis("Thrust") > 0)
        {
            if (rigidbody2D.velocity.magnitude > maxSpeed)
            {
                rigidbody2D.velocity = rigidbody2D.velocity.normalized * maxSpeed; 
            } else
            {
                rigidbody2D.AddForce(thrustDirection * ThrustForce, ForceMode2D.Force);
                Vector3 currentPosition = gameObject.transform.position;
                spriteRenderer.sprite = spriteShipThrust;
            }

        } else
        {
            spriteRenderer.sprite = spriteShip;
        }

        if (Input.GetAxis("Fire1") > 0)
        {
            if (bulletTimer.Finished)
            {
                Quaternion shipRotation = gameObject.transform.rotation;
                float degreeOffset = -15;
                float radiansOffset = degreeOffset * Mathf.Deg2Rad;
                float distanceFromShip = 1f;
                Vector2 firstBulletOffset = gameObject.transform.position + new Vector3(Mathf.Cos(radiansOffset + shipRotation.z) * distanceFromShip,
                    Mathf.Sin(radiansOffset + shipRotation.z) * distanceFromShip,
                    -Camera.main.transform.position.z);
                
                // Spawn first bullet
                GameObject firstBullet = Instantiate<GameObject>(prefabBullet, gameObject.transform.position, shipRotation);
                firstBullet.tag = "Bullet";
                AudioManager.Play(AudioClipName.LaserGun);

                // Spawn second bullet
                Vector3 secondBulletLocation = gameObject.transform.position;
                
                
                bulletTimer.Run();
            }
        }
    }

    /// <summary>
    /// Use OnBecameInvisible method for screen wrapping
    /// </summary>
    private void OnBecameInvisible()
    {
        ScreenWrapper.AdjustPosition(gameObject, collCircleRadius);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        rigidbody2D.angularVelocity = 0;
            GameManager.DecreaseHealth();
            if (GameManager.Health == 0)
            {
                      List<GameObject> allTargets = new List<GameObject>();
                GameObject[] targets = GameObject.FindGameObjectsWithTag("AsteroidHalf");
                allTargets.AddRange(targets);
                targets = GameObject.FindGameObjectsWithTag("AsteroidGreen");
                allTargets.AddRange(targets);
                targets = GameObject.FindGameObjectsWithTag("AsteroidMagenta");
                allTargets.AddRange(targets);
                targets = GameObject.FindGameObjectsWithTag("AsteroidWhite");
                allTargets.AddRange(targets);
                targets = GameObject.FindGameObjectsWithTag("Bullet");
                allTargets.AddRange(targets);
                foreach (GameObject gObject in allTargets)
                {
                    Destroy(gObject);
                }
                AudioManager.Play(AudioClipName.GameOver);
                Destroy(gameObject);
        }
    }

}
