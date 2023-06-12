using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Singleton pattern
    public static Player Instance { get; private set; } // public get, but private set
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    private bool isWalking;
    private Vector3 lastInteractDirection;
    private ClearCounter selectedCounter;

    private void Awake()
    {
        // Singleton pattern: need to check that we have only one instance of Player
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }
    private void Start()
    {
        // 3. Listen the event from GameInput.cs
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }
    // listener function
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact();
        }
    }
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDirection != Vector3.zero)
        {
            lastInteractDirection = moveDirection;
        }

        float interactDistance = 2f;
        // RaycastHit raycastHit; // alternatively we can define it here

        if (Physics.Raycast(
            transform.position,
            // moveDirection,
            lastInteractDirection, // instead of moveDirection, so even if player stops moving we still can use last interact direction
            out RaycastHit raycastHit, // here we define var and pass it immediately, so we can use it later
            interactDistance,
            countersLayerMask
        ))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                // clearCounter.Interact();
                if (clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }

            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        // we need to cast Vector2 to Vector3 to be able to set new value to the transform.position
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;

        bool canMove = !Physics.CapsuleCast(
            transform.position, // bottom point
            transform.position + Vector3.up * playerHeight, // top point (player head)
            playerRadius,
            moveDirection,
            moveDistance
        );

        if (!canMove)
        {
            // attempt only X move
            Vector3 moveDirX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDirection = moveDirX;
            }
            else
            {
                // attempt only Z move
                Vector3 moveDirZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDirection = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            // transform refers to the Transform of the object the script is attached to
            // * Time.deltaTime makes movement independent from frame rate
            // otherwise the speed will depend on FPS value (frames per second)
            // transform.position += moveDirection * moveSpeed * Time.deltaTime;

            transform.position += moveDirection * moveDistance;
        }

        isWalking = moveDirection != Vector3.zero;

        // to rotate player in right direction
        // transform.forward = moveDirection;
        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
    }

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
}
