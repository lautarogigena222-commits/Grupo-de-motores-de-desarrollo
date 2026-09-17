using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interacción")]
    [SerializeField] private float interactDistance = 4f;
    [SerializeField] private LayerMask interactLayer;

    private PlayerControls controls;

    private void Start()
    {
        controls = new PlayerControls();

        controls.Player.Interact.performed += OnInteract;

        controls.Player.Enable();
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            interactDistance,
            interactLayer,
            QueryTriggerInteraction.Collide))
        {
            IInteractable interactable =
                hit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
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
            controls.Player.Interact.performed -= OnInteract;

            controls.Dispose();
            controls = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawRay(
            transform.position,
            transform.forward * interactDistance
        );
    }
}