using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    // Stats
    public int walkSpeed;
    [HideInInspector] public bool canMove;

    [SerializeField] private Vector2 movementInput;

    // Components and Managers
    [HideInInspector] public Rigidbody2D rb;

    private InputManager inputManager;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;

        inputManager = InputManager.Instance;
        Instance = this;
    }

    private void Update() {

        movementInput = inputManager.Move.ReadValue<Vector2>();

    }

    private void FixedUpdate() {

        if (canMove)
            rb.linearVelocity = movementInput * walkSpeed;

    }
}
