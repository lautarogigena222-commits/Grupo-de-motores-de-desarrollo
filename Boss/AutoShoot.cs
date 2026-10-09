using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    
    [SerializeField] private bool hasAutoShoot = false;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 5f;

    
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private float timer;

    void Update()
    {
        if (!hasAutoShoot) return;

        timer += Time.deltaTime;
        if (timer >= timeBetweenShots)
        {
            Transform closestEnemy = FindClosestEnemy();
            if (closestEnemy != null)
            {
                Shoot(closestEnemy);
                timer = 0f;
            }
        }
    }

    private Transform FindClosestEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range);
        Transform closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = hit.transform;
                }
            }
        }

        return closest;
    }

    private void Shoot(Transform target)
    {
        Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position;
        GameObject projectileObj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);

        Projectile projectile = projectileObj.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.SetTarget(target, damage);
        }
    }

    

    public void EnableAutoShoot()
    {
        hasAutoShoot = true;
    }

    /*public void IncreaseDamage(float amount)
    {
        damage += amount;
    }

    public void IncreaseFireRate(float amount)
    {
        timeBetweenShots = Mathf.Max(0.1f, timeBetweenShots - amount);
    }*/
}
