using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private float maxHealth = 3;
    public float currentHealth { private get; set; }
    public bool isDead { get; private set; }

    private bool isAttacking;

    private NavMeshAgent _agent;
    private Animator _animator;

    private EnemiesManager _enemiesManager;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _enemiesManager = GetComponentInParent<EnemiesManager>();

        currentHealth = maxHealth;
    }

    private void Update()
    {
        Vector3 target = _enemiesManager.relic.transform.position;
        transform.LookAt(_enemiesManager.relic.transform.position);
        _agent.SetDestination(target);
        _animator.SetFloat("Speed", _agent.velocity.magnitude);

        float dist = _agent.remainingDistance;
        if (dist != Mathf.Infinity && _agent.remainingDistance > 0 && _agent.remainingDistance <= 1f && !isAttacking)
        {
            isAttacking = true;
            _animator.SetBool("Attack", true);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0 && !isDead) Die();
    }

    private void Die()
    {
        isDead = true;
        Destroy(this.gameObject);
    }
}
