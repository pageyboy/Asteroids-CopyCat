using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Sprite[] asteroidSprites = new Sprite[3];

    private new Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;

    float collCircleRadius;
    const float maxAsteroidForce = 100;

    private void OnBecameInvisible()
    {
        ScreenWrapper.AdjustPosition(gameObject, collCircleRadius);
    }

    public void Initialize(Direction direction)
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        collCircleRadius = gameObject.GetComponent<CircleCollider2D>().radius;
        asteroidSprites[0] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidGreen");
        asteroidSprites[1] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidMagenta");
        asteroidSprites[2] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidWhite");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        int randomSprite = Random.Range(0, 3);
        spriteRenderer.sprite = asteroidSprites[randomSprite];

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

}
