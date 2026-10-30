using UnityEngine;

public class Bullet : MonoBehaviour
{

    [field: SerializeField] public int damage {get; private set;}
    [field: SerializeField] public int speed {get; private set;}

    public float timer = 30;

    private void Update() {
        if (timer > 0) {
            transform.position += transform.up * Time.deltaTime * speed;
            timer -= Time.deltaTime;
        } else {
            GameObject.Destroy(gameObject);
        }   
    }
}
