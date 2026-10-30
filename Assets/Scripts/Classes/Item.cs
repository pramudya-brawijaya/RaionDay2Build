using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [field: SerializeField] public string itemName {get; private set;}
    [field: SerializeField] public string description {get; private set;}
    [field: SerializeField] public Sprite sprite {get; private set;}

    private CircleCollider2D itemCollider;

    protected virtual void Awake()
    {
        itemCollider = GetComponent<CircleCollider2D>();
    }

    public void SetCollider(bool boolean)
    {
        itemCollider.enabled = boolean;
    }

    public virtual void OnUse() {
    }

    public virtual void OnEquip() {
    }

    public virtual void OnPick() {
    }

    public virtual void OnDrop() {
    }

    protected virtual void OnTriggerEnter2D(Collider2D hitCollider)
    {

        if (!hitCollider.transform.parent.gameObject.CompareTag("Player"))
            return;

        if (!PlayerInventory.Instance.pickableItems.Contains(this))
            PlayerInventory.Instance.pickableItems.Add(this);
            PlayerInventory.Instance.UpdatePickUpNotification();

    }

    protected virtual void OnTriggerExit2D(Collider2D hitCollider)
    {
        if (hitCollider.transform.parent.gameObject.CompareTag("Player"))
            PlayerInventory.Instance.pickableItems.Remove(this);
            PlayerInventory.Instance.UpdatePickUpNotification();
    }
}