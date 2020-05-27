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
    const int fontChangeTime = 2;
    int fontChangeSpeed;

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
        /*
        if (GameManager.SpawnFlag)
        {
            level.text = GameManager.Level.ToString();
            level.fontSize -= (int)(Time.deltaTime * fontChangeSpeed);
        } else
        {
            level.text = "";
            level.fontSize = 180; 
        }
        */
    }
}
