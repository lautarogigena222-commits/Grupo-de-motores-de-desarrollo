using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Movimiento básico de un jefe (boss) en Unity 3D.
/// Persigue al jugador, se detiene al llegar a la distancia de ataque
/// y siempre lo mira de frente con un giro suave.
///
/// Uso:
///  1. Hornea el NavMesh (Window > AI > Navigation > Bake).
///  2. Poné este script en el boss (el NavMeshAgent se agrega solo).
///  3. Poné el tag "Player" al jugador, o arrastrá su Transform al campo Target.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class BossMovimientoBasico : MonoBehaviour
{
    [Header("Objetivo")]
    [SerializeField] private Transform target;
    [SerializeField] private string playerTag = "Player";

    [Header("Movimiento")]
    [SerializeField] private float speed = 4f;
    [SerializeField] private float rotationSpeed = 6f;

    [Header("Distancias")]
    [Tooltip("A partir de esta distancia el boss empieza a perseguir.")]
    [SerializeField] private float detectionRange = 20f;
    [Tooltip("Distancia a la que se detiene frente al jugador.")]
    [SerializeField] private float stopDistance = 2.5f;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.updateRotation = false;   // rotamos a mano para un giro más suave
        agent.stoppingDistance = stopDistance;
    }

    private void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(playerTag);
            if (player != null) target = player.transform;
        }
    }

    private void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= detectionRange && distance > stopDistance)
        {
            // Persigue
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }
        else
        {
            // Fuera de rango o ya está encima: se queda quieto
            agent.isStopped = true;
        }

        // Siempre encara al jugador si lo tiene detectado
        if (distance <= detectionRange) FaceTarget();
    }

    /// <summary>Gira suavemente hacia el jugador, solo en el eje Y.</summary>
    private void FaceTarget()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0f;
        if (direction.sqrMagnitude < 0.01f) return;

        Quaternion look = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, look, rotationSpeed * Time.deltaTime);
    }

    // Dibuja los radios en la escena para ajustarlos a ojo
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
