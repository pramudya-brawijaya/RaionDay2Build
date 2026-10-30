using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int attackDamage = 10;

    private Transform playerTransform;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hanya serang jika menabrak Player
        if (collision.CompareTag("Player"))
        {
            CharacterStat playerStat = collision.GetComponent<CharacterStat>();

            if (playerStat != null)
            {
                try
                {
                    playerStat.ModifyHP(-attackDamage);
                    Debug.Log("Musuh menyerang Player! HP berkurang " + attackDamage);
                }
                catch (System.NullReferenceException)
                {
                    Debug.LogWarning("Player terkena serangan, tapi HUDManager belum ada di scene ini.");
                }
            }
        }
    }
}