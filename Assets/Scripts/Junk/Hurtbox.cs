using UnityEngine;

public class Hurtbox : MonoBehaviour
{

    public int damage;
    public float duration;
    public bool hurtOnlyPlayer;

    private float lastHit;

    private void OnTriggerEnter2D(Collider2D hitCollider) {

        if (hitCollider.transform.parent != null && hitCollider.transform.parent != transform.parent && Time.time >= lastHit && (!hurtOnlyPlayer || hitCollider.transform.parent.gameObject.CompareTag("Player"))) {
            CharacterStat stats = hitCollider.transform.parent.GetComponent<CharacterStat>(); 

            if (stats != null) {
                stats.ModifyHP(-damage);
                lastHit = Time.time + duration;
            }
        }
    }
}
