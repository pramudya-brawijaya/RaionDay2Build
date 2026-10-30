using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private void OnEnable()
    {
        CharacterStat.OnPlayerDeath += DisablePlayerMovement;
    }

    private void OnDisable()
    {
        CharacterStat.OnPlayerDeath -= DisablePlayerMovement;
    }
    private void Start()
    {
        EnablePlayerMovement();
    }
    public static PlayerMovement Instance { get; private set; }

    // Stats
    public int walkSpeed;
    [HideInInspector] public bool canMove;

    [SerializeField] private Vector3 movementDirection;

    private Vector2 movementInput;

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

    private void DisablePlayerMovement()
    {
        inputManager.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void EnablePlayerMovement()
    {
        inputManager.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
