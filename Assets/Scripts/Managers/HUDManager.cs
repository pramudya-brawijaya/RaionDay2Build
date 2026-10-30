using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[DefaultExecutionOrder(-300)]
public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }

    [field: SerializeField] public Transform healthHUD {get; private set;}
    [field: SerializeField] public Transform inventoryHUD {get; private set;}
    [field: SerializeField] public Transform ammoHUD {get; private set;}
    [field: SerializeField] public Transform cycleHUD {get; private set;}

    private PlayerInventory playerInventory;

    public void UpdateHealth(int newHP) {
        TMP_Text hpText = healthHUD.GetComponentInChildren<TMP_Text>();

        hpText.text = "x" + newHP.ToString();
    }

    public void UpdateAmmo() {
        TMP_Text ammoText = ammoHUD.GetComponentInChildren<TMP_Text>();

        ammoText.text = "x" + PlayerInventory.Instance.ammoCount.ToString();
    }

    public void UpdateCycle() {
        TMP_Text countText = cycleHUD.Find("NumberText").GetComponent<TMP_Text>();
        TMP_Text dayText = cycleHUD.Find("DayText").GetComponent<TMP_Text>();

        if (CycleManager.Instance.isDay) {
            cycleHUD.Find("SunIcon").gameObject.SetActive(true);
            cycleHUD.Find("MoonIcon").gameObject.SetActive(false);
            countText.text = Mathf.CeilToInt(CycleManager.Instance.dayTimeRemaining).ToString();
        } else {
            cycleHUD.Find("SunIcon").gameObject.SetActive(false);
            cycleHUD.Find("MoonIcon").gameObject.SetActive(true);
            countText.text = "SURVIVE!";
        }

        dayText.text = "DAY " + CycleManager.Instance.cycleNumber;
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
