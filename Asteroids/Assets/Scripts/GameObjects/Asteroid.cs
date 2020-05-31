using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Handles the Asteroid and AsteroidHalves
/// </summary>
public class Asteroid : MonoBehaviour
{

    // Store some sprite arrays
    Sprite[] asteroidSprites = new Sprite[3];
    Sprite[] asteroidHalves = new Sprite[6];
    GameObject prefabAsteroidHalf;
    GameObject prefabLife;

    // Declare common components that require accessing
    private new Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;

    // Store some usual properties for the asteroids.
    // CollCircleRadius is used when ScreenWrapping the Asteroid
    float collCircleDiameter;
    const float maxAsteroidForce = 100;
    const float minAsteroidForce = 10;

    // Use the OnBecameInvisible Method to help screen wrap.
    private void OnBecameInvisible()
    {
        ScreenWrapper.AdjustPosition(gameObject, collCircleDiameter);
    }

    // The Asteroid Halves are not instantiated with any inbuilt directions unlike the Asteroids.
    // The Asteroid Halves are spawned where the Asteroids are destroyed
    // Therefore the Asteroid halves run the Start Method, whereas the Asteroid Halves used the Start method
    public void Start()
    {
        // Check that this Asteroid instance is an Asteroid Half
        if (gameObject.tag == "AsteroidHalf")
        {
            // Needs to be spun into a seperate method. Asteroid Movement.
            rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            collCircleDiameter = gameObject.GetComponent<CircleCollider2D>().radius * 2;
            AsteroidMovement(0, 2 * Mathf.PI, gameObject.transform.position, GameManager.AsteroidMagnitudeModifier);
            prefabLife = Resources.Load<GameObject>(@"Prefabs\Life");
        }

    }

    /// <summary>
    /// Initialize method is used for whole asteroids when being spawned from the screen sides (top, bottom, left, right)
    /// </summary>
    /// <param name="direction"></param>
    public void Initialize(Direction direction)
    {
        // Assign components
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        collCircleDiameter = gameObject.GetComponent<CircleCollider2D>().radius;
        //  Check that this Asteroid is not an Asteroid Half
        if (gameObject.tag != "AsteroidHalf")
        {
            // Load the Asteroid Sprite resources. Randomise and assign a random sprite and it's correct tag
            // to the Asteroid being created
            asteroidSprites[0] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidGreen");
            asteroidSprites[1] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidMagenta");
            asteroidSprites[2] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidWhite");
            string[] tags = new string[3] { "AsteroidGreen", "AsteroidMagenta", "AsteroidWhite" };
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            int randomSprite = Random.Range(0, 3);
            spriteRenderer.sprite = asteroidSprites[randomSprite];
            gameObject.tag = tags[randomSprite];

            // As this asteroid will spawn Asteroid halves once destroyed, these resources can also be loaded now.
            // These will not be used until this Asteroid is destroyed.
            prefabAsteroidHalf = Resources.Load<GameObject>(@"Prefabs\AsteroidHalf");
            asteroidHalves[0] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidGreen1Half");
            asteroidHalves[1] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidGreen2Half");
            asteroidHalves[2] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidMagenta1Half");
            asteroidHalves[3] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidMagenta2Half");
            asteroidHalves[4] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidWhite1Half");
            asteroidHalves[5] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidWhite2Half");

            prefabLife = Resources.Load<GameObject>(@"Prefabs\Life");
        }


        // Gets the Asteroid moving.
        float min = 0;
        float max = 0;
        float xStart = 0;
        float yStart = 0;
        float zStart = -Camera.main.transform.position.z;
        switch (direction)
        {
            case Direction.Left:
                xStart = ScreenUtils.Right + collCircleDiameter;
                yStart = 0;
                min = 165 * Mathf.Deg2Rad;
                max = 195 * Mathf.Deg2Rad;
                break;
            case Direction.Right:
                xStart = ScreenUtils.Left - collCircleDiameter;
                yStart = 0;
                min = -15 * Mathf.Deg2Rad;
                max = 15 * Mathf.Deg2Rad;
                break;
            case Direction.Up:
                xStart = 0;
                yStart = ScreenUtils.Bottom - collCircleDiameter;
                min = 75 * Mathf.Deg2Rad;
                max = 105 * Mathf.Deg2Rad;
                break;
            case Direction.Down:
                xStart = 0;
                yStart = ScreenUtils.Top + collCircleDiameter;
                min = 255 * Mathf.Deg2Rad;
                max = 285 * Mathf.Deg2Rad;
                break;
        }
        // Determine the min and maximums by using the GameManager modifiers
        min /= GameManager.AngleModifier;
        max *= GameManager.AngleModifier;
        Vector3 startLocation = new Vector3(xStart, yStart, zStart);
        AsteroidMovement(min, max, startLocation, GameManager.AsteroidMagnitudeModifier);
    }

