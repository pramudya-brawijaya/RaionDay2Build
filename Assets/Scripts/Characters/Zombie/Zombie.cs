using UnityEngine;
using UnityEngine.InputSystem;

public class Zombie : MonoBehaviour
{
    // Stats
    public int walkSpeed;
    [HideInInspector] public bool canMove;

    [SerializeField] private Vector3 movementDirection;

    // Components and Managers
    [HideInInspector] public Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
    }

    private void Update() {

        if (PlayerMovement.Instance != null)
            movementDirection = (PlayerMovement.Instance.transform.position - transform.position).normalized;

    }

    private void FixedUpdate() {

        if (canMove)
            rb.linearVelocity = movementDirection * walkSpeed;


    }
}
