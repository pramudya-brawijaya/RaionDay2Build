using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    public int ammoCount;
    public List<Item> inventory = new List<Item> {null, null, null};
    public List<Item> pickableItems = new List<Item>();
    public int currentSlot { get; private set; }
    public Item equippedItem;

    private Notification currentItemName;
    private Notification currentPickUpNotification;

    [SerializeField] private Transform inventoryTransform;

    private InputManager inputManager;

    private void Awake() {
        Instance = this;
        inputManager = InputManager.Instance;

        inputManager.PickItem.performed += PickItem;
        inputManager.DropItem.performed += DropItem;
        inputManager.UseItem.performed += UseItem;
        inputManager.InvSlot1.performed += action => EquipSlot(1);
        inputManager.InvSlot2.performed += action => EquipSlot(2);
        inputManager.InvSlot3.performed += action => EquipSlot(3);

        currentSlot = 1;
        ammoCount = 20;
        HUDManager.Instance.UpdateAmmo();
        HUDManager.Instance.UpdateInventory();
    }

    private void DropItem(InputAction.CallbackContext context) {

        if (equippedItem == null)
            return;

        int index = inventory.IndexOf(equippedItem);

        if (index == -1)
            return;

        inventory[index] = null;
        equippedItem.transform.position = PlayerMovement.Instance.transform.position - Vector3.up * .5f;
        equippedItem.transform.localRotation = Quaternion.identity;
        equippedItem.transform.SetParent(GameObject.Find("Map/Items").transform);
        equippedItem.SetCollider(true);
        equippedItem.gameObject.SetActive(true);
        equippedItem.OnDrop();
        equippedItem = null;

        HUDManager.Instance.UpdateInventory();
    }

    private void EquipSlot(int slot) {
        
        currentSlot = slot;

        if (equippedItem != null)
            equippedItem.gameObject.SetActive(false);

        equippedItem = inventory[slot - 1];

        if (equippedItem != null)
        {
            equippedItem.transform.localPosition = Vector3.zero;
            equippedItem.transform.localRotation = Quaternion.identity;
            equippedItem.gameObject.SetActive(true);
            equippedItem.OnEquip();
        }

        HUDManager.Instance.UpdateInventory();

    }

    public void UpdatePickUpNotification() {

        if (currentItemName != null)
            Destroy(currentItemName.gameObject);

        if (currentPickUpNotification != null)
            Destroy(currentPickUpNotification.gameObject);

        if (pickableItems.Count == 0)
        {
            currentItemName = null;
            currentPickUpNotification = null;
            return;
        }

        Item item = pickableItems[pickableItems.Count - 1];

        currentItemName = NotificationManager.Instance.Create(
            item.itemName,
            item.transform.position + Vector3.up * .5f,
            -1
        );

        currentPickUpNotification = NotificationManager.Instance.Create(
            "Press [E] to pick up item.",
            item.transform.position + Vector3.down * .5f,
            -1
        );
    }

    private void PickItem(InputAction.CallbackContext context) {

        if (pickableItems.Count <= 0) 
            return;

        int index = inventory.FindIndex(itemslot => itemslot == null);

        // inv is full
        if (index == -1)
            return; 

        Item item = pickableItems[pickableItems.Count - 1];

        inventory[index] = item;
        pickableItems.Remove(item);

        item.SetCollider(false);
        item.transform.SetParent(inventoryTransform);
        item.gameObject.SetActive(false);
        item.OnPick();
        HUDManager.Instance.UpdateInventory();
        UpdatePickUpNotification();
        EquipSlot(currentSlot); // prevent item from not appearing when picked up
    }

    private void UseItem(InputAction.CallbackContext context)
    {
        equippedItem?.OnUse();
    }
}
