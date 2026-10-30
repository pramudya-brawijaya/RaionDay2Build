using UnityEngine;
using System;
using UnityEngine.UI;

/* 

    Untuk memudahkan sistem health dan mengabung method-method yang akan dipakai
    setiap karakter, seperti method ketika mati, method untuk mengubah health, dll,
    aku akan menggabung semuanya dalam satu skrip ini.

    Untuk membedakan karakter player and musuh, sebaiknya compare tagnya aja.
    GameObject.CompareTag("Player");
    - pram 28/10/2026

*/


public class CharacterStat : MonoBehaviour
{
    /* Stats
    {get; private set;} membikin field tersebut bisa diakses oleh class lain (get)
    tetapi tidak bisa diganti isinya diluar class (private set). Ini disebut property 
    sebuah field.

    [field: SerializeField] membuat field tersebut muncul di inspector untuk diubah.
    Ketika menggunakan private dalam field, field tersebut tidak akan muncul dalam
    inspector jadi agak susah untuk dimodify ketika lagi playtest. Ketika field tersebut
    private, [SerializeField] bisa digunakan untuk membuat field tersebut muncul dalam
    inspector. Namun, [SerializeField] tidak bisa digunakan untuk sebuah field yang
    menggunakan property. Ini bisa diperbaiki dengan menggunakan [field: SerializeField].
    */

    [field: SerializeField] public int maxHP {get; private set;}
    [field: SerializeField] public int currentHP {get; private set;}
    private bool isPlayer;

    public void ModifyHP(int hpChange) {

        // Parameter hpChange akan ditambah dalam currentHP, jika ingin
        // mengurangi nyawa, hanya perlu menggunakan minus (-)
        // Contoh : Aku mau mengurangi nyawa player, jadi hanya memanggil
        // method ini dan memasuki damage dengan minus.
        // CharacterStat.ModifyHP(-2); 

        currentHP = Mathf.Clamp(currentHP + hpChange, 0, maxHP);

        // update hud jika player
        if (isPlayer)
            HUDManager.Instance.UpdateHealth(currentHP);

        if (currentHP <= 0) {
            if (isPlayer) {
                PlayerMovement.Instance.canMove = false;
                MenuManager.Instance.EnableGameOverMenu();
            } else {
               WaveManager.Instance.RegisterDeath(gameObject); 
            }

            Destroy(gameObject);
        }
    }

    private void Awake() {
        currentHP = maxHP;
        isPlayer = gameObject.CompareTag("Player"); // apakah ini player?
    }

}
