using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UpgradeUI : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject panel; // El panel completo, desactivado por defecto

    [Header("Botones (asignar los 3 en orden)")]
    [SerializeField] private Button[] optionButtons;
    [SerializeField] private TextMeshProUGUI[] optionNames;
    [SerializeField] private TextMeshProUGUI[] optionDescriptions;
    [SerializeField] private Image[] optionIcons;

    [Header("Referencia al jugador")]
    [SerializeField] private Player player; // Arrastrar el jugador acá (el que tiene Player.cs)

    private Cofre currentCofre;

    void Awake()
    {
        panel.SetActive(false);
    }

    public void ShowOptions(List<MejorasDatos> options, Cofre cofre)
    {
        currentCofre = cofre;
        panel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < options.Count)
            {
                MejorasDatos data = options[i];

                optionButtons[i].gameObject.SetActive(true);
                optionNames[i].text = data.upgradeName;
                optionDescriptions[i].text = data.description;
                optionIcons[i].sprite = data.icon;

                
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => SelectUpgrade(data));
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void SelectUpgrade(MejorasDatos data)
    {
        player.ApplyUpgrade(data);

        panel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentCofre.OnUpgradeChosen();
    }
}