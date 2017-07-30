using UnityEngine;

public class Turret : MonoBehaviour
{
    private Collider[] colliders;
    private float radius = 5f;

    private TurretTop _turretTop;
    private Enemy enemy;

    private void Awake()
    {
        _turretTop = GetComponentInChildren<TurretTop>();
    }

    private void Update()
    {
        colliders = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Enemy"));
        if(colliders.Length > 0)
        {
            Enemy enemy = colliders[0].transform.GetComponent<Enemy>();
            if (enemy)
            {
                _turretTop.enemyFound = true;
                _turretTop.Shoot(enemy);
            }
        }
        else
        {
            _turretTop.enemyFound = false;
        }
    }
}
