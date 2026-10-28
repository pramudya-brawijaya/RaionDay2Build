using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D hitCollider) {

        CharacterStat stats = hitCollider.transform.parent.GetComponent<CharacterStat>(); 

        if (stats != null) {
            stats.ModifyHP(-2);
        }
    }
}
