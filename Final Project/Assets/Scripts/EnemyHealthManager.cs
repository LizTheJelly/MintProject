using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : HealthManager
{
    [SerializeField]
    private GameObject spawner;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip enemyDeathAudio;

    private Animator animControl;

    void Start()
    {
        animControl = GetComponent<Animator>();
        if (animControl != null)
        {
            animControl.SetBool("isDead", false);
        }
        numHealth = startingNumHealth;
    }
    public override void ApplyDamage(int numDamage)
    {
        numHealth -= numDamage;
        if (numHealth <= 0)
        {
            StartCoroutine(EnemyDeath());
        }
    }

    // EnemyDeath() destroys the enemy after playing enemy's death animation.
    // I grab the time of the enemy's death animation by grabbing all clips in Animator.
    // Code was inspired at "https://forum.unity.com/threads/how-to-find-animation-clip-length.465751/"
    private IEnumerator EnemyDeath()
    {
        if (animControl != null)
        {
            animControl.SetBool("isDead", true);
        }

        audioSource.PlayOneShot(enemyDeathAudio);

        float time = 0;
        AnimationClip[] clips = animControl.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "EnemyIdle":
                    time += clip.length;
                    break;
                case "EnemyDeath":
                    time += clip.length;
                    break;
            }
        }

        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
