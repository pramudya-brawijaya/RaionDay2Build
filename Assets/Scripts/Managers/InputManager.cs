using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public InputActionAsset inputActionAsset;

    private InputActionMap playerInputMap;

    // Inputs
    public InputAction Move { get; private set; }

    private void Awake() {
        Instance = this;

        playerInputMap = inputActionAsset.FindActionMap("Player", true);
        Move = playerInputMap.FindAction("Move", true);

        inputActionAsset.Enable();

        DontDestroyOnLoad(gameObject);
    }
}
