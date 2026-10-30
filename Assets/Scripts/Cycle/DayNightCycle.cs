using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class DayNightCycle : MonoBehaviour
{
    public enum Phase { Day, Night }

    public static DayNightCycle Instance { get; private set; }

    [Header("Referensi")]
    [SerializeField] Light2D globalLight;
    [SerializeField] GameObject campfire;       [SerializeField] TMP_Text timerText;    
    [SerializeField] GameObject sunIcon;
    [SerializeField] GameObject moonIcon;

    [Header("Durasi")]
    [SerializeField] float dayDuration = 30f; 

    [Header("Intensitas Global Light")]
    [SerializeField] float dayIntensity = 1f;
    [SerializeField] float nightIntensity = 0f; 

    public Phase CurrentPhase { get; private set; }
    public int DayNumber { get; private set; } = 1;
    public float TimeRemaining { get; private set; }

    // Buat disambungin ke sistem wave
    public event Action<int> OnDayStarted;
    public event Action<int> OnNightStarted;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        StartDay();
    }

    void Update()
    {
        if (CurrentPhase != Phase.Day) return;

        TimeRemaining -= Time.deltaTime;
        UpdateTimerUI();

        if (TimeRemaining <= 0f)
        {
            TimeRemaining = 0f;
            StartNight();
        }
    }

    void StartDay()
    {
        CurrentPhase = Phase.Day;
        TimeRemaining = dayDuration;

        globalLight.intensity = dayIntensity;
        campfire.SetActive(false);
        sunIcon.SetActive(true);
        moonIcon.SetActive(false);

        UpdateTimerUI();
        OnDayStarted?.Invoke(DayNumber);
    }

    void StartNight()
    {
        CurrentPhase = Phase.Night;

        globalLight.intensity = nightIntensity;
        campfire.SetActive(true);
        sunIcon.SetActive(false);
        moonIcon.SetActive(true);

        if (timerText != null) timerText.text = "";
        OnNightStarted?.Invoke(DayNumber);
    }

    
    public void EndNight()
    {
        if (CurrentPhase != Phase.Night) return;
        DayNumber++;
        StartDay();
    }

    void UpdateTimerUI()
    {
        if (timerText == null) return;
        timerText.text = Mathf.CeilToInt(TimeRemaining).ToString();
    }
}