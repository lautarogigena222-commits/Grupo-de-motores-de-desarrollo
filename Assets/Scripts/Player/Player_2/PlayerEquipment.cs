using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEquipment : MonoBehaviour
{
    [Header("Punto donde aparece el arma")]
    [SerializeField] private Transform weaponHolder;

    [Header("Inventario del jugador")]
    [SerializeField] private PlayerInventory playerInventory;

    private PlayerControls controls;

    private GameObject currentWeapon;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.QuickS1.performed += OnQuickSlot1;
        controls.Player.QuickS2.performed += OnQuickSlot2;
        controls.Player.QuickS3.performed += OnQuickSlot3;
        controls.Player.Unequip.performed += OnUnequip;
    }

    private void Start()
    {
        controls.Player.Enable();
    }

    private void OnQuickSlot1(InputAction.CallbackContext ctx)
    {
        Debug.Log("Quick Slot 1 - Ofensivo");
    }

    private void OnQuickSlot2(InputAction.CallbackContext ctx)
    {
        if (playerInventory == null)
            return;

        if (playerInventory.Weapon == null)
        {
            Debug.Log("No hay un arma en el slot 2.");
            return;
        }

        EquipWeapon(playerInventory.Weapon);
    }

    private void OnQuickSlot3(InputAction.CallbackContext ctx)
    {
        Debug.Log("Quick Slot 3 - Defensivo");
    }

    private void OnUnequip(InputAction.CallbackContext ctx)
    {
        UnequipWeapon();
    }

    public void EquipWeapon(ItemData item)
    {
        if (item == null)
            return;

        if (item.itemType != ItemType.Weapon)
            return;

        if (item.itemPrefab == null)
        {
            Debug.LogWarning("El objeto no tiene un prefab asignado.");
            return;
        }

        if (weaponHolder == null)
        {
            Debug.LogWarning("No hay un Weapon Holder asignado.");
            return;
        }

        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        currentWeapon = Instantiate(
            item.itemPrefab,
            weaponHolder
        );

        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
        currentWeapon.transform.localScale = Vector3.one;

        Debug.Log("Arma equipada: " + item.itemName);
    }

    private void UnequipWeapon()
    {
        if (currentWeapon == null)
        {
            Debug.Log("No hay ningún arma equipada.");
            return;
        }

        Destroy(currentWeapon);
        currentWeapon = null;

        Debug.Log("Arma desequipada. Manos libres.");
    }

    private void OnDisable()
    {
        if (controls != null)
        {
            controls.Player.Disable();
        }
    }

    private void OnDestroy()
    {
        if (controls != null)
        {
            controls.Player.QuickS1.performed -= OnQuickSlot1;
            controls.Player.QuickS2.performed -= OnQuickSlot2;
            controls.Player.QuickS3.performed -= OnQuickSlot3;
            controls.Player.Unequip.performed -= OnUnequip;

            controls.Dispose();
            controls = null;
        }
    }
}