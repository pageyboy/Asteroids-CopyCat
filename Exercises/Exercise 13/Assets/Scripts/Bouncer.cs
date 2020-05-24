using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{

    #region Fields

    int health = 100;
    int healthLoss = 10;
    float colorLoss;
    Color spriteColor;
    SpriteRenderer sprite;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rg2d = gameObject.GetComponent<Rigidbody2D>();
        rg2d.AddForce(new Vector2(10, 10), ForceMode2D.Impulse);
        sprite = gameObject.GetComponent<SpriteRenderer>();
        spriteColor =sprite.color;
        colorLoss = 1f / healthLoss;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        health -= healthLoss;
        spriteColor.a -= colorLoss;
        sprite.color = spriteColor;
        print(spriteColor.a);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
