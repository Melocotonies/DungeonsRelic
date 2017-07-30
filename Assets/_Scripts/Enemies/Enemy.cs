using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float damage;

    [SerializeField] private float maxHealth;
    public float _maxHealth
    {
        get
        {
            return maxHealth;
        }
    }
    public float currentHealth { get; private set; }
    public bool isDead { get; private set; }

    private bool isAttacking;
    private float attackRate = 1f;

    public bool isOnSpikesTrap { get; set; }

    private NavMeshAgent _agent;
    private Animator _animator;

    private EnemiesManager _enemiesManager;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _enemiesManager = GetComponentInParent<EnemiesManager>();
        
        currentHealth = _maxHealth;
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
            InvokeRepeating("Attack", 0f, attackRate);
        }
    }

    public void Attack()
    {
        _enemiesManager.relic.TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !isDead) Die();
    }

    private void Die()
    {
        isDead = true;
        Destroy(this.gameObject);
    }
}
