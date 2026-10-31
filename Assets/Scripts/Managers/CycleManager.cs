using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class CycleManager : MonoBehaviour
{
    public static CycleManager Instance { get; private set; }

    [field: SerializeField] public bool isDay { get; private set; }
    [field: SerializeField] public int cycleNumber { get; private set; }
    [field: SerializeField] public float dayDuration { get; private set; }
    [field: SerializeField] public float dayTimeRemaining { get; private set; }
    [field: SerializeField] public Light2D globalLight { get; private set; }

    private int targetIntensity;
    private bool notified;
    private HUDManager hudManager;

    private void Awake()
    {
        Instance = this;
        hudManager = HUDManager.Instance;

        dayTimeRemaining = dayDuration;
        cycleNumber = 0;

        ToggleDaytime(true);
    }

    private void Update()
    {
        hudManager.UpdateCycle();
        globalLight.intensity = Mathf.MoveTowards(globalLight.intensity, isDay ? 1 : 0, 6 * Time.deltaTime);

        if (!isDay)
            return;

        dayTimeRemaining -= Time.deltaTime;

        if (dayTimeRemaining <= 10f && !notified)
        {
            notified = true;
            NotificationManager.Instance.Create("10 seconds left!", Vector3.zero, 5, PlayerMovement.Instance.transform);

        }
        else if (dayTimeRemaining <= 0f)
        {
            ToggleDaytime(false);
        }
    }

    public void ToggleDaytime(bool dayOrNot) {

        isDay = dayOrNot;

        if (isDay) {

            notified = false;
            dayTimeRemaining = dayDuration;
            ItemManager.Instance.SpawnItems();
            StructureManager.Instance.SpawnStructures();
            cycleNumber++;

        } else {
            WaveManager.Instance.SpawnEnemies();
        } 
    }
}