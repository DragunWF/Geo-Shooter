using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioClip click;
    private const float clickVolume = 2.25f;

    private AudioClip damage;
    private const float damageVolume = 0.75f;

    private AudioClip shoot;
    private const float shootVolume = 0.6f;

    private AudioClip upgrade;
    private const float upgradeVolume = 1.25f;

    public void PlayClick() { PlayClip(click, clickVolume); }
    public void PlayDamage() { PlayClip(damage, damageVolume); }
    public void PlayShoot() { PlayClip(shoot, shootVolume); }
    public void PlayUpgrade() { PlayClip(upgrade, upgradeVolume); }

    private void Awake()
    {
        click = Resources.Load("Audio/Click") as AudioClip;
        damage = Resources.Load("Audio/Damage") as AudioClip;
        shoot = Resources.Load("Audio/Shoot") as AudioClip;
        upgrade = Resources.Load("Audio/Upgrade") as AudioClip;
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector2 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
