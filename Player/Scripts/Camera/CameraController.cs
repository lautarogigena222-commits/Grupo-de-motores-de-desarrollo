using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 8f;
    [SerializeField] private float height = 3f;
    [SerializeField] private float sensitivity = 0.2f;

    private PlayerControls controls;

    private float rotationX = 15f;
    private float rotationY;
    private bool wasFreeLook;

    private void Start()
    {
        controls = new PlayerControls();
        controls.Enable();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (target != null)
        {
            rotationY = target.eulerAngles.y;
        }
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector2 lookInput = controls.Player.Look.ReadValue<Vector2>();
        bool freeLook = controls.Player.FreeLook.IsPressed();

        // Entramos en Free Look
        if (freeLook)
        {
            rotationY += lookInput.x * sensitivity;
            rotationX -= lookInput.y * sensitivity;
        }
        else
        {
            // Si acabamos de soltar Alt, volvemos a la orientación del Knight
            if (wasFreeLook)
            {
                rotationY = target.eulerAngles.y;
            }
            else
            {
                // Comportamiento normal: cámara y personaje giran juntos
                rotationY += lookInput.x * sensitivity;
                rotationX -= lookInput.y * sensitivity;

                target.rotation = Quaternion.Euler(0f, rotationY, 0f);
            }
        }

        rotationX = Mathf.Clamp(rotationX, -30f, 60f);

        Quaternion cameraRotation =
            Quaternion.Euler(rotationX, rotationY, 0f);

        Vector3 targetPosition =
            target.position + Vector3.up * height;

        transform.position =
            targetPosition - cameraRotation * Vector3.forward * distance;

        transform.rotation = cameraRotation;

        wasFreeLook = freeLook;
    }
}