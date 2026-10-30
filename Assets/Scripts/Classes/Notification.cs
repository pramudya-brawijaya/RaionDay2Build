using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    // duration -1 is infinite
    public float duration;

    private TMP_Text tmpText;

    private void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
    }

    public void Initialize(string newText, float newDuration)
    {
        duration = newDuration;
        tmpText.text = newText;
    }

    private void Update()
    {
        if (duration != -1) {
            duration -= Time.deltaTime;

            if (duration <= 0)
                Destroy(gameObject);
        }
    }
}