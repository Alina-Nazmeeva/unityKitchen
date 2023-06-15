using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    // 1. define an event
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        // construct and activate Input system
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        // add listener to the event ("E" is pressed) from InputAction.cs
        playerInputActions.Player.Interact.performed += Interact_performed;

        // add listener to the event ("F" is pressed) from InputAction.cs
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    // Listener signature should match the event signature
    // Action<UnityEngine.InputSystem.InputAction.CallbackContext> UnityEngine.InputSystem.InputAction.performed
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        // 2. Fire the event (this event is being listened in Player.cs)
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        // 2. Fire the event (this event is being listened in Player.cs)
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }
    public Vector2 GetMovementVectorNormalized()
    {
        // using new Input System
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        // using legacy Input Manager
        // Vector2 inputVector = new Vector2(0, 0);
        // if (Input.GetKey(KeyCode.W))
        // {
        //     inputVector.y = +1;
        // }
        // if (Input.GetKey(KeyCode.S))
        // {
        //     inputVector.y = -1;
        // }
        // if (Input.GetKey(KeyCode.A))
        // {
        //     inputVector.x = -1;
        // }
        // if (Input.GetKey(KeyCode.D))
        // {
        //     inputVector.x = +1;
        // }

        // to normalize diagonal movement otherwise it will move faster diagonally
        // alternatively: in Unity open PlayerInputActions (Input Action Asset) and add a Processor
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
