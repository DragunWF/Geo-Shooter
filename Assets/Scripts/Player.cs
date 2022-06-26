using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float BulletDamage { get; private set; }

    private Camera mainCamera;
    private Rigidbody2D rigidBody;
    private Vector2 mousePos;

    private Transform firePoint;
    private GameObject bulletPrefab;
    private const float bulletForce = 22.5f;

    private float reloadTime = 0.75f;
    private bool isReloading = false;

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    private void Awake()
    {
        BulletDamage = 25f;

        rigidBody = GetComponent<Rigidbody2D>();
        mainCamera = FindObjectOfType<Camera>();
        bulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;
        firePoint = GameObject.Find("FirePoint 1").transform;
    }

    private void Update()
    {
        AimTowardsMouse();
        Shooting();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
            TakeDamage();
    }

    private void AimTowardsMouse()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePos - rigidBody.position;

        const float rotationOffset = 90;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - rotationOffset;

        rigidBody.rotation = angle;
    }

    private void Shooting()
    {
        if (Input.GetButton("Fire1") && !isReloading)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D bulletRigidBody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidBody.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            StartCoroutine(Reloading());
        }
    }

    private IEnumerator Reloading()
    {
        isReloading = true;
        yield return new WaitForSecondsRealtime(reloadTime);
        isReloading = false;
    }

    private void TakeDamage()
    {

    }
}
