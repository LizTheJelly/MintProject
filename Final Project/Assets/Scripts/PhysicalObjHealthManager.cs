using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalObjHealthManager : HealthManager
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip hitAudio;
    public override void ApplyDamage(int numDamage)
    {
        audioSource.PlayOneShot(hitAudio);
        numHealth -= numDamage;
        if (numHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