    /// <summary>
    /// Creates the asteroid movements
    /// </summary>
    /// <param name="minAngle"></param>
    /// <param name="maxAngle"></param>
    /// <param name="startLocation"></param>
    /// <param name="modifier"></param>
    public void AsteroidMovement(float minAngle, float maxAngle, Vector3 startLocation, float modifier)
    {
        gameObject.transform.position = startLocation;
        float randomAngleRadians = Random.Range(minAngle, maxAngle);
        Vector2 newVector = new Vector2(Mathf.Cos(randomAngleRadians), Mathf.Sin(randomAngleRadians));
        float randomForce = Random.Range(minAsteroidForce, maxAsteroidForce);
        rigidbody2D.AddForce(newVector * maxAsteroidForce * modifier, ForceMode2D.Force);
    }

    /// <summary>
    /// Handles the Collisions when they occur.
    /// This Method will destroy the Asteroid when it collides with a bullet.
    /// If this object is a full Asteroid of tag "Asteroid (Green/Magenta/White)" then it will spawn the relevantly
    /// coloured sprite.
    /// If this object is a Half asteroid then it will just be destroyed.
    /// When all asteroids are destroyed then the game will move to the next level.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision that has occured is with a Bullet.
        if (collision.gameObject.tag == "Bullet")
        {
            // Check if the tag has the word 'Half' in it. If it has then this is a whole asteroid and therefore should be split
            if (gameObject.tag.IndexOf("Half") == -1)
            {
                // Instantiate the two halves
                GameObject firstAsteroidHalf = Instantiate<GameObject>(prefabAsteroidHalf, gameObject.transform.position, Quaternion.identity);
                firstAsteroidHalf.tag = "AsteroidHalf";
                GameObject secondAsteroidHalf = Instantiate<GameObject>(prefabAsteroidHalf, gameObject.transform.position, Quaternion.identity);
                secondAsteroidHalf.tag = "AsteroidHalf";
                // Depending on the colour of the tag, choose the correct coloured sprites to use.
                switch (gameObject.tag)
                {
                    case "AsteroidGreen":
                        firstAsteroidHalf.GetComponent<SpriteRenderer>().sprite = asteroidHalves[0];
                        secondAsteroidHalf.GetComponent<SpriteRenderer>().sprite = asteroidHalves[1];
                        break;
                    case "AsteroidMagenta":
                        firstAsteroidHalf.GetComponent<SpriteRenderer>().sprite = asteroidHalves[2];
                        secondAsteroidHalf.GetComponent<SpriteRenderer>().sprite = asteroidHalves[3];
                        break;
                    case "AsteroidWhite":
                        firstAsteroidHalf.GetComponent<SpriteRenderer>().sprite = asteroidHalves[4];
                        secondAsteroidHalf.GetComponent<SpriteRenderer>().sprite = asteroidHalves[5];
                        break;
                }
            }
                // Award points based on which asteroid was hit
                if (gameObject.tag == "AsteroidHalf")
                {
                    GameManager.HitHalfAsteroid();
                }
                else
                {
                    GameManager.HitAsteroid();
                }

                // Determine whether to initialize a life on the death of this Asteroid.
                // Uses the GameManager.HealthRandom which is derived from the difficulty selected
                int lifeChance = Random.Range(0, GameManager.HealthRandom);
                if (lifeChance == 1)
                {
                    GameObject life = Instantiate<GameObject>(prefabLife, gameObject.transform.position, Quaternion.identity);
                }
                // Destroy the Asteroid and play the AsteroidDeath clip
                Destroy(gameObject);
                AudioManager.Play(AudioClipName.AsteroidDeath);

        }
    }

}
