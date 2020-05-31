using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using System;

/// <summary>
/// Handle the buttons on the Game Menu
/// </summary>

public class ButtonHandler : MonoBehaviour
{
    #region Fields
    public Button prefabNewGameButton;
    public Button prefabEasyRadio;
    public Button prefabMediumRadio;
    public Button prefabHardRadio;
    public Button prefabAudioToggle;
    public Button prefabGithub;
    public Button prefabCredits;

    public Sprite audioMute;
    public Sprite audioOn;

    Button btnNewGame;
    Button btnEasy;
    Button btnMedium;
    Button btnHard;
    Button btnAudio;
    Button btnGithub;
    Button btnCredits;

    TMP_Text tmpEasy;
    TMP_Text tmpMedium;
    TMP_Text tmpHard;
    Image audioImage;
    #endregion

    // Fields for handling button flashing
    float flashStep;
    const int flashLength = 1;
    bool flashPlace = true; //true = up / false = down

    // Game difficulty based on the Enumeration
    GameDifficulty gameDifficulty;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize all of the buttons and add their methods on clicking
        btnNewGame = prefabNewGameButton.GetComponent<Button>();
        btnNewGame.onClick.AddListener(NewGame);
        btnEasy = prefabEasyRadio.GetComponent<Button>();
        btnEasy.onClick.AddListener(SwitchDifficulty);
        btnMedium = prefabMediumRadio.GetComponent<Button>();
        btnMedium.onClick.AddListener(SwitchDifficulty);
        btnHard = prefabHardRadio.GetComponent<Button>();
        btnHard.onClick.AddListener(SwitchDifficulty);
        btnAudio = prefabAudioToggle.GetComponent<Button>();
        btnAudio.onClick.AddListener(ToggleAudio);
        btnGithub = prefabGithub.GetComponent<Button>();
        btnGithub.onClick.AddListener(LinkToGithub);
        btnCredits = prefabCredits.GetComponent<Button>();
        btnCredits.onClick.AddListener(LinkToCredits);

        // Get the TextMeshPro components for color manipulation
        tmpEasy = btnEasy.GetComponentInChildren<TMP_Text>(true);
        tmpMedium = btnMedium.GetComponentInChildren<TMP_Text>(true);
        tmpHard = btnHard.GetComponentInChildren<TMP_Text>(true);

        // Set the audio image
        audioImage = btnAudio.GetComponent<Image>();

        if (AudioManager.IsSound)
        {
            audioImage.sprite = audioOn;
        } else
        {
            audioImage.sprite = audioMute;
        }

        // Initialize the game if not already initialized
        if (GameManager.Initialized)
        {
            gameDifficulty = GameManager.GameDifficulty;
        } else
        {
            gameDifficulty = GameDifficulty.Medium;
        }

