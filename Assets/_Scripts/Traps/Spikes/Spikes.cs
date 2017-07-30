using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float damage { get; private set; }

    //private float damageRateTime = 3f;
    private float damageRateTime;
    private float maxDamageRateTime = 1f;
    
    private Enemy enemy;

    private Vector3 halfExtents;
    private Collider[] colliders;

    private void Awake()
    {
        damage = 1f;
        Vector3 trapSize = GetComponentInChildren<Renderer>().bounds.size;
        halfExtents = new Vector3(trapSize.x * .5f, trapSize.y, trapSize.z * .5f);
    }

    private void FixedUpdate()
    {
        colliders = Physics.OverlapBox(transform.position, halfExtents, Quaternion.identity, LayerMask.GetMask("Enemy"));
        if (colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                Enemy enemy = collider.transform.GetComponent<Enemy>();
                if (enemy && !enemy.isDead)
                {
                    damageRateTime += Time.deltaTime;
                    if (damageRateTime > maxDamageRateTime)
                    {
                        damageRateTime -= maxDamageRateTime;
                        enemy.TakeDamage(damage);
                    }
                }
            }
        }
    }
}
