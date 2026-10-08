using UnityEngine;

public class Spawn : MonoBehaviour
{
    //Spawn
    [SerializeField] float timeBetweenSpawn;
    private float spawnTime;

  


    //Enemigos
    [SerializeField] GameObject enemy;
    [SerializeField] private int enemyNumber;
    [SerializeField] private int maxEnemies;
    [SerializeField] bool spawnEnemies = false;

    //Animales
    [SerializeField] GameObject animal;
    [SerializeField] private int animalNumber;
    [SerializeField] private int maxAnimals;
    [SerializeField] bool spawnAnimals = false;

    //Collider
 

    void Start()
    {
        
    }

    void Update()
    {
        
        if(enemyNumber < maxEnemies)
        {
            SpawnEnemy();
        }
        if(animalNumber < maxAnimals)
        {
            SpawnAnimal();
        }

           


    }

    private void SpawnEnemy()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime > timeBetweenSpawn)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);       

            spawnTime = 0;
            enemyNumber++;
        }
    }

    private void SpawnAnimal()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime > timeBetweenSpawn)
        {
            Instantiate(animal, transform.position, Quaternion.identity);
            spawnTime = 0;
            animalNumber++;
        }
    } 

       



}
