using UnityEngine;

[CreateAssetMenu(fileName = "MejorasDatos", menuName = "Cofre/Upgrade")]
public class MejorasDatos : ScriptableObject
{
    public enum UpgradeType
    {
        AumentarVida,
        AumentarDaño,
        AumentarVelocidad,
        AutoDisparo,
        AumentarDañoDisparo,
        AumentarVelocidadDisparo,
        CurarInstantaneo,
    }

    //info panel
    public string upgradeName;
    [TextArea] public string description;
    public Sprite icon;

   
    
    [Header("Efecto")]
    public UpgradeType type;
    public float value;
}
