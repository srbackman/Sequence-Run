using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private GameObject _mainMenu;
    [Space]
    [SerializeField] private GameObject _inGameHud;
    [Space]
    [SerializeField] private GameObject _pauseMenu;
    [Space]
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private TMP_Text _causeOfDeathText;
    [Space]
    [SerializeField] private GameObject _continueMenu;
    [Space]
    [SerializeField] private GameObject _winnerMenu;

    private GameObject _currentMenu;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        _currentMenu = _mainMenu;
        _currentMenu.SetActive(true);

    }

    private void CurrentMenuSwitch(GameObject newCurrentMenu, GameState gameState)
    {
        _currentMenu.SetActive(false);
        _currentMenu = newCurrentMenu;
        _currentMenu.SetActive(true);

        gameManager._gameState = gameState;
    }

    public void GoToMainMenu()
    {
        CurrentMenuSwitch(_mainMenu, GameState.pause);

    }

    public void GoToInGameHud()
    {
        CurrentMenuSwitch(_inGameHud, GameState.inGame);

    }

    public void GoToPauseMenu()
    {
        CurrentMenuSwitch(_pauseMenu, GameState.pause);

    }

    public void GoToGameOverMenu(string deathText)
    {
        CurrentMenuSwitch(_gameOverMenu, GameState.pause);
        _causeOfDeathText.text = deathText;
        gameManager.StopTiming();
    }

    public void GoToContinueMenu()
    {
        CurrentMenuSwitch(_continueMenu, GameState.pause);

    }

    public void GoToWinnerMenu()
    {
        CurrentMenuSwitch(_winnerMenu, GameState.pause);

    }
}
