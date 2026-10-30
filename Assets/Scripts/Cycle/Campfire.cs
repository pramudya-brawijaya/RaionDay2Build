using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class Campfire : MonoBehaviour
{
    [Header("Durasi (detik)")]
    [SerializeField] float startDuration = 60f;
    [SerializeField] float woodAddDuration = 30f;
    [SerializeField] float maxDuration = 120f;

    [Header("Deteksi Kayu")]
    [SerializeField] string woodTag = "Wood";
    [SerializeField] float woodDetectRadius = 1.5f;

    [Header("UI (opsional)")]
    [SerializeField] TMP_Text durationText;

    [Header("Debug")]
    [SerializeField] bool debugLog = true;

    public float TimeRemaining { get; private set; }
    public bool IsBurning { get; private set; }

    SpriteRenderer[] fireSprites;
    Light2D fireLight;
    float logTimer;

    void Awake()
    {
        fireSprites = GetComponentsInChildren<SpriteRenderer>(true);
        fireLight = GetComponentInChildren<Light2D>(true);
    }

    void OnEnable()
    {
        TimeRemaining = startDuration;
        SetBurning(true);
    }

    void OnDisable()
    {
        if (durationText != null) durationText.text = "";
    }

    void Update()
    {
        if (!IsBurning) return;

        TimeRemaining -= Time.deltaTime;
        if (TimeRemaining <= 0f)
        {
            TimeRemaining = 0f;
            SetBurning(false);
            UpdateUI();
            return;
        }

        CheckWood();
        UpdateUI();
    }

   void CheckWood()
{
    Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, woodDetectRadius);

    foreach (Collider2D hit in hits)
    {
        if (!hit.CompareTag(woodTag)) continue;

        // Skip kayu yang lagi dipegang (masih jadi bagian dari player)
        if (hit.GetComponentInParent<PlayerInventory>() != null) continue;

        TimeRemaining = Mathf.Min(TimeRemaining + woodAddDuration, maxDuration);
        Debug.Log($"Kayu dimakan: {hit.name}. Sisa durasi: {TimeRemaining:0}");
        Destroy(hit.gameObject);
    }
}
    void UpdateUI()
    {
        if (durationText == null) return;
        durationText.text = IsBurning ? Mathf.CeilToInt(TimeRemaining).ToString() : "0";
    }

    void SetBurning(bool burning)
    {
        IsBurning = burning;
        foreach (SpriteRenderer sr in fireSprites) sr.enabled = burning;
        if (fireLight != null) fireLight.enabled = burning;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, woodDetectRadius);
    }
}