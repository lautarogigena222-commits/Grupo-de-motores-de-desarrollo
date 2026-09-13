using UnityEngine;

public class force_ball : MonoBehaviour
{
    public GameObject Spawn_ball;
    public float force_shot = 100f;
    private Rigidbody2D rb;


    public static int puntuacion = 0;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * force_shot, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("destroyable"))
        {
            Destroy(collision.gameObject);
            puntuacion += 1;
            Debug.Log("Puntos totales: " + puntuacion);
        }
        if (collision.gameObject.CompareTag("KillZone"))
        {
            Destroy(gameObject);
        }
    }
}
