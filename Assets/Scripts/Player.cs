using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int BulletDamage { get; private set; }
    public float DamageEffectDuration { get; private set; }

    private int healthPoints = 1;
    private int expPoints;
    private int expToLevelUp;
    private const int maxLevel = 10;
    private int level;

    private float reloadTime = 1.25f;
    private bool isReloading = false;
    private const float bulletForce = 22.5f;
    private float[] initialStats;

    private Camera mainCamera;
    private Rigidbody2D rigidBody;
    private Vector2 mousePos;

    private Transform firePoint;
    private GameObject bulletPrefab;
    private GameUI gameUI;
    private FlashEffect flashEffect;

    public int GetExperiencePoints() { return expPoints; }
    public int GetMaxLevel() { return maxLevel; }
    public Vector2 GetPosition() { return transform.position; }

    public void GainExpPoints(int gainAmount)
    {
        expPoints += gainAmount;
        if (expPoints >= expToLevelUp && level < maxLevel)
            LevelUp();
        gameUI.UpdateLevelSlider(expPoints, expToLevelUp);
    }

    private void Awake()
    {
        DamageEffectDuration = 1.5f;
        BulletDamage = 10;

        bulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;
        firePoint = GameObject.Find("FirePoint 1").transform;

        rigidBody = GetComponent<Rigidbody2D>();
        flashEffect = GetComponent<FlashEffect>();

        mainCamera = FindObjectOfType<Camera>();
        gameUI = FindObjectOfType<GameUI>();
    }

    private void Start()
    {
        initialStats = new float[2] { BulletDamage, reloadTime };
        LevelUp();
        UpdateStats();
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

    private void UpdateStats()
    {
        int initialBulletDamage = (int)initialStats[0];
        float initialReloadTime = initialStats[1];

        reloadTime = initialReloadTime - 0.11f * level;
        BulletDamage = initialBulletDamage + 7 * level;
    }

    private void LevelUp()
    {
        int nextLevel = level + 1;
        int baseNum = expToLevelUp;

        while (level != nextLevel)
        {
            baseNum++;
            nextLevel = (int)Mathf.Floor(Mathf.Log(baseNum, 2));
        }

        level++;
        expToLevelUp = baseNum;
        UpdateStats();

        gameUI.UpdateLevelText(level);
        gameUI.UpdateLevelSlider(expPoints, expToLevelUp);

        if (level > 1)
        {
            healthPoints++;
            gameUI.UpdateHealthText(healthPoints);
        }
    }

    private void TakeDamage()
    {
        healthPoints -= 1;
        gameUI.UpdateHealthText(healthPoints);
        flashEffect.Flash();
        if (healthPoints <= 0)
            Death();
    }

    private void Death()
    {
        // FindObjectOfType<FadeToBlack>().InitializeFade();
    }
}
