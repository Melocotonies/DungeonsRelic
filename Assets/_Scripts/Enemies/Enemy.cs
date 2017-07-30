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
    
    private float attackRateTime;
    private float maxAttackRateTime = 1f;

    public bool isOnSpikesTrap { get; set; }

    private NavMeshAgent _agent;
    private Animator _animator;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        
        currentHealth = _maxHealth;
    }

    private void Update()
    {
        Vector3 target = GameManager.relic.transform.position;
        transform.LookAt(GameManager.relic.transform.position);
        _agent.SetDestination(target);
        _animator.SetFloat("Speed", _agent.velocity.magnitude);

        float dist = _agent.remainingDistance;
        if (dist != Mathf.Infinity && _agent.remainingDistance > 0 && _agent.remainingDistance <= 1f)
        {
            _animator.SetBool("Attack", true);

            attackRateTime += Time.deltaTime;
            if (attackRateTime > maxAttackRateTime)
            {
                attackRateTime -= maxAttackRateTime;
                Attack();
            }
        }
    }

    public void Attack()
    {
        GameManager.relic.TakeDamage(damage);
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
