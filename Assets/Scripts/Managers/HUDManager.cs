using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }

    [field: SerializeField] public Transform healthHUD {get; private set;}

    public void UpdateHealth(int maxHP, int newHP) {
        TMP_Text hpText = healthHUD.GetComponentInChildren<TMP_Text>();

        hpText.text = newHP.ToString();
    }

    private void Awake() {
        Instance = this;

        healthHUD = transform.Find("Health");
    }
}
