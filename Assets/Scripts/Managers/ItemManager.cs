using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

[DefaultExecutionOrder(-200)]
public class ItemManager : MonoBehaviour
{

    public static ItemManager Instance { get; private set; }

    public int spawnAttempts;
    public int maxPositionAttempts;
    public float extendedCampfireExclusionRadius;
    public float minItemDistance;

    public Light2D campfireLight;
    public GameObject floor;
    public List<GameObject> itemPrefabs;

    private SpriteRenderer floorRenderer;
    private List<GameObject> spawnedItems = new List<GameObject>();

    private void Awake() {
        Instance = this;

        floorRenderer = floor.GetComponent<SpriteRenderer>();
    }

    public void SpawnItems() {

        foreach (Transform child in transform)
        {
            float distance = Vector2.Distance(child.position, campfireLight.transform.position);

            float safeRadius = campfireLight.pointLightOuterRadius + extendedCampfireExclusionRadius;

            if (distance > safeRadius)
                Destroy(child.gameObject);
        }

        spawnedItems.RemoveAll(item => item == null);

        Bounds bounds = floorRenderer.bounds;

        for (int i = 0; i <= spawnAttempts; i++) {

            if (UnityEngine.Random.Range(1, 10) > 3) // LOLOLOLOL
                continue;

            int randomIndex = UnityEngine.Random.Range(0, itemPrefabs.Count);
            GameObject randomItem = itemPrefabs[randomIndex];

            Vector3 spawnPosition = Vector3.zero;

            for (int j = 0; j <= maxPositionAttempts; j++) {

                Vector3 randomPosition = new Vector3(
                    Random.Range(bounds.min.x, bounds.max.x),
                    Random.Range(bounds.min.y, bounds.max.y),
                    floor.transform.position.z);

                float campfireDistance = Vector2.Distance(randomPosition, campfireLight.transform.position);

                if (campfireDistance < campfireLight.pointLightOuterRadius + extendedCampfireExclusionRadius)
                    continue;

                bool tooClose = false;

                foreach (GameObject item in spawnedItems)
                {
                    if (Vector2.Distance(randomPosition, item.transform.position) < minItemDistance)
                    {
                        tooClose = true;
                        break;
                    }
                }

                if (tooClose)
                    continue;

                spawnPosition = randomPosition;
                break;
            }

            if (spawnPosition != Vector3.zero) {
                GameObject spawnedItem = Instantiate(randomItem, spawnPosition, Quaternion.identity, transform);

                spawnedItems.Add(spawnedItem);
            }
        }
    }
}
