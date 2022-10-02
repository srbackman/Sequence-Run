using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _inGameHud;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _continueMenu;
    [SerializeField] private GameObject _winnerMenu;

    private GameObject _currentMenu;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        _currentMenu = _mainMenu;
        _currentMenu.SetActive(true);

    }

    private void CurrentMenuSwitch(GameObject newCurrentMenu)
    {
        _currentMenu.SetActive(false);
        _currentMenu = newCurrentMenu;
        _currentMenu.SetActive(true);
    }

    public void GoToMainMenu()
    {
        CurrentMenuSwitch(_mainMenu);
        gameManager._inGameStatus = false;

    }

    public void GoToInGameHud()
    {
        CurrentMenuSwitch(_inGameHud);
        gameManager._inGameStatus = true;

    }

    public void GoToPauseMenu()
    {
        CurrentMenuSwitch(_pauseMenu);
        gameManager._inGameStatus = false;

    }

    public void GoToGameOverMenu()
    {
        CurrentMenuSwitch(_gameOverMenu);
        gameManager._inGameStatus = false;

    }

    public void GoToContinueMenu()
    {
        CurrentMenuSwitch(_continueMenu);
        gameManager._inGameStatus = false;
    }

    public void GoToWinnerMenu()
    {
        CurrentMenuSwitch(_winnerMenu);
        gameManager._inGameStatus = false;

    }
}
