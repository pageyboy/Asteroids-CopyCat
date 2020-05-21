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

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        collCircleRadius = gameObject.GetComponent<CircleCollider2D>().radius;
        float randomAngleRadians = Random.Range(0, Mathf.PI * 2);
        Vector2 newVector = new Vector2(Mathf.Cos(randomAngleRadians), Mathf.Sin(randomAngleRadians));
        float randomForce = Random.Range(9, maxAsteroidForce);
        rigidbody2D.AddForce(newVector * maxAsteroidForce, ForceMode2D.Force);
        asteroidSprites[0] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidGreen");
        asteroidSprites[1] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidMagenta");
        asteroidSprites[2] = Resources.Load<Sprite>(@"Sprites\Asteroids\AsteroidWhite");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        int randomSprite = Random.Range(0, 3);
        spriteRenderer.sprite = asteroidSprites[randomSprite];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible()
    {
        ScreenWrapper.AdjustPosition(gameObject, collCircleRadius);
    }

}
