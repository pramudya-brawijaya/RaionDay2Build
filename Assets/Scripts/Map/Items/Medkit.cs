using UnityEngine;

public class Medkit : Item
{
    public override void OnUse()
    {
        PlayerInventory playerInventory = PlayerInventory.Instance;
        CharacterStat characterStat = playerInventory.transform.GetComponent<CharacterStat>();
        int index = playerInventory.inventory.IndexOf(playerInventory.equippedItem);

        if (index == -1)
            return;

        characterStat.ModifyHP(5);
        playerInventory.inventory[index] = null;
        playerInventory.equippedItem = null;
        Destroy(gameObject);

        HUDManager.Instance.UpdateInventory();
        HUDManager.Instance.UpdateHealth(characterStat.currentHP);
        NotificationManager.Instance.Create(
                "+5 HP",
                Vector3.zero,
                5,
                PlayerMovement.Instance.transform
                );
    }
}