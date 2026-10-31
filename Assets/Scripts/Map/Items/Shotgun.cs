using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Shotgun : Item
{

    [field: SerializeField] public GameObject bulletPrefab {get; private set;}
    [field: SerializeField] public Transform bulletTransform {get; private set;}
    [field: SerializeField] public float fireRate {get; private set;}
    [field: SerializeField] public int ammoPerUse {get; private set;}
    public float spreadAngle;
    public int bulletCount;

    private bool equipped;
    private SpriteRenderer spriteRenderer;
    private InputManager inputManager;
    private Coroutine shootingCoroutine;
    private float nextFireTime;

    public override void OnEquip() {
        StartCoroutine(RotateGun());
    }

    private IEnumerator RotateGun() {
        while (PlayerInventory.Instance.equippedItem == this)
        {
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 direction = worldMousePosition - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);
            spriteRenderer.flipY = angle > 90 || angle < -90;

            yield return null;
        }
    }

    private IEnumerator ShootGun() {
        while (inputManager.UseItem.IsPressed() && PlayerInventory.Instance.equippedItem == this && PlayerInventory.Instance.ammoCount >= ammoPerUse) {
            // fixing bullet not alligning with sprite when flipped horizontally
            Vector3 bulletPosition = bulletTransform.position;

            if (spriteRenderer.flipY)
            {
                Transform spriteTransform = spriteRenderer.transform;
                Vector3 localPosition = spriteTransform.InverseTransformPoint(bulletPosition);
                localPosition.y *= -1;
                bulletPosition = spriteTransform.TransformPoint(localPosition);
            }

            for (int i = 0; i < bulletCount; i++)
            {
                float angle = -spreadAngle / 2f + (spreadAngle / (bulletCount - 1)) * i;

                Quaternion bulletRotation = bulletTransform.rotation * Quaternion.Euler(0, 0, angle);

                Instantiate(bulletPrefab, bulletPosition, bulletRotation, GameObject.Find("Junk").transform);
            }

            nextFireTime = Time.time + fireRate;
            PlayerInventory.Instance.ammoCount -= ammoPerUse;
            HUDManager.Instance.UpdateAmmo();

            yield return new WaitForSeconds(fireRate);
        }

        if (PlayerInventory.Instance.ammoCount < ammoPerUse) {
            NotificationManager.Instance.Create(
                "Not enough ammo!",
                Vector3.zero,
                5,
                PlayerMovement.Instance.transform
                );
        }

        shootingCoroutine = null;
    }


    protected override void Awake() {
        base.Awake();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        inputManager = InputManager.Instance;
    }

    public override void OnUse() {
        if (shootingCoroutine == null && Time.time >= nextFireTime)
            shootingCoroutine = StartCoroutine(ShootGun());
    }
}