using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    Text hud;
    Text level;
    Slider healthBar;
    float healthUnits;
    const int startFontSize = 180;
    const int endFontSize = 20;
    const float fontChangeTime = 1.5f;
    float fontChangeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        hud = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        healthBar = GameObject.Find("Health Bar").GetComponent<Slider>();
        healthUnits = 1 / (float)GameManager.MaxHealth;
        fontChangeSpeed = (startFontSize - endFontSize) / fontChangeTime;
        level = GameObject.Find("LevelText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Health >= 1) {
            hud.text = Time.realtimeSinceStartup.ToString("0.0") +
                        "\nScore: " + GameManager.Score +
                        "\nLevel: " + GameManager.Level;
        }
        healthBar.value = GameManager.Health * healthUnits;
        
        if (GameManager.SpawnFlag && GameManager.Health >= 1)
        {
            if (level.fontSize < endFontSize)
            {
                level.text = "";
            } else
            {
                level.text = GameManager.Level.ToString();
            }
            float newFontSize = level.fontSize - (fontChangeSpeed * Time.deltaTime);
            level.fontSize = (int)(newFontSize);
        } else
        {
            level.text = "";
            level.fontSize = 180;
        }

        if (GameManager.Health == 0)
        {
            level.fontSize = 130;
            level.text = "Game Over";
        }

    }
}
