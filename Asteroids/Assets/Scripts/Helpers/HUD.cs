using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Class that handles the heads up display
/// </summary>
public class HUD : MonoBehaviour
{
    // Fields
    Text hud;
    Text level;
    Slider healthBar;
    float healthUnits;

    // Constants associated with Level Number fade out at beginning of level
    const int startFontSize = 180;
    const int endFontSize = 20;
    const float fontChangeTime = 1.5f;
    float fontChangeSpeed;

    // Timer for timing GameOver text
    Timer gameOverTimer;
    bool gameOverFlag = false;
    bool restartAdded = false;

    // Restart button
    public Button prefabBtnRestart;
    Button btnRestart;
    Color restartButtonColor;
    TMP_Text restartButtonText;
    float flashStep;
    const int flashLength = 1;
    bool flashPlace = true; //true = up / false = down

    // Take into account time on the instructions screen
    float timeGameStarted;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize everything
        hud = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        // Health bar
        healthBar = GameObject.Find("Health Bar").GetComponent<Slider>();
        healthUnits = 1 / (float)GameManager.MaxHealth;

        // Level number font speed
        fontChangeSpeed = (startFontSize - endFontSize) / fontChangeTime;
        level = GameObject.Find("LevelText").GetComponent<Text>();

        // Gameover timer
        gameOverTimer = gameObject.AddComponent<Timer>();
        gameOverTimer.Duration = 2;

        // Restart button
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
        // If the game is started and the escape button is pushed
        if (Input.GetAxis("Cancel") > 0 && !GameManager.IsGameStarted)
        {
            GameManager.StartGame();
            timeGameStarted = Time.timeSinceLevelLoad;
            level.fontSize = 180;
        }
        
        if (GameManager.IsGameStarted)
        {
            // if the game has started has started the the ship's health >= 1 then update the score display
            if (GameManager.Health >= 1)
            {
                hud.text = (Time.timeSinceLevelLoad - timeGameStarted).ToString("0.0") +
                            "\nScore: " + GameManager.Score;
            }
            // Update the healthbar
            healthBar.value = GameManager.Health * healthUnits;

            // If the GameManager SpawnFlag is true and the ship has health then the levels can continue progressing
            if (GameManager.SpawnFlag && GameManager.Health >= 1)
            {
                // Manage the level number shrinking
                if (level.fontSize < endFontSize)
                {
                    level.text = "";
                }
                else
                {
                    level.text = GameManager.Level.ToString();
                }
                float newFontSize = level.fontSize - (fontChangeSpeed * Time.deltaTime);
                level.fontSize = (int)(newFontSize);
            }
            else
            {
                level.text = "";
                level.fontSize = 180;
            }
        } else
        {
            // if the SpawnFlag is false and the game hasn't started then the game
            // hasn't started and so the controls should be shown.
            level.fontSize = 20;
            level.text = "Game Controls\n\n" +
                           "Spacebar: Thrust\n" +
                           "Left Arrow: Rotate Anti-Clockwise\n" +
                           "Right Arrow: Rotate Clockwise\n" +
                           "Left Control: Shoot\n\n" +
                           "Practice in this screen\n" +
                           "Hit escape when ready to play!";
        }

        // if the Health is 0 (or less - unlikely) then the game has finished
        if (GameManager.Health <= 0)
        {
            // if the gameover flag is false then set it to true and run the game over timer
            if (!gameOverFlag)
            {
                gameOverTimer.Run();
                gameOverFlag = true;
            }
            // Display game over
            level.fontSize = 130;
            level.text = "Game Over";
            // After the timer has finished then display the restart button
            if (gameOverTimer.Finished)
            {
                level.text = "";
                restartButtonColor.a = 1;
                restartButtonText.color = ButtonFlash(restartButtonText);
                // Check if the restart listener has already been set, if not set it.
                // Set here and not in the Start method as making buttons invisible is
                // difficult. Therefore the button was made invisible.
                if (!restartAdded)
                {
                    restartButtonText.color = new Color(0, 1, 0, 1);
                    btnRestart.onClick.AddListener(Restart);
                    restartAdded = true;
                }
                // If enter button on keyboard is hit then run the restart method
                if (Input.GetAxis("Submit") > 0)
                {
                    Restart();
                }
            }
        }

    }

    /// <summary>
    /// Restart method to load the Menu scene
    /// </summary>
    void Restart()
    {
        SceneManager.LoadScene(0);
        GameManager.IsGameStarted = false;
    }


    /// <summary>
    /// This method returns a color when flashing from the set colour to white over a defined period of time
    /// </summary>
    /// <param name="btnTMP"></param>
    /// <returns></returns>
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
        btnTextColor.b = b;
        return btnTextColor;
    }

}
