using System.Collections;
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

        gameDifficulty = GameDifficulty.Easy;
        tmpEasy.color = Color.blue;

    }

    void NewGame()
    {
        // initialize screen utils
        if (!GameManager.Initialized)
        {
            GameManager.Initialize(GameDifficulty.Easy);
        }
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    void SwitchDifficulty()
    {

        tmpEasy.color = Color.white;
        tmpMedium.color = Color.white;
        tmpHard.color = Color.white;

        switch (EventSystem.current.currentSelectedGameObject.name)
        {
            case "Easy":
                gameDifficulty = GameDifficulty.Easy;
                tmpEasy.color = Color.blue;
                break;
            case "Medium":
                gameDifficulty = GameDifficulty.Medium;
                tmpMedium.color = Color.blue;
                break;
            case "Hard":
                gameDifficulty = GameDifficulty.Hard;
                tmpHard.color = Color.blue;
                break;
        }

        print(gameDifficulty);
    }

}
