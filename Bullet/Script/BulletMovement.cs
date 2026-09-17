using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;

    private Transform target;
    [SerializeField] private float damage;
                     public float Damage => damage;

    public void SetTarget(Transform newTarget, float projectileDamage)
    {
        target = newTarget;
        damage = projectileDamage;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.LookAt(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
           Destroy(gameObject);
        }
    }
}
