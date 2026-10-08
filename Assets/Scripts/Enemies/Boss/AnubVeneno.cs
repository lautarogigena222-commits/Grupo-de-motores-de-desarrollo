using UnityEngine;

public class AnubVeneno : MonoBehaviour
{
    float cronometro;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * 6 * Time.deltaTime);
        transform.localScale += new Vector3(3, 3, 3) * Time.deltaTime;

        cronometro+=1 * Time.deltaTime;
        if(cronometro > 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
            gameObject.SetActive(false);
            cronometro = 0;
                    
        }
    }
}
