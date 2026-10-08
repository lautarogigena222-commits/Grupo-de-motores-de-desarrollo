using Unity.VisualScripting;
using UnityEngine;

public class RangoBoss : MonoBehaviour
{
    public Animator anim;
    public AnubisBoss anubisBoss;
    public int meleeDamage;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            meleeDamage = Random.Range(0, 4);
            switch(meleeDamage)
            {
                case 0:
                    //golpe1
                    anim.SetFloat("Skills", 0);
                    anubisBoss.hit_Select = 0;
                    break;
                case 1:
                    //golpe2
                    anim.SetFloat("Skills", 0);
                    anubisBoss.hit_Select = 1;
                    break;
                case 2:
                    //golpe3
                    anim.SetFloat("Skills", 0);
                    anubisBoss.hit_Select = 2;
                    break;
                case 3:
                    //lanzallamas / Veneno
                    if(anubisBoss.fase == 2)
                    {
                        anim.SetFloat("Skills", 0);
                    }
                    else
                    {
                        meleeDamage = 0;
                    }
                    break;

            }
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            anim.SetBool("attack", true);
            anubisBoss.atacando = true;
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