        // Set the button flashing parameters
        flashStep = 1 / (float)flashLength;

    }

    /// <summary>
    /// Load a new gmae
    /// </summary>
    void NewGame()
    {
        // Initialize GameManager if required
        if (!GameManager.Initialized)
        {
            GameManager.Initialize(gameDifficulty);
        } else
        {
            GameManager.ChangeDifficulty(gameDifficulty);
        }
        // Switch to the Game Scene
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    /// <summary>
    /// Switch the difficulty selected. This also sets the button flashing too
    /// </summary>
    void SwitchDifficulty()
    {

        tmpEasy.color = new Color(1, 1, 1, 1);
        tmpMedium.color = new Color(1, 1, 1, 1);
        tmpHard.color = new Color(1, 1, 1, 1);

        switch (EventSystem.current.currentSelectedGameObject.name)
        {
            case "Easy":
                gameDifficulty = GameDifficulty.Easy;
                tmpEasy.color = new Color(0, 0, 1, 1);
                break;
            case "Medium":
                gameDifficulty = GameDifficulty.Medium;
                tmpMedium.color = new Color(0, 0, 1, 1);
                break;
            case "Hard":
                gameDifficulty = GameDifficulty.Hard;
                tmpHard.color = new Color(0, 0, 1, 1);
                break;
        }
    }

    /// <summary>
    /// For handling the input manager
    /// </summary>
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetAxis("Submit") > 0)
            {
                NewGame();
            }
            float inputValueHorizontal = Input.GetAxis("Horizontal");
            if (inputValueHorizontal != 0)
            {
                AudioManager.Play(AudioClipName.Click);
                tmpEasy.color = new Color(1, 1, 1, 1);
                tmpMedium.color = new Color(1, 1, 1, 1);
                tmpHard.color = new Color(1, 1, 1, 1);
                if (inputValueHorizontal > 0)
                {
                    switch (gameDifficulty)
                    {
                        case GameDifficulty.Easy:
                            gameDifficulty = GameDifficulty.Medium;
                            tmpMedium.color = new Color(0, 0, 1, 1);
                            break;
                        case GameDifficulty.Medium:
                            gameDifficulty = GameDifficulty.Hard;
                            tmpHard.color = new Color(0, 0, 1, 1);
                            break;
                        case GameDifficulty.Hard:
                            gameDifficulty = GameDifficulty.Easy;
                            tmpEasy.color = new Color(0, 0, 1, 1);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (gameDifficulty)
                    {
                        case GameDifficulty.Easy:
                            gameDifficulty = GameDifficulty.Hard;
                            tmpHard.color = new Color(0, 0, 1, 1);
                            break;
                        case GameDifficulty.Medium:
                            gameDifficulty = GameDifficulty.Easy;
                            tmpEasy.color = new Color(0, 0, 1, 1);
                            break;
                        case GameDifficulty.Hard:
                            gameDifficulty = GameDifficulty.Medium;
                            tmpMedium.color = new Color(0, 0, 1, 1);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        switch (gameDifficulty)
        {
            case GameDifficulty.Easy:
                tmpEasy.color = ButtonFlash(tmpEasy);
                break;
            case GameDifficulty.Medium:
                tmpMedium.color = ButtonFlash(tmpMedium);
                break;
            case GameDifficulty.Hard:
                tmpHard.color = ButtonFlash(tmpHard);
                break;
        }
        
    }

    /// <summary>
    /// Returns a colour to set the button to that's flashing
    /// </summary>
    /// <param name="btnTMP"></param>
    /// <returns></returns>
    Color ButtonFlash(TMP_Text btnTMP)
    {
        Color btnTextColor = btnTMP.color;

        float r = btnTextColor.r;
        float g = btnTextColor.g;
        btnTextColor.b = 1;
        btnTextColor.a = 1;


        if (flashPlace == true)
        {
            r += flashStep * Time.deltaTime;
            g = r;
            if (r > 1)
            {
                r = 1 - (r - 1);
                g = r;
                flashPlace = false;
            }
        }
        else
        {
            r -= flashStep * Time.deltaTime;
            g = r;
            if (r < 0)
            {
                r = 0 - r;
                g = r;
                flashPlace = true;
            }
        }
        btnTextColor.r = r;
        btnTextColor.g = g;
        return btnTextColor;
    }

    /// <summary>
    /// Handles the Audio button being toggled
    /// </summary>
    void ToggleAudio()
    {
        if (Input.GetAxis("Submit") == 0)
        {
            if (AudioManager.IsSound)
            {
                AudioManager.ChangeAudioToMute(true);
                audioImage.sprite = audioMute;
            }
            else
            {
                AudioManager.ChangeAudioToMute(false);
                audioImage.sprite = audioOn;
            }
        }

    }

    /// <summary>
    /// Handles the two buttons to Github and credits
    /// </summary>
    void LinkToCredits()
    {
        if (Input.GetAxis("Submit") == 0)
        {
            Application.ExternalEval("window.open(\"https://github.com/pageyboy/Asteroids-CopyCat/blob/master/Asteroids/Assets/Credits.txt\")");
        }
    }

    void LinkToGithub()
    {
        if (Input.GetAxis("Submit") == 0)
        { 
            Application.ExternalEval("window.open(\"https://github.com/pageyboy/Asteroids-CopyCat\")");
        }
    }

}
