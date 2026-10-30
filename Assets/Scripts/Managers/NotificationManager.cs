using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }

    [SerializeField] private GameObject notificationPrefab;

    private void Awake() {
        Instance = this;
    }

    public Notification Create(string newText, Vector3 pos, float newDuration, Transform parent = null) {

        if (parent == null) {
            parent = GameObject.Find("Junk").transform;
        } else if (parent == PlayerMovement.Instance.transform) {
            parent = parent.Find("Notification");

            if (parent.childCount >= 7)
            {
                Destroy(parent.GetChild(0).gameObject);
            }
        }

        GameObject notificationObject = Instantiate(notificationPrefab, pos, Quaternion.identity, parent);
        Notification notification = notificationObject.GetComponent<Notification>();

        notification.Initialize(newText, newDuration);

        return notification;
    }
}
