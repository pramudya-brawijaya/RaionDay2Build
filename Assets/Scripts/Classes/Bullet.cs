using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{

    [field: SerializeField] public int damage {get; private set;}
    [field: SerializeField] public int speed {get; private set;}
    [field: SerializeField] public bool isPlayerBullet {get; private set;}

    public float timer = 30;

    private List<Collider2D> hitColliders = new List<Collider2D>(); 

    private void Update() {
        if (timer > 0) {
            transform.position += transform.up * Time.deltaTime * speed;
            timer -= Time.deltaTime;
        } else {
            Destroy(gameObject);
        }   
    }

    private void OnTriggerEnter2D(Collider2D hitCollider) {

        GameObject hitObject = hitCollider.transform.parent.gameObject;

        if ((isPlayerBullet && !hitObject.CompareTag("Player")) || (!isPlayerBullet && hitObject.CompareTag("Player"))) {

            if (hitColliders.Contains(hitCollider))
                return;

            CharacterStat characterStat = hitObject.GetComponent<CharacterStat>();

            if (characterStat != null) {
                hitColliders.Add(hitCollider);
                characterStat.ModifyHP(-damage);
                Destroy(gameObject);
            }
        }
    }
}
