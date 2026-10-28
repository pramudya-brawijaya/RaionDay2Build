using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    // Stats
    public int walkSpeed;

    [SerializeField] private Vector3 movementDirection;

    private Vector2 movementInput;

    // Components and Managers
    private Rigidbody2D rb;
    private InputManager inputManager;

    private void Awake() {

        rb = GetComponent<Rigidbody2D>();

        inputManager = InputManager.Instance;
    }

    private void Update() {

        movementInput = inputManager.Move.ReadValue<Vector2>();

    }

    private void FixedUpdate() {

        rb.linearVelocity = movementInput * walkSpeed;

    }

}
