using UnityEngine;

public class AmmoBox : Item
{
    public override void OnPick()
    {
        PlayerInventory playerInventory = PlayerInventory.Instance;

        playerInventory.ammoCount += 20;

        int index = playerInventory.inventory.IndexOf(this);

        if (index == -1)
            return;

        playerInventory.inventory[index] = null;
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