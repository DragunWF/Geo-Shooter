using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Attributes")]
    [SerializeField] float health = 50;
    [SerializeField] float moveSpeed = 0.5f;
    [SerializeField] bool isCircle = false;

    public float DamageEffectDuration { get; private set; }
    private float[] initialStats;

    private int minScoreGain = 25;
    private int maxScoreGain = 50;
    private int minExpGain = 1;
    private int maxExpGain = 2;

    private GameInfo gameInfo;
    private Player player;
    private FlashEffect flashEffect;

    private EnemySpawner enemySpawner;
    private ParticlesPlayer particlesPlayer;
    private AudioPlayer audioPlayer;

    private void Awake()
    {
        gameInfo = FindObjectOfType<GameInfo>();
        player = FindObjectOfType<Player>();

        enemySpawner = FindObjectOfType<EnemySpawner>();
        particlesPlayer = FindObjectOfType<ParticlesPlayer>();
        audioPlayer = FindObjectOfType<AudioPlayer>();

        flashEffect = GetComponent<FlashEffect>();
        DamageEffectDuration = 0.25f;

        initialStats = new float[2] { health, moveSpeed };
        SetStats();

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (gameInfo.FactionChosen == "RED")
            spriteRenderer.color = new Color32(107, 178, 238, 255);
        else
            spriteRenderer.color = new Color32(245, 75, 75, 255);
    }

    private void Update()
    {
        Vector2 playerPos = new Vector2(0, 0);
        float relativeSpeed = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, playerPos, relativeSpeed);

        if (!isCircle)
        {
            float rotationSpeed = 85.5f * Time.deltaTime;
            transform.Rotate(0, 0, rotationSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
            TakeDamage();
        if (other.tag == "Player")
            Death(true);
    }

    private void SetStats()
    {
        int difficultyLevel = enemySpawner.GetDifficultyLevel();
        if (difficultyLevel > 1)
        {
            float initialHealth = initialStats[0];
            float initialSpeed = initialStats[1];
            health = initialHealth + 12 * difficultyLevel;
            moveSpeed = initialSpeed + 0.025f * difficultyLevel;
        }
    }

    private void TakeDamage()
    {
        audioPlayer.PlayDamage();
        flashEffect.Flash();
        if (player != null)
        {
            health -= player.BulletDamage;
            if (health <= 0)
                Death(false);
        }
    }

    private void Death(bool deathThroughPlayerCollision)
    {
        particlesPlayer.PlayDeathEffect(transform.position);
        if (!deathThroughPlayerCollision)
        {
            int scoreGain = Random.Range(minScoreGain, maxScoreGain);
            int expGain = Random.Range(minExpGain, maxExpGain);

            player.GainExpPoints(expGain);
            gameInfo.IncreaseScore(scoreGain);

            Destroy(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
