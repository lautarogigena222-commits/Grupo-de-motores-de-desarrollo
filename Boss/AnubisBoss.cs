using NUnit.Framework;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AnubisBoss : MonoBehaviour
{
    //Rutina
    [SerializeField]int rutina;
    [SerializeField] float cronometro;
    [SerializeField] float time_Rutina;
    Quaternion angulo;
    float grado;
    GameObject target;
    public bool atacando;
    [SerializeField] RangoBoss rango;
    [SerializeField] float speed;
    public GameObject[] hit;
    public int hit_Select;

    //Animación
    public Animator animator;

    
    
    //Ataques
    //Lanzallamas/Veneno
    [SerializeField] bool lanza_Llamas;
    [SerializeField] List<GameObject> pool = new List<GameObject>();
    [SerializeField] GameObject fire;
    [SerializeField] GameObject cabezaDisparo;
    float cronometro2;

    //Salto
    [SerializeField] float jumpDistancia;
    bool direction_Skill;
    [SerializeField] float jumpSpeed;

    //Fases
    public int fase = 1;
    [SerializeField] float lifeMin;
    [SerializeField] float lifeMax;
    public UnityEngine.UI.Image barraVida;
    public AudioSource musica;
    [SerializeField] bool muerto;



    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player");


    }

    //comportamiento jefe
    public void ComportamientoBoss()
    {
        if(Vector3.Distance(transform.position, target.transform.position) < 15)
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            //point.transform.LookAt(target.transform.rotation);
            musica.enabled = true;

            if(Vector3.Distance(transform.position, target.transform.position) > 1 && !atacando)
            {
               switch(rutina)
               {
                    case 0: //caminar
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        animator.SetBool("Walk", true);
                        animator.SetBool("run", false);

                        if(transform.rotation == rotation)
                        {
                            transform.Translate(Vector3.forward * speed * Time.deltaTime);
                        }
                        animator.SetBool("atack", false);

                        cronometro += 1 * Time.deltaTime;
                        if(cronometro > time_Rutina)
                        {
                            rutina = Random.Range(0, 5);
                            cronometro = 0;
                        }
                        break;
                    case 1: //correr
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        animator.SetBool("Walk", false);
                        animator.SetBool("run", true);

                        if (transform.rotation == rotation)
                        {
                            transform.Translate(Vector3.forward * speed*2 * Time.deltaTime);
                        }
                        animator.SetBool("atack", false);
                        break;
                    case 2: //ataque veneno
                        animator.SetBool("Walk", false);
                        animator.SetBool("run", false);
                        animator.SetBool("attack", true);
                        animator.SetFloat("skills", 0);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        rango.GetComponent<CapsuleCollider>().enabled = false;
                        break;
                    case 3: //ataque salto
                        if(fase == 2)
                        {
                            jumpDistancia += 1 * Time.deltaTime;
                            animator.SetBool("Walk", false);
                            animator.SetBool("run", false);
                            animator.SetBool("attack", true);
                            animator.SetFloat("skills", 0);
                            hit_Select = 3;
                            rango.GetComponent<CapsuleCollider>().enabled = false;
                            
                            if(direction_Skill)
                            {
                                if(jumpDistancia < 1f)
                                {
                                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                                }
                                transform.Translate(Vector3.forward * 8 * Time.deltaTime);
                            }

                        }
                        else
                        {
                            rutina = 0;
                            cronometro = 0;
                        }
                        break;
               }
            }
        }
      

    }

    //animación
    public void FinalAnimation()
    {
        rutina = 0;
        atacando = false;
        rango.GetComponent<CapsuleCollider>().enabled = true;
        lanza_Llamas = false;
        jumpDistancia = 0;
        direction_Skill = false;
    }
    public void DirecciónAttackStart()
    {
        direction_Skill = true;
    }
    public void DirecciónAttackFinal()
    {
        direction_Skill = false;
    }


    //cuertoacuerpo
    public void ColliderWeaponTrue()
    {
        hit[hit_Select].GetComponent<BoxCollider>().enabled = true;
    }

    public void ColliderWeaponFalse()
    {
        hit[hit_Select].GetComponent<BoxCollider>().enabled = false;
    }


    //lanzallamas / veneno
    public GameObject GetBala()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }
        GameObject obj = Instantiate(fire,cabezaDisparo.transform.position,cabezaDisparo.transform.rotation) as GameObject;
        pool.Add(obj);
        return obj;
    }

    public void lanzallamas_Skill()
    {
        cronometro2 += 1 * Time.deltaTime;
        if (cronometro2 > 0.1f)
        {
            GameObject obj = GetBala();
            obj.transform.position = cabezaDisparo.transform.position;
            obj.transform.rotation = cabezaDisparo.transform.rotation;
            cronometro2 = 0;
        }

    }
    public void StartFire()
    {
        lanza_Llamas = true;
    }

    public void StopFire()
    {
        lanza_Llamas = false;
    }


    //esta vivo
    public void VIVO()
    {
        if(lifeMin < 500)
        {
            fase = 2;
            time_Rutina = 1;
        }

        ComportamientoBoss();
        if(lanza_Llamas)
        {
            lanzallamas_Skill();
        }
    }



    void Update()
    {
        barraVida.fillAmount = lifeMin / lifeMax;
        if(lifeMin > 0)
        {
            VIVO();
        }
        else
        {
            if (!muerto)
            {
                animator.SetTrigger("dead");
                musica.enabled = false;
                muerto = true;
            }
        }
    }
}
