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
    public InputAction PickItem { get; private set; }
    public InputAction DropItem { get; private set; }
    public InputAction UseItem { get; private set; } 
    public InputAction InvSlot1 { get; private set; }
    public InputAction InvSlot2 { get; private set; }
    public InputAction InvSlot3 { get; private set; }

    private void Awake() {
        Instance = this;
        playerInputMap = inputActionAsset.FindActionMap("Player", true);

        Move = playerInputMap.FindAction("Move", true);
        PickItem = playerInputMap.FindAction("PickItem", true);
        DropItem = playerInputMap.FindAction("DropItem", true);
        UseItem = playerInputMap.FindAction("UseItem", true);
        InvSlot1 = playerInputMap.FindAction("InvSlot1", true);
        InvSlot2 = playerInputMap.FindAction("InvSlot2", true);
        InvSlot3 = playerInputMap.FindAction("InvSlot3", true);

        inputActionAsset.Enable();
        DontDestroyOnLoad(gameObject);
    }
}
