using UnityEngine;

public class AmmoBox : Item
{
    public override void OnUse()
    {
        PlayerInventory playerInventory = PlayerInventory.Instance;
        CharacterStat characterStat = playerInventory.transform.GetComponent<CharacterStat>();
        int index = playerInventory.inventory.IndexOf(playerInventory.equippedItem);

        if (index == -1)
            return;

        playerInventory.ammoCount += 20;
        playerInventory.inventory[index] = null;
        playerInventory.equippedItem = null;
        Destroy(gameObject);

        HUDManager.Instance.UpdateInventory();
        HUDManager.Instance.UpdateAmmo();
        NotificationManager.Instance.Create(
                "+20 Ammo",
                Vector3.zero,
                5,
                PlayerMovement.Instance.transform
                );
    }
}