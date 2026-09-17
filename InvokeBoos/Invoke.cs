using UnityEngine;

public class Invoke : MonoBehaviour
{
    [SerializeField] GameObject Boss;
    
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
            Instantiate(Boss, transform.position + Vector3.up * 5f, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
