using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesPlayer : MonoBehaviour
{
    private GameObject deathEffect;
    private GameObject hitEffect;
    private const float effectDuration = 1.5f;

    public void PlayDeathEffect(Vector2 pos)
    {
        PlayParticleEffect(pos, deathEffect);
    }

    public void PlayHitEffect(Vector2 pos)
    {
        PlayParticleEffect(pos, hitEffect);
    }

    private void Awake()
    {
        deathEffect = Resources.Load("Prefabs/DeathEffect") as GameObject;
        hitEffect = Resources.Load("Prefabs/HitEffect") as GameObject;
    }

    private void PlayParticleEffect(Vector2 pos, GameObject effect)
    {
        GameObject instance = Instantiate(effect, pos, Quaternion.identity);
        Destroy(instance.gameObject, effectDuration);
    }
}
