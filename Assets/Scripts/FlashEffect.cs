using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    [Tooltip("Flash Type")]
    [SerializeField] bool isUsingPlayer;

    private SpriteRenderer spriteRenderer;
    private Color flashColor = new Color(1, 1, 1, 1);
    private Color originalColor;

    private Coroutine flashRoutine;
    private float effectDuration;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        effectDuration = isUsingPlayer ?
                         GetComponent<Player>().DamageEffectDuration :
                         GetComponent<Enemy>().DamageEffectDuration;
        originalColor = spriteRenderer.color;
    }

    public void Flash()
    {
        flashRoutine = StartCoroutine(StartFlashEffect());
        Invoke("StopFlash", effectDuration);
    }

    private void StopFlash()
    {
        StopCoroutine(flashRoutine);
        spriteRenderer.color = originalColor;
    }

    private IEnumerator StartFlashEffect()
    {
        const float flashDuration = 0.25f;

        while (true)
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);

            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
    }
}
