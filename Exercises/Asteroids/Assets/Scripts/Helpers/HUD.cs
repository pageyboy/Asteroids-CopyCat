using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    Timer gameOverTimer;
    public Button prefabBtnRestart;
    Button btnRestart;
    Color restartButtonColor;
    TMP_Text restartButtonText;
    bool gameOverFlag = false;
    bool restartAdded = false;

    float flashStep;
    const int flashLength = 1;
    bool flashPlace = true; //true = up / false = down

    // Start is called before the first frame update
    void Start()
    {
        hud = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        healthBar = GameObject.Find("Health Bar").GetComponent<Slider>();
        healthUnits = 1 / (float)GameManager.MaxHealth;
        fontChangeSpeed = (startFontSize - endFontSize) / fontChangeTime;
        level = GameObject.Find("LevelText").GetComponent<Text>();
        gameOverTimer = gameObject.AddComponent<Timer>();
        gameOverTimer.Duration = 2;
        btnRestart = prefabBtnRestart.GetComponent<Button>();
        restartButtonText = btnRestart.GetComponentInChildren<TMP_Text>();
        restartButtonColor = restartButtonText.color;
        restartButtonColor.a = 0;
        restartButtonText.color = restartButtonColor;
        flashStep = 1 / (float)flashLength;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Health >= 1) {
            hud.text = Time.timeSinceLevelLoad.ToString("0.0") +
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

        if (GameManager.Health <= 0)
        {
            if (!gameOverFlag)
            {
                gameOverTimer.Run();
                gameOverFlag = true;
            }
            level.fontSize = 130;
            level.text = "Game Over";
            if (gameOverTimer.Finished)
            {
                level.text = "";
                restartButtonColor.a = 1;
                restartButtonText.color = ButtonFlash(restartButtonText);
                if (!restartAdded)
                {
                    restartButtonText.color = new Color(0, 1, 0, 1);
                    btnRestart.onClick.AddListener(Restart);
                    restartAdded = true;
                }
            }
        }

    }

    void Restart()
    {
        SceneManager.LoadScene(0);
        //GameManager.
    }

    Color ButtonFlash(TMP_Text btnTMP)
    {
        Color btnTextColor = btnTMP.color;

        float r = btnTextColor.r;
        float b = btnTextColor.b;
        btnTextColor.g = 1;
        btnTextColor.a = 1;


        if (flashPlace == true)
        {
            r += flashStep * Time.deltaTime;
            b = r;
            if (r > 1)
            {
                r = 1 - (r - 1);
                b = r;
                flashPlace = false;
            }
        }
        else
        {
            r -= flashStep * Time.deltaTime;
            b = r;
            if (r < 0)
            {
                r = 0 - r;
                b = r;
                flashPlace = true;
            }
        }
        btnTextColor.r = r;
        btnTextColor.g = b;
        return btnTextColor;
    }

}
