using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class ButtonHandler : MonoBehaviour
{

    public Button prefabNewGameButton;
    public Button prefabEasyRadio;
    public Button prefabMediumRadio;
    public Button prefabHardRadio;
    public Button prefabAudioToggle;

    public Sprite audioMute;
    public Sprite audioOn;

    Button btnNewGame;
    Button btnEasy;
    Button btnMedium;
    Button btnHard;
    Button btnAudio;

    TMP_Text tmpEasy;
    TMP_Text tmpMedium;
    TMP_Text tmpHard;
    Image audioImage;

    float flashStep;
    const int flashLength = 1;
    bool flashPlace = true; //true = up / false = down

    GameDifficulty gameDifficulty;

    DateTime nextHorizontalAllowed = DateTime.Now.AddSeconds(-2);

    bool horizontalPositiveLast;

    // Start is called before the first frame update
    void Start()
    {
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

        tmpEasy = btnEasy.GetComponentInChildren<TMP_Text>(true);
        tmpMedium = btnMedium.GetComponentInChildren<TMP_Text>(true);
        tmpHard = btnHard.GetComponentInChildren<TMP_Text>(true);

        audioImage = btnAudio.GetComponent<Image>();

        if (AudioManager.IsSound)
        {
            audioImage.sprite = audioOn;
        } else
        {
            audioImage.sprite = audioMute;
        }

        if (GameManager.Initialized)
        {
            gameDifficulty = GameManager.GameDifficulty;
        } else
        {
            gameDifficulty = GameDifficulty.Hard;
        }

        flashStep = 1 / (float)flashLength;

    }

    void NewGame()
    {
        // initialize screen utils
        if (!GameManager.Initialized)
        {
            GameManager.Initialize(gameDifficulty);
        } else
        {
            GameManager.ChangeDifficulty(gameDifficulty);
        }
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

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

        print(gameDifficulty);
    }

    private void Update()
    {
        if (Input.GetAxis("Submit") > 0)
        {
            NewGame();
        }
        float inputValueHorizontal = Input.GetAxis("Horizontal");
        if (inputValueHorizontal != 0 &&
            (DateTime.Now > nextHorizontalAllowed || (inputValueHorizontal > 0 == !horizontalPositiveLast) || (inputValueHorizontal < 0 == horizontalPositiveLast)))
        {
            AudioManager.Play(AudioClipName.Click);
            tmpEasy.color = new Color(1, 1, 1, 1);
            tmpMedium.color = new Color(1, 1, 1, 1);
            tmpHard.color = new Color(1, 1, 1, 1);
            nextHorizontalAllowed = DateTime.Now.AddMilliseconds(500);
            if (inputValueHorizontal > 0)
            {
                horizontalPositiveLast = true;
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
            } else
            {
                horizontalPositiveLast = false;
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

}
