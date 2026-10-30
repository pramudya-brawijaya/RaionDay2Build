using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Campfire : MonoBehaviour
{
    public float maxFireDuration;
    public float fireDuration;
    public Sprite litSprite;
    public Sprite unlitSprite;
    public Transform greenBar;
    public Light2D spotLight;

    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        NotificationManager.Instance.Create(
            "Bring wood here to refuel the campfire!",
            Vector3.up,
            60
            );
    }

    private void Update()
    {
        if (fireDuration > 0) {
            spotLight.intensity = 1;
            fireDuration -= Time.deltaTime;
            spriteRenderer.sprite = litSprite;

            greenBar.parent.gameObject.SetActive(true);

            float ratio = fireDuration / maxFireDuration;
            greenBar.localScale = new Vector3(ratio, 1, 1);
            greenBar.localPosition = new Vector3(-1 * (1 - ratio) / 2, 0, 0);
        } else {
            greenBar.parent.gameObject.SetActive(false);
            spotLight.intensity = 0;
            spriteRenderer.sprite = unlitSprite;
        }
    }

    public void ModifyDuration(int amount) {
        fireDuration = Mathf.Clamp(fireDuration + amount, 0, maxFireDuration);
    }
}
