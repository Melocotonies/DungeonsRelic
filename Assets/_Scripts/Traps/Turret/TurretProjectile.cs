using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    private float shootForce = 20f;

    private Rigidbody _rigidbody;

    private Turret turret;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        turret = GetComponentInParent<Turret>();
    }

    private void Update()
    {
        _rigidbody.AddForce(transform.forward * shootForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
        Enemy enemy = collision.transform.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.TakeDamage(turret.damage);
        }
    }
}
