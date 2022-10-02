using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GameManager gameManager;
    private PlayerInputactions inputActions;
    

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();

    }

    private void OnEnable()
    {
        inputActions = new PlayerInputactions();
        inputActions.Player.Enable();
    }


    void Update()
    {
        if (!gameManager._inGameStatus) return;
        bool jumpStatus = inputActions.Player.Jump.IsPressed();
        float movementDirection = inputActions.Player.Move.ReadValue<Vector2>().x;
        playerMovement.MovePlayer(movementDirection, jumpStatus);
    }
}
