using UnityEngine;

public class TurretTop : MonoBehaviour
{
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject turretProjectile;

    private float shootRateTime;
    private float maxShootRateTime = 1f;

    public bool enemyFound { private get; set; }

    private void Update()
    {
        if (!enemyFound) return;
        
        shootRateTime += Time.deltaTime;
        if (shootRateTime > maxShootRateTime)
        {
            shootRateTime -= maxShootRateTime;
            Instantiate(turretProjectile, shootingPoint.position, shootingPoint.rotation, transform);
        }
    }

    public void Shoot(Enemy enemy)
    {
        Vector3 enemyPosition = enemy.transform.position;
        transform.LookAt(enemyPosition);
    }
}
