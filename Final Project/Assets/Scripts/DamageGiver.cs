using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageGiver : MonoBehaviour
{
    [SerializeField]
    protected int damagePower;

    public void damageEnemy(EnemyHealthManager enemy)
    {
        enemy.ApplyDamage(damagePower);
    }

    public void damagePhysicalObj(PhysicalObjHealthManager obj)
    {
        obj.ApplyDamage(damagePower);
    }

    public void damagePlayer(PlayerLivesHealthManager player)
    {
        player.ApplyDamage(damagePower);
    }
}
