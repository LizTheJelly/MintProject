using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageGiver : DamageGiver
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    float attackRadius;
    [SerializeField]
    float attackTime;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip playerAttackAudio;

    private Animator animControl;
    void Start()
    {
        animControl = GetComponent<Animator>();
        if (animControl != null)
        {
            animControl.SetBool("isAttacking", false);
        }
    }

    // Update is called once per frame
    // When user clicks the left mouse button, Update() simulates an attack motion and sound.
    // Then uses ray to detect if mouse click any gameObject. Depending on the gameobject's 
    // tag, it will damage the selected gameobject such as physical objects or enemies.
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(WaitForSeconds());
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            {
                Collider2D collision = hit.collider;
                Vector2 objLocation = collision.gameObject.transform.position;
                float distance = Vector2.Distance(player.transform.position, objLocation);
                if (distance < attackRadius)
                {
                    if (collision.gameObject.tag == "Physical Obj")
                    {
                        PhysicalObjHealthManager pObject = collision.gameObject.GetComponentInParent<PhysicalObjHealthManager>();
                        damagePhysicalObj(pObject);
                    }
                    if (collision.gameObject.tag == "Enemy")
                    {
                        EnemyHealthManager enemy = collision.gameObject.GetComponentInParent<EnemyHealthManager>();
                        damageEnemy(enemy);
                    }
                }
            }
        }
    }

    // Plays Player's attacking animation and stops the animation after animation ends.
    // If Pause Menu is currently open, it won't play attack sound.
    private IEnumerator WaitForSeconds()
    {
        if (animControl != null)
        {
            animControl.SetBool("isAttacking", true);
        }

        if (!GameStateManager.IsPauseActive)
        {
            audioSource.PlayOneShot(playerAttackAudio);
        }

        yield return new WaitForSeconds(attackTime);
        if (animControl != null)
        {
            animControl.SetBool("isAttacking", false);
        }
    }
}
