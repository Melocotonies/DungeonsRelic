using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private float damage = 1;
    private float damageRate = 1f;

    private bool isEnemyOnTrap;

    private RaycastHit hit;

    private Enemy enemy;

    private void OnCollisionEnter(Collision collision)
    {
        enemy = collision.transform.GetComponent<Enemy>();
        if (enemy && !isEnemyOnTrap && !enemy.isDead)
        {
            isEnemyOnTrap = true;
            InvokeRepeating("DamageEnemy", 0, damageRate);
        }
        else
        {
            CancelInvoke();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        enemy = collision.transform.GetComponent<Enemy>();
        if (enemy)
        {
            CancelInvoke();
        }
    }

    private void DamageEnemy()
    {
        enemy.TakeDamage(damage);
    }
}
