using UnityEngine;

public class Invoke : MonoBehaviour
{
    //Boss
    [SerializeField] GameObject Boss;
    [SerializeField] private GameObject bossHealthPanel;

    //Enemigos
    [SerializeField] private GameObject spawnEnemigos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            if (spawnEnemigos != null)
            {
                Destroy(spawnEnemigos);
            }

                Instantiate(Boss, transform.position + Vector3.up * 1f, Quaternion.identity);
            if (bossHealthPanel != null)
            {
                bossHealthPanel.SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
