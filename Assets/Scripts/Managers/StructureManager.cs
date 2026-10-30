using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

// copied from itemmanager
[DefaultExecutionOrder(-200)]
public class StructureManager : MonoBehaviour
{
    public static StructureManager Instance { get; private set; }

    public int spawnAttempts;
    public int maxPositionAttempts;
    public float extendedCampfireExclusionRadius;
    public float minStructureDistance;

    public Light2D campfireLight;
    public GameObject floor;
    public List<GameObject> structurePrefabs;

    private SpriteRenderer floorRenderer;
    private List<GameObject> spawnedStructures = new List<GameObject>();

    private void Awake() {
        Instance = this;

        floorRenderer = floor.GetComponent<SpriteRenderer>();
    }

    public void SpawnStructures() {

        foreach (GameObject elementObject in spawnedStructures)
        {
            if (elementObject == null)
                continue;

            if (elementObject.name == "Campfire" || elementObject.name == "Floor")
                continue;

            Destroy(elementObject);
        }

        spawnedStructures.Clear();

        Bounds bounds = floorRenderer.bounds;

        for (int i = 0; i <= spawnAttempts; i++) {

            if (UnityEngine.Random.Range(1, 10) > 2)
                continue;

            int randomIndex = UnityEngine.Random.Range(0, structurePrefabs.Count);
            GameObject randomItem = structurePrefabs[randomIndex];

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

                foreach (GameObject item in spawnedStructures)
                {
                    if (Vector2.Distance(randomPosition, item.transform.position) < minStructureDistance)
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

                spawnedStructures.Add(spawnedItem);
            }
        }
    }
}
