using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    private PlayerControls controls;

    // Indica si el jugador está realizando un ataque
    public bool IsAttacking { get; private set; }

    // Duración aproximada del ataque
    [SerializeField] private float attackDuration = 1.36f;

    private void Start()
    {
        animator = GetComponent<Animator>();

        controls = new PlayerControls();

        controls.Player.Attack.performed += OnAttack;

        // Activamos solamente el Action Map del jugador
        controls.Player.Enable();
    }

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        // Si el inventario está abierto y el cursor está libre,
        // no permitimos atacar con el clic izquierdo.
        if (Cursor.lockState != CursorLockMode.Locked)
            return;

        Attack();
    }

    private void Attack()
    {
        // Evita iniciar otro ataque mientras el actual está en curso
        if (IsAttacking)
            return;

        IsAttacking = true;

        Debug.Log("ATAQUE");

        animator.SetTrigger("Attack");

        Invoke(nameof(EndAttack), attackDuration);
    }

    private void EndAttack()
    {
        IsAttacking = false;
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
            controls.Player.Attack.performed -= OnAttack;

            controls.Dispose();
            controls = null;
        }

        CancelInvoke();
    }
}