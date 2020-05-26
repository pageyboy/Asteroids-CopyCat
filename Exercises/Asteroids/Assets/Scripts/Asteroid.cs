using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Sprite[] asteroidSprites = new Sprite[3];
    Sprite[] asteroidHalves = new Sprite[6];
    GameObject prefabAsteroidHalf;

    private new Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;

    float collCircleRadius;
    const float maxAsteroidForce = 100;

    private void OnBecameInvisible()
    {
        ScreenWrapper.AdjustPosition(gameObject, collCircleRadius);
    }

    public void Start()
    {
        if (gameObject.tag == "AsteroidHalf")
        {
            rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            collCircleRadius = gameObject.GetComponent<CircleCollider2D>().radius;
            float randomAngleRadians = Random.Range(0, Mathf.PI * 2);
            Vector2 newVector = new Vector2(Mathf.Cos(randomAngleRadians), Mathf.Sin(randomAngleRadians));
            float randomForce = Random.Range(9, maxAsteroidForce);
            rigidbody2D.AddForce(newVector * maxAsteroidForce, ForceMode2D.Force);
        }

    }

    public void Initialize(Direction direction)
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        collCircleRadius = gameObject.GetComponent<CircleCollider2D>().radius;
        if (gameObject.tag != "AsteroidHalf")
        {
            asteroidSprites[0] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidGreen");
            asteroidSprites[1] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidMagenta");
            asteroidSprites[2] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidWhite");
            string[] tags = new string[3] { "AsteroidGreen", "AsteroidMagenta", "AsteroidWhite" };
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            int randomSprite = Random.Range(0, 3);
            spriteRenderer.sprite = asteroidSprites[randomSprite];
            gameObject.tag = tags[randomSprite];
            prefabAsteroidHalf = Resources.Load<GameObject>(@"Prefabs\AsteroidHalf");
            asteroidHalves[0] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidGreen1Half");
            asteroidHalves[1] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidGreen2Half");
            asteroidHalves[2] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidMagenta1Half");
            asteroidHalves[3] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidMagenta2Half");
            asteroidHalves[4] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidWhite1Half");
            asteroidHalves[5] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidWhite2Half");
        }

        float randomAngleRadians;
        float min = 0;
        float max = 0;
        float xStart = 0;
        float yStart = 0;
        float zStart = -Camera.main.transform.position.z;
        switch (direction)
        {
            case Direction.Left:
                xStart = ScreenUtils.Right + collCircleRadius;
                yStart = 0;
                min = 165 * Mathf.Deg2Rad;
                max = 195 * Mathf.Deg2Rad;
                break;
            case Direction.Right:
                xStart = ScreenUtils.Left - collCircleRadius;
                yStart = 0;
                min = -15 * Mathf.Deg2Rad;
                max = 15 * Mathf.Deg2Rad;
                break;
            case Direction.Up:
                xStart = 0;
                yStart = ScreenUtils.Bottom - collCircleRadius;
                min = 75 * Mathf.Deg2Rad;
                max = 105 * Mathf.Deg2Rad;
                break;
            case Direction.Down:
                xStart = 0;
                yStart = ScreenUtils.Top + collCircleRadius;
                min = 255 * Mathf.Deg2Rad;
                max = 285 * Mathf.Deg2Rad;
                break;
        }
        randomAngleRadians = Random.Range(min, max); 
        Vector2 newVector = new Vector2(Mathf.Cos(randomAngleRadians), Mathf.Sin(randomAngleRadians));
        float randomForce = Random.Range(9, maxAsteroidForce);
        rigidbody2D.AddForce(newVector * maxAsteroidForce, ForceMode2D.Force);
        gameObject.transform.position = new Vector3(xStart, yStart, zStart);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (gameObject.tag.IndexOf("Half") == -1)
            {
                GameObject firstAsteroidHalf = Instantiate<GameObject>(prefabAsteroidHalf, gameObject.transform.position, Quaternion.identity);
                firstAsteroidHalf.tag = "AsteroidHalf";
                GameObject secondAsteroidHalf = Instantiate<GameObject>(prefabAsteroidHalf, gameObject.transform.position, Quaternion.identity);
                secondAsteroidHalf.tag = "AsteroidHalf";
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
            Destroy(gameObject);
        }
    }

}
