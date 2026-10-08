using System.Collections.Generic;
using UnityEngine;

public class Cofre : MonoBehaviour
{
    [SerializeField] float vidaCofree;
    
    [Header("Mejoras posibles")]
    [SerializeField] private List<MejorasDatos> possibleUpgrades;

    [Header("Referencias")]
    [SerializeField] private UpgradeUI upgradeUI; // Arrastrar el objeto que tiene UpgradeUI.cs

    private bool isOpened = false;

    // Llamá a este método desde tu sistema de ataque (el del OverlapSphere)
    // o desde un OnTriggerEnter, según cómo rompas el cofre.
    public void OpenChest()
    {
        if (isOpened) return;
        isOpened = true;

        List<MejorasDatos> options = GetRandomUpgrades(3);
        upgradeUI.ShowOptions(options, this);
    }

    private List<MejorasDatos> GetRandomUpgrades(int amount)
    {
        List<MejorasDatos> pool = new List<MejorasDatos>(possibleUpgrades);
        List<MejorasDatos> result = new List<MejorasDatos>();

        amount = Mathf.Min(amount, pool.Count);

        for (int i = 0; i < amount; i++)
        {
            int randomIndex = Random.Range(0, pool.Count);
            result.Add(pool[randomIndex]);
            pool.RemoveAt(randomIndex); // Evita elegir la misma mejora dos veces
        }

        return result;
    }

    
    public void OnUpgradeChosen()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenChest();
        }
    }




    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }*/
}
