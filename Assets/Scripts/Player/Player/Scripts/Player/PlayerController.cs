using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls controls;
    private CharacterController characterController;
    private Animator animator;

    private Vector2 moveInput;

    private Vector3 movementForward;
    private Vector3 movementRight;

    private float verticalVelocity;

    private bool wasFreeLook;
    private bool wasGrounded;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float rotationSpeed = 10f;

    void Start()
    {
        controls = new PlayerControls();
        controls.Enable();

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        controls.Player.Move.performed += ctx =>
            moveInput = ctx.ReadValue<Vector2>();

        controls.Player.Move.canceled += ctx =>
            moveInput = Vector2.zero;

        controls.Player.Jump.performed += ctx =>
            Jump();

        wasGrounded = characterController.isGrounded;
    }

    private void Jump()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(
                jumpHeight * -2f * Physics.gravity.y
            );

            animator.SetBool("IsJumping", true);
        }
    }

    void Update()
    {
        bool freeLook = controls.Player.FreeLook.IsPressed();
        bool isRunning = controls.Player.Run.IsPressed();

        // Actualizar dirección del movimiento según la cámara.
        // Durante Free Look mantenemos la dirección que ya teníamos.
        if (!freeLook)
        {
            movementForward = Camera.main.transform.forward;
            movementRight = Camera.main.transform.right;

            movementForward.y = 0f;
            movementRight.y = 0f;

            movementForward.Normalize();
            movementRight.Normalize();
        }
        else if (!wasFreeLook)
        {
            movementForward = Camera.main.transform.forward;
            movementRight = Camera.main.transform.right;

            movementForward.y = 0f;
            movementRight.y = 0f;

            movementForward.Normalize();
            movementRight.Normalize();
        }

        // Movimiento relativo a la cámara.
        Vector3 movement =
            movementForward * moveInput.y +
            movementRight * moveInput.x;

        // Evitar que la diagonal sea más rápida.
        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }

        // Rotación del personaje.
        if (movement.sqrMagnitude > 0.01f && !freeLook)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(movement);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Velocidad.
        float currentSpeed = isRunning ? runSpeed : moveSpeed;

        Vector3 horizontalMovement =
            movement * currentSpeed;

        // Gravedad.
        if (characterController.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity +=
            Physics.gravity.y * Time.deltaTime;

        horizontalMovement.y = verticalVelocity;

        // Mover personaje.
        characterController.Move(
            horizontalMovement * Time.deltaTime
        );

        // Detectar si acaba de aterrizar.
        bool grounded = characterController.isGrounded;

        if (grounded && !wasGrounded)
        {
            animator.SetBool("IsJumping", false);
        }

        // Animaciones.
        float speed = movement.magnitude;

        animator.SetFloat("Speed", speed);
        animator.SetBool("IsRunning", isRunning);

        // Guardar estados para el siguiente frame.
        wasGrounded = grounded;
        wasFreeLook = freeLook;
    }
}