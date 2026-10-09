using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] private float detectionRange;

    public GameObject target;

    //rutina
    [SerializeField] int rutina;
    [SerializeField] float cronometro;
    public Quaternion angulo;
    [SerializeField] float grado;


    //Daño
    [SerializeField] private float damage = 3;
                     public float Damge => damage;

    [SerializeField] private float enemyLife;

    //Drop
    [SerializeField] private GameObject moneda;
    [SerializeField] int drop;
    [SerializeField] float expEnemy;
              public float ExpEnemy => expEnemy;
    private float expPlayer;




    void Start()
    {
       
            target = GameObject.FindGameObjectWithTag("Player");
        
    }

   
    void Update()
    {
        Comportamiento_Enemy();

        float distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance < detectionRange)
        {
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0f;

            //if (direction.sqrMagnitude > 0.001f)
            
               
                Quaternion lookRotation = Quaternion.LookRotation(direction.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10f * Time.deltaTime);
            

            Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

           

        }
    }

      
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyLife -= collision.gameObject.GetComponent<Player>().PlayerDamage;
            CheckDeath();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            enemyLife -= other.GetComponent<Projectile>().Damage;
            CheckDeath();
        }
    }

    private void CheckDeath()
    {
        if (enemyLife <= 0)
        {
            if (target != null)
            {
                Player player = target.GetComponent<Player>();
                if (player != null)
                {
                    player.MasExp(expEnemy);
                }
            }

            DropMoneda();
            Destroy(gameObject);
        }
    }

    public void DropMoneda()
    {
        if (enemyLife <= 0)
        {
            drop = UnityEngine.Random.Range(0, 2);
            switch (drop)
            {
                case 0:
                    break;
                case 1:
                    Instantiate(moneda, transform.position, transform.rotation);
                    break;
                case 2:
                    break;
            }

        }
       

     
    }

    public void Comportamiento_Enemy()
    {
        cronometro += 1 * Time.deltaTime;
        if (cronometro >= 2)
        {
            rutina = UnityEngine.Random.Range(0, 1);
            cronometro = 0;
        }
        switch (rutina)
        {
            case 0:
                grado = UnityEngine.Random.Range(0, 360);
                angulo = Quaternion.Euler(0, grado, 0);
                rutina++;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                //Anim de caminar
                transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                break;
            case 1:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                //Anim de caminar
                transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                break;
        }
    }

}
