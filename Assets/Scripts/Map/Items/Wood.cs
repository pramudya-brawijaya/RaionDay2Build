using UnityEngine;

public class Wood : Item
{
    public override void OnUse()
    {
        NotificationManager.Instance.Create(
                "Use this near the campfire to refuel the fire!",
                Vector3.zero,
                5,
                PlayerMovement.Instance.transform
                );
    }
}