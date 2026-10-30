using UnityEngine;

public class Wood : Item
{
    public override void OnUse()
    {

        CircleCollider2D collider = GetComponent<CircleCollider2D>(); 

        Collider2D[] hitColliders  = Physics2D.OverlapCircleAll(transform.position, collider.radius * 2);
        Campfire campfire = null;

        foreach (Collider2D hitCollider in hitColliders)
        {
            campfire = hitCollider.GetComponentInParent<Campfire>();

            if (campfire != null)
                break;
        }

        if (campfire == null)
        {
            NotificationManager.Instance.Create(
                "Use this near the campfire to refuel the fire!",
                Vector3.zero,
                5,
                PlayerMovement.Instance.transform
            );

            return;
        }

        campfire.ModifyDuration(7);

        PlayerInventory playerInventory = PlayerInventory.Instance;

        int index = playerInventory.inventory.IndexOf(
            playerInventory.equippedItem
        );

        if (index == -1)
            return;

        playerInventory.inventory[index] = null;
        playerInventory.equippedItem = null;

        Destroy(gameObject);

        HUDManager.Instance.UpdateInventory();
    }
}