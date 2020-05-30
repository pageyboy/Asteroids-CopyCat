﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonHandler : MonoBehaviour
{

    public Button prefabNewGameButton;
    public Button prefabEasyRadio;
    public Button prefabMediumRadio;
    public Button prefabHardRadio;

    Button btnNewGame;
    Button btnEasy;
    Button btnMedium;
    Button btnHard;

    TMP_Text tmpEasy;
    TMP_Text tmpMedium;
    TMP_Text tmpHard;

    float flashStep;
    const int flashLength = 1;
    bool flashPlace = true; //true = up / false = down

    GameDifficulty gameDifficulty;

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

        tmpEasy = btnEasy.GetComponentInChildren<TMP_Text>(true);
        tmpMedium = btnMedium.GetComponentInChildren<TMP_Text>(true);
        tmpHard = btnHard.GetComponentInChildren<TMP_Text>(true);

        gameDifficulty = GameDifficulty.Hard;

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

}
