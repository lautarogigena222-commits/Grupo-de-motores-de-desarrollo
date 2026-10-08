using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Vida
    [SerializeField] private float life;
                    public float Life => life;
    [SerializeField] private float lifeMax;
    [SerializeField] private UnityEngine.UI.Image vida;

    
    //Daño
    [SerializeField] private int playerDamage;
    public int PlayerDamage => playerDamage;
    //[SerializeField] private LayerMask enemys;
    [SerializeField] private float attackRange;

    //puntos
    [SerializeField] int money;
    [SerializeField] private TextMeshProUGUI coins;

    //Exp
    [SerializeField] float exp;
    [SerializeField] float expMax;
    [SerializeField] private UnityEngine.UI.Image experiencia;
    [SerializeField] float nivel;
    [SerializeField] float nivelMaximo;
    [SerializeField] private TextMeshProUGUI nivelTexto;

    //Mejora
    [SerializeField] private AutoShooter autoShooter;



    void Start()
    {
        coins.text = "COINS: " + money.ToString();
        nivelTexto.text = "NIVEL: " + nivel.ToString();
    }

    
    void Update()
    {
        coins.text = "COINS: " + money.ToString();
        if (exp >= expMax)
        {
            nivel++;
            exp = 0;
            expMax += 10;
        }
        nivelTexto.text = "NIVEL: " + nivel.ToString();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            life -= collision.gameObject.GetComponent<Enemy>().Damge;
            exp += collision.gameObject.GetComponent<Enemy>().ExpEnemy;
            vida.fillAmount = (float)life / lifeMax;
            experiencia.fillAmount = (float)exp / expMax;
        }
        if (collision.gameObject.CompareTag("Animal"))
        {
            Destroy(collision.gameObject);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coins"))
        {
            money++;
            coins.text = "COINS: " + money.ToString();

        }
        if (other.gameObject.CompareTag("Health"))
        {
            life += other.gameObject.GetComponent<Drops>().HealFood;
            vida.fillAmount = (float)life / lifeMax;
            if (life > lifeMax)
            {
                life = lifeMax;
            }
        }
    }

    public void MasExp(float amount)
    {
        exp += amount;
        experiencia.fillAmount = (float)exp / expMax;
    }


    public void TakeDamage(float amount)
    {
        life -= amount;
        vida.fillAmount = (float)life / lifeMax;
    }



    public void ApplyUpgrade(MejorasDatos data)
    {
        switch (data.type)
        {
            case MejorasDatos.UpgradeType.AumentarVida:
                lifeMax += data.value;
                life += data.value;
                vida.fillAmount = (float)life / lifeMax;
                break;

            case MejorasDatos.UpgradeType.AumentarDaño:
                playerDamage += (int)data.value;
                break;

            case MejorasDatos.UpgradeType.CurarInstantaneo:
                life = Mathf.Min(life + data.value, lifeMax);
                vida.fillAmount = (float)life / lifeMax;
                break;
            case MejorasDatos.UpgradeType.AutoDisparo:
                if (autoShooter != null)
                {
                    autoShooter.EnableAutoShoot();
                }
                break;

            /*case MejorasDatos.UpgradeType.AumentarDañoDisparo:
                if (autoShooter != null)
                {
                    autoShooter.IncreaseDamage(data.value);
                }
                break;

            case MejorasDatos.UpgradeType.AumentarVelocidadDisparo:
                if (autoShooter != null)
                {
                    autoShooter.IncreaseFireRate(data.value);
                }
                break;*/
                /*case MejorasDatos.UpgradeType.AumentarVelocidad:
                    if (playerController != null)
                    {
                        playerController.IncreaseSpeed(data.value);
                    }
                    break;*/
        }

        
    }

}