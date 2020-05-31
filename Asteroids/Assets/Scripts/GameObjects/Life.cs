using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    Timer lifeTimer;
    Color lifeColor;
    SpriteRenderer spriteRenderer;
    int lifeTime = 10;
    float lifeAlphaStep;

    private void Start()
    {
        lifeTimer = gameObject.AddComponent<Timer>();
        lifeTimer.Duration = lifeTime;
        lifeTimer.Run();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        lifeColor = spriteRenderer.color;
        lifeAlphaStep = 1 / (float)lifeTime;
    }

    private void Update()
    {
        if (lifeTimer.Finished)
        {
            Destroy(gameObject);
        }
        lifeColor.a -= Time.deltaTime * lifeAlphaStep;
        spriteRenderer.color = lifeColor;
        
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
