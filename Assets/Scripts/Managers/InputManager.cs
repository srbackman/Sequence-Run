using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GameManager gameManager;
    private MenuManager menuManager;
    private PlayerInputactions inputActions;
    

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        menuManager = FindObjectOfType<MenuManager>();
    }

    private void OnEnable()
    {
        inputActions = new PlayerInputactions();
        inputActions.Player.Enable();
    }


    void Update()
    {
        if (gameManager._gameState != GameState.inGame || gameManager._countdownOn) return;
        bool jumpStatus = inputActions.Player.Jump.IsPressed();
        float movementDirection = inputActions.Player.Move.ReadValue<Vector2>().x;
        playerMovement.MovePlayer(movementDirection, jumpStatus);

        if (inputActions.Player.Reset.WasPressedThisFrame())
        { gameManager.RestartLevel(); }
        if (inputActions.Player.Escape.WasPressedThisFrame())
        { menuManager.GoToPauseMenu(); }
    }
}
