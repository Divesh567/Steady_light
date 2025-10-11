using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Vector2 mousePosition;
    private bool isClicking;

    // Input Actions reference
    public PlayerActions playerInput;

    public TouchController touchController;

    private void Awake()
    {
        mainCamera = Camera.main;
        playerInput = new PlayerActions();
    }
    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Mouse.Movement.performed += OnMouseMove;
        playerInput.Mouse.DoMove.performed += OnMouseClick;
        playerInput.Mouse.StopMovement.performed += OnMouseRelease;
    }

    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.Mouse.Movement.performed -= OnMouseMove;
        playerInput.Mouse.DoMove.performed -= OnMouseClick;
        playerInput.Mouse.StopMovement.performed -= OnMouseRelease;

    }

    private void OnMouseMove(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();

        touchController.Movement(mousePosition);
    }

    private void OnMouseClick(InputAction.CallbackContext context)
    {
        isClicking = true;
        touchController.CheckShouldMove(isClicking);
    }

    private void OnMouseRelease(InputAction.CallbackContext context)
    {
        isClicking = false;
        touchController.CheckShouldMove(isClicking);
    }

    private void Update()
    {
        if (MyGameManager.Instance.gameState != MyGameManager.GameState.GameRunning) return;


        if (isClicking)
        {
            Vector2 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
        }
    }
}
