using UnityEngine;

public class Drops : MonoBehaviour
{
    //Monedas
    [SerializeField] GameObject coins;
    

    //Comida
    [SerializeField] GameObject comida;
    [SerializeField] float healFood;
                     public float HealFood => healFood;
    Animation animacion;
    [SerializeField] int cantidadDrop;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
       
    }
}
