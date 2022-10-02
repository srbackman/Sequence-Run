using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //private MenuManager menuManager;
    private PlayerMovement playerMovement;

    [SerializeField] private LevelCore[] _levelCores;
    private int _currentLevel = 0;
    public bool _inGameStatus = false;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }


    void Update()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void ResetPlayer()
    {
        playerMovement.AddBounceForce(Vector2.zero);
    }

    public void GameOver()
    {

    }

    public void RestartLevel()
    {

    }

    public void NextLevel()
    {
        _currentLevel++;
        if (_currentLevel >= _levelCores.Length)
        {

        }


    }


}
