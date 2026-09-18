using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movimiento : MonoBehaviour
{
    public float speed_Rotation = 100f;
    public GameObject ballfire;
    public GameObject Point_Fire;
    public int NumBalls = 25;

    public bool ganaste = false;
    public bool perdiste = false;


    public int puntuacionmin = 18;

    public Animator anim;
    public Animator anim2;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim2.SetBool("inmove", true);
            transform.Rotate(0,0,-speed_Rotation * Time.deltaTime);
        }
       
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            anim2.SetBool("inmove", true);
            transform.Rotate(0,0,speed_Rotation * Time.deltaTime);
        }


        if (Input.GetKeyDown(KeyCode.Space)&&(NumBalls>0))
        {
            Instantiate(ballfire, Point_Fire.transform.position, transform.rotation);
            NumBalls--;
        }

  
        if (force_ball.puntuacion >= puntuacionmin && !ganaste)
        {
            ganaste = true;
            SceneManager.LoadScene("victory");

        }
        if (NumBalls <= 0 && force_ball.puntuacion < puntuacionmin && !perdiste && !ganaste)
        {
                perdiste = true;
            anim.SetBool("lose", true);
            SceneManager.LoadScene("lose");
        }

       
    }
}
