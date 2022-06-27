using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float BulletDamage { get; private set; }

    private int healthPoints = 5;
    private int expPoints;
    private int expToLevelUp;
    private const int maxLevel = 10;
    private int level;

    private float reloadTime = 0.75f;
    private bool isReloading = false;
    private const float bulletForce = 22.5f;

    private Camera mainCamera;
    private Rigidbody2D rigidBody;
    private Vector2 mousePos;

    private Transform firePoint;
    private GameObject bulletPrefab;
    private GameUI gameUI;

    public int GetExperiencePoints() { return expPoints; }
    public int GetMaxLevel() { return maxLevel; }
    public Vector2 GetPosition() { return transform.position; }

    public void GainExpPoints(int gainAmount)
    {
        expPoints += gainAmount;
        if (expPoints >= expToLevelUp)
            LevelUp();
        gameUI.UpdateLevelSlider(expPoints, expToLevelUp);
    }

    private void Awake()
    {
        BulletDamage = 25f;

        bulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;
        firePoint = GameObject.Find("FirePoint 1").transform;

        rigidBody = GetComponent<Rigidbody2D>();
        mainCamera = FindObjectOfType<Camera>();
        gameUI = FindObjectOfType<GameUI>();
    }

    private void Start()
    {
        LevelUp();
        gameUI.UpdateHealthText(healthPoints);
        gameUI.UpdateLevelText(level);
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
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }

    private void LevelUp()
    {
        int nextLevel = level + 1;
        int baseNum = expToLevelUp;

        while (nextLevel != level)
        {
            baseNum++;
            nextLevel = (int)Mathf.Floor(Mathf.Log(baseNum, 2));
        }

        level++;
        expToLevelUp = baseNum;
        Debug.Log(string.Format("Exp: {0}, Until next lvl: {1}", expPoints, expToLevelUp));
        gameUI.UpdateLevelText(level);
        gameUI.UpdateLevelSlider(expPoints, expToLevelUp);
    }

    private void TakeDamage()
    {
        healthPoints -= 1;
        gameUI.UpdateHealthText(healthPoints);
        if (healthPoints <= 0)
            Death();
    }

    private void Death()
    {

    }
}
