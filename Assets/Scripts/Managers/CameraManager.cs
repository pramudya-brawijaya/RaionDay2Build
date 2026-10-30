using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public Transform cameraTransform;
    public Transform attachedTransform;


    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (attachedTransform != null)
            cameraTransform.position = new Vector3(attachedTransform.position.x, attachedTransform.position.y, -10); 
    }
}
