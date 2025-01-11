using UnityEngine;
using UnityEngine.AI;

public class NPCNavMeshMovement : MonoBehaviour
{
    public Transform[] waypoints;
    private NavMeshAgent agent;

    public float waitTime = 2f;
    private float waitTimer = 0f;

    public float interactionRange = 2f;
    private GameObject player;

    public Animator animacionesNPC;

    public GameObject interactionIndicator; // Referencia al indicador visual

    private bool isInteracting = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (interactionIndicator != null)
        {
            interactionIndicator.SetActive(false); // Ocultar el indicador al inicio
        }

        if (waypoints.Length > 0)
        {
            SelectRandomWaypoint();
        }
    }

    private void Update()
    {
        if (isInteracting)
        {

            // Si está interactuando, revisa si el jugador se aleja.
            DetectPlayerDistanceAndEndInteraction();
            return;
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            animacionesNPC.SetBool("idle", true);
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                animacionesNPC.SetBool("idle", false);
                waitTimer = 0f;
                SelectRandomWaypoint();
            }
        }

        DetectPlayerInteraction();
    }

    private void SelectRandomWaypoint()
    {
        Transform waypoint = waypoints[Random.Range(0, waypoints.Length)];
        agent.SetDestination(waypoint.position);
    }

    private void DetectPlayerInteraction()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log("Distancia al jugador: " + distanceToPlayer);

        if (distanceToPlayer <= interactionRange)
        {
            // Mostrar indicador si está cerca
            if (interactionIndicator != null && !isInteracting)
            {
                interactionIndicator.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E) && !isInteracting)
            {
                StartInteraction();
            }
        }
        else
        {
            // Ocultar indicador si está lejos
            if (interactionIndicator != null && !isInteracting)
            {
                interactionIndicator.SetActive(false);
            }
        }
    }

    private void StartInteraction()
    {
        isInteracting = true;
        agent.isStopped = true;
        animacionesNPC.SetBool("idle", true);

        if (interactionIndicator != null)
        {
            interactionIndicator.SetActive(false); // Ocultar indicador durante la interacción
        }

        Debug.Log("Interacción con el NPC iniciada.");

        // Hacer que el NPC mire al jugador al comenzar la interacción
        LookAtPlayer();
    }

    private void DetectPlayerDistanceAndEndInteraction()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer > interactionRange) // Si el jugador se aleja más allá del rango de interacción
        {
            animacionesNPC.SetBool("idle", false);
            EndInteraction();
        }
    }

    private void LookAtPlayer()
    {
        if (player != null)
        {
            // Hacer que el NPC mire hacia la posición del jugador en el plano XZ
            Vector3 directionToPlayer = player.transform.position - transform.position;
            directionToPlayer.y = 0; // Evitar que mire hacia arriba o abajo (solo en el plano horizontal)
            Quaternion rotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = rotation;
        }
    }

    public void EndInteraction()
    {
        isInteracting = false;
        agent.isStopped = false;

        if (interactionIndicator != null)
        {
            interactionIndicator.SetActive(false); // Ocultar indicador al finalizar la interacción
        }

        Debug.Log("Interacción con el NPC terminada.");
    }
}
