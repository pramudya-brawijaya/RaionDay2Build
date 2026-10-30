using UnityEngine;
using UnityEngine.UI;
using TMPro;

[DefaultExecutionOrder(-100)]
public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }

    [field: SerializeField] public Transform healthHUD {get; private set;}
    [field: SerializeField] public Transform inventoryHUD {get; private set;}

    private PlayerInventory playerInventory;

    public void UpdateHealth(int maxHP, int newHP) {
        TMP_Text hpText = healthHUD.GetComponentInChildren<TMP_Text>();

        hpText.text = newHP.ToString();
    }

    public void UpdateInventory() {
        for (int i = 0; i < PlayerInventory.Instance.inventory.Count; i++)
        {
            Transform slot = inventoryHUD.GetChild(i);
            Item item = PlayerInventory.Instance.inventory[i];

            slot.Find("Equipped").gameObject.SetActive(
                i == PlayerInventory.Instance.currentSlot - 1);

            Image image = slot.Find("Image").GetComponent<Image>();

            if (item != null)
            {
                image.sprite = item.sprite;
                image.enabled = true;
            }
            else
            {
                image.sprite = null;
                image.enabled = false;
            }
        }
    }

    private void Awake() {
        Instance = this;

        playerInventory = PlayerInventory.Instance;
    }
}
