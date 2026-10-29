using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Fist : Item
{
    public override void OnUse() {

        PlayerMovement plr = PlayerMovement.Instance;

        plr.canMove = false;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(
            Mouse.current.position.ReadValue()
        );
        Vector2 direction = (mousePosition - (Vector2) plr.transform.position).normalized;

        plr.rb.linearVelocity = direction * 10;

        plr.StartCoroutine(EnableMovement());
        // itemdata is a class with no monobehaviour, corountines can't be run.
    }

    private IEnumerator EnableMovement()
    {
        yield return new WaitForSeconds(0.5f);

        PlayerMovement.Instance.canMove = true;
    }
}
