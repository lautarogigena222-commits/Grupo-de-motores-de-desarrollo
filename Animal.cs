using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] int rutina;
    [SerializeField] float cronometro;
    public Quaternion angulo;
    [SerializeField] float grado;

    [SerializeField] float animalLife;

    [SerializeField] GameObject comida;
    [SerializeField] int drop;

    void Start()
    {
        
    }

    public void Comportamiento_Animal()
    {
        cronometro += 1 * Time.deltaTime;
        if(cronometro >= 2)
        {
            rutina = Random.Range(0, 2);
            cronometro = 0;
        }
        switch(rutina)
        {
            case 0:
                //Animacion de caminar
                break;
            case 1:
                grado = Random.Range(0, 360);
                angulo = Quaternion.Euler(0, grado, 0);
                rutina++;
                break;
            case 2:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                //Animacion de caminar
                transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                break;
        }
    }

    void Update()
    {
        Comportamiento_Animal();
        DropComida();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animalLife -= other.GetComponent<Player>().PlayerDamage;
        }
        if (animalLife <= 0)
        {
            Destroy(gameObject);
            DropComida();
        }
    }

    public void DropComida()
    {
        if (animalLife <= 0)
        {
            drop = UnityEngine.Random.Range(0, 2);
            switch (drop)
            {
                case 0:
                    break;
                case 1:
                    Instantiate(comida, transform.position, transform.rotation);
                    break;
                case 2:
                    break;
            }

        }



    }
}
