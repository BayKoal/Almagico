//using UnityEngine;
//using UnityEngine.AI;
//using UnityEngine.UI;  // Para usar UI Text

//public class NPCNavMeshMovement : MonoBehaviour
//{
//    public Transform[] waypoints;
//    private NavMeshAgent agent;

//    public float waitTime = 2f;
//    private float waitTimer = 0f;

//    public float interactionRange = 2f;
//    private GameObject player;

//    public Animator animacionesNPC;

//    public GameObject interactionIndicator; // Referencia al indicador visual

//    private bool isInteracting = false;

//    // Para mostrar el texto de interacci�n
//    public Text npcText;  // Referencia al Text en la UI
//    public string[] interactionMessages; // Mensajes de la interacci�n
//    private int currentMessageIndex = 0; // �ndice del mensaje actual

//    private Camera mainCamera; // Referencia a la c�mara principal

//    // Referencia al PlayerUI donde se guarda el n�mero de cartas
//    public PlayerUI playerUI;

//    // Variable modificable desde el Inspector para definir la cantidad de cartas necesarias
//    public int requiredCards = 3;  // N�mero de cartas necesarias para desbloquear el �ltimo mensaje

//    private bool hasRequiredCards = false;  // Estado que indica si el jugador tiene las cartas necesarias

//    private void Start()
//    {
//        agent = GetComponent<NavMeshAgent>();
//        player = GameObject.FindGameObjectWithTag("Player");
//        mainCamera = Camera.main; // Asegurarse de obtener la c�mara principal

//        if (interactionIndicator != null)
//        {
//            interactionIndicator.SetActive(false); // Ocultar el indicador al inicio
//        }

//        if (npcText != null)
//        {
//            npcText.gameObject.SetActive(false); // Asegurarse de que el texto est� oculto al inicio
//        }

//        if (waypoints.Length > 0)
//        {
//            SelectRandomWaypoint();
//        }
//    }

//    private void Update()
//    {
//        if (isInteracting)
//        {
//            DetectPlayerDistanceAndEndInteraction();

//            // Hacer que el NPC siempre mire al jugador durante la interacci�n
//            LookAtPlayer();

//            if (Input.GetMouseButtonDown(0)) // Clic izquierdo
//            {
//                ShowNextMessage();
//            }
//            return;
//        }

//        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
//        {
//            animacionesNPC.SetBool("idle", true);
//            waitTimer += Time.deltaTime;
//            if (waitTimer >= waitTime)
//            {
//                animacionesNPC.SetBool("idle", false);
//                waitTimer = 0f;
//                SelectRandomWaypoint();
//            }
//        }

//        DetectPlayerInteraction();

//        // Actualizar el indicador para que mire al jugador
//        UpdateInteractionIndicator();
//    }

//    private void SelectRandomWaypoint()
//    {
//        Transform waypoint = waypoints[Random.Range(0, waypoints.Length)];
//        agent.SetDestination(waypoint.position);
//    }

//    private void DetectPlayerInteraction()
//    {
//        if (player == null) return;

//        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

//        if (distanceToPlayer <= interactionRange)
//        {
//            // Mostrar indicador si est� cerca
//            if (interactionIndicator != null && !isInteracting)
//            {
//                interactionIndicator.SetActive(true);
//            }

//            if (Input.GetKeyDown(KeyCode.E) && !isInteracting)
//            {
//                StartInteraction();
//            }
//        }
//        else
//        {
//            // Ocultar indicador si est� lejos
//            if (interactionIndicator != null && !isInteracting)
//            {
//                interactionIndicator.SetActive(false);
//            }
//        }
//    }

//    private void StartInteraction()
//    {
//        isInteracting = true;
//        agent.isStopped = true;
//        animacionesNPC.SetBool("idle", true);

//        if (interactionIndicator != null)
//        {
//            interactionIndicator.SetActive(false); // Ocultar indicador durante la interacci�n
//        }

//        // Verificar si el jugador tiene las cartas necesarias
//        hasRequiredCards = HasRequiredCards();

//        // Si el jugador tiene las cartas necesarias, comenzar desde el saludo
//        if (hasRequiredCards)
//        {
//            currentMessageIndex = 0;  // Reiniciar el flujo del di�logo para mostrar desde el saludo
//        }

//        // Mostrar el primer mensaje
//        ShowNextMessage();
//    }

//    private void DetectPlayerDistanceAndEndInteraction()
//    {
//        if (player == null) return;

//        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
//        if (distanceToPlayer > interactionRange) // Si el jugador se aleja m�s all� del rango de interacci�n
//        {
//            animacionesNPC.SetBool("idle", false);
//            EndInteraction();
//        }
//    }

//    private void LookAtPlayer()
//    {
//        if (player != null)
//        {
//            // Hacer que el NPC mire hacia la posici�n del jugador en el plano XZ
//            Vector3 directionToPlayer = player.transform.position - transform.position;
//            directionToPlayer.y = 0; // Evitar que mire hacia arriba o abajo (solo en el plano horizontal)
//            Quaternion rotation = Quaternion.LookRotation(directionToPlayer);
//            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f); // Movimiento suave
//        }
//    }

//    private void UpdateInteractionIndicator()
//    {
//        if (interactionIndicator != null && mainCamera != null)
//        {
//            // Aseg�rate de que el indicador siempre mire hacia la c�mara principal
//            Vector3 directionToCamera = mainCamera.transform.position - interactionIndicator.transform.position;
//            directionToCamera.y = 0; // Mant�n solo el plano horizontal
//            Quaternion rotation = Quaternion.LookRotation(directionToCamera); // Apuntar hacia la c�mara
//            interactionIndicator.transform.rotation = Quaternion.Slerp(interactionIndicator.transform.rotation, rotation, Time.deltaTime * 5f);
//        }
//    }

//    public void EndInteraction()
//    {
//        isInteracting = false;
//        agent.isStopped = false;

//        if (interactionIndicator != null)
//        {
//            interactionIndicator.SetActive(false); // Ocultar indicador al finalizar la interacci�n
//        }

//        if (npcText != null)
//        {
//            npcText.gameObject.SetActive(false); // Ocultar el texto al finalizar
//        }
//    }

//    private void ShowNextMessage()
//    {
//        if (npcText != null && interactionMessages.Length > 0)
//        {
//            // Si es el �ltimo mensaje y no tiene las cartas necesarias, mostrar el mensaje de advertencia
//            if (currentMessageIndex == interactionMessages.Length - 1 && !hasRequiredCards)
//            {
//                npcText.text = "Te faltan " + (requiredCards - playerUI.GetItemCount()) + " cartas para continuar...";
//                return;
//            }

//            npcText.gameObject.SetActive(true); // Asegurarse de que el texto se muestre
//            npcText.text = interactionMessages[currentMessageIndex]; // Mostrar el mensaje actual

//            currentMessageIndex++;

//            if (currentMessageIndex >= interactionMessages.Length)
//            {
//                currentMessageIndex = 0; // Volver al primer mensaje si ya se mostr� todos
//            }
//        }
//    }

//    // Funci�n que verifica si el jugador tiene las cartas necesarias
//    private bool HasRequiredCards()
//    {
//        // Comprobamos si el jugador tiene las cartas necesarias (esto depende de c�mo manejes las cartas en tu juego)
//        if (playerUI != null && playerUI.GetItemCount() >= requiredCards) // Usamos la variable p�blica "requiredCards"
//        {
//            return true;
//        }
//        return false;
//    }
//}
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;

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

    // Para mostrar el texto de interacci�n
    public Text npcText;  // Referencia al Text en la UI
    public string[] interactionMessages; // Mensajes de la interacci�n
    private int currentMessageIndex = 0; // �ndice del mensaje actual

    private Camera mainCamera; // Referencia a la c�mara principal

    // Referencia al PlayerUI donde se guarda el n�mero de cartas
    public PlayerUI playerUI;

    // Referencia al CorduraController para obtener la cordura
    public CorduraController corduraController;

    // Variable modificable desde el Inspector para definir la cantidad de cartas necesarias
    public int requiredCards = 3;  // N�mero de cartas necesarias para desbloquear el �ltimo mensaje

    // Umbral de cordura necesario para acceder al �ltimo mensaje
    public int requiredCordura = 50;  // Umbral de cordura necesario

    private bool hasRequiredCards = false;  // Estado que indica si el jugador tiene las cartas necesarias
    private bool hasRequiredCordura = false;  // Estado que indica si el jugador tiene suficiente cordura

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main; // Asegurarse de obtener la c�mara principal

        if (interactionIndicator != null)
        {
            interactionIndicator.SetActive(false); // Ocultar el indicador al inicio
        }

        if (npcText != null)
        {
            npcText.gameObject.SetActive(false); // Asegurarse de que el texto est� oculto al inicio
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
            DetectPlayerDistanceAndEndInteraction();

            // Hacer que el NPC siempre mire al jugador durante la interacci�n
            LookAtPlayer();

            if (Input.GetMouseButtonDown(0)) // Clic izquierdo
            {
                ShowNextMessage();
            }
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

        // Actualizar el indicador para que mire al jugador
        UpdateInteractionIndicator();
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

        if (distanceToPlayer <= interactionRange)
        {
            // Mostrar indicador si est� cerca
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
            // Ocultar indicador si est� lejos
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
            interactionIndicator.SetActive(false); // Ocultar indicador durante la interacci�n
        }

        // Verificar si el jugador tiene las cartas y cordura necesarias
        hasRequiredCards = HasRequiredCards();
        hasRequiredCordura = HasRequiredCordura();

        // Reiniciar el �ndice de los mensajes cada vez que el jugador interact�e
        currentMessageIndex = 0;  // Reiniciar el flujo del di�logo para mostrar desde el primer mensaje

        // Mostrar el primer mensaje
        ShowNextMessage();
    }


    private void DetectPlayerDistanceAndEndInteraction()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer > interactionRange) // Si el jugador se aleja m�s all� del rango de interacci�n
        {
            animacionesNPC.SetBool("idle", false);
            EndInteraction();
        }
    }

    private void LookAtPlayer()
    {
        if (player != null)
        {
            // Hacer que el NPC mire hacia la posici�n del jugador en el plano XZ
            Vector3 directionToPlayer = player.transform.position - transform.position;
            directionToPlayer.y = 0; // Evitar que mire hacia arriba o abajo (solo en el plano horizontal)
            Quaternion rotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f); // Movimiento suave
        }
    }

    private void UpdateInteractionIndicator()
    {
        if (interactionIndicator != null && mainCamera != null)
        {
            // Aseg�rate de que el indicador siempre mire hacia la c�mara principal
            Vector3 directionToCamera = mainCamera.transform.position - interactionIndicator.transform.position;
            directionToCamera.y = 0; // Mant�n solo el plano horizontal
            Quaternion rotation = Quaternion.LookRotation(directionToCamera); // Apuntar hacia la c�mara
            interactionIndicator.transform.rotation = Quaternion.Slerp(interactionIndicator.transform.rotation, rotation, Time.deltaTime * 5f);
        }
    }

    public void EndInteraction()
    {
        isInteracting = false;
        agent.isStopped = false;

        if (interactionIndicator != null)
        {
            interactionIndicator.SetActive(false); // Ocultar indicador al finalizar la interacci�n
        }

        if (npcText != null)
        {
            npcText.gameObject.SetActive(false); // Ocultar el texto al finalizar
        }
    }

    private void ShowNextMessage()
    {
        if (npcText != null && interactionMessages.Length > 0)
        {
            // Si es el �ltimo mensaje y no tiene las cartas necesarias o suficiente cordura, mostrar el mensaje de advertencia
            if (currentMessageIndex == interactionMessages.Length - 1)
            {
                if (!hasRequiredCards)
                {
                    npcText.text = "Te faltan " + (requiredCards - playerUI.GetItemCount()) + " cartas para continuar...";
                    return;
                }

                if (!hasRequiredCordura)
                {
                    // Aqu� mostramos la cantidad exacta de cordura que falta
                    float corduraFaltante = requiredCordura - corduraController.corduraSlider.value;
                    npcText.text = "Tu cordura est� demasiado baja. Te faltan " + Mathf.CeilToInt(corduraFaltante) + " puntos de cordura para continuar.";
                    return;
                }
            }

            npcText.gameObject.SetActive(true); // Asegurarse de que el texto se muestre
            npcText.text = interactionMessages[currentMessageIndex]; // Mostrar el mensaje actual

            currentMessageIndex++;

            if (currentMessageIndex >= interactionMessages.Length)
            {
                currentMessageIndex = 0; // Volver al primer mensaje si ya se mostr� todos
            }
        }
    }

    // Funci�n que verifica si el jugador tiene las cartas necesarias
    private bool HasRequiredCards()
    {
        // Comprobamos si el jugador tiene las cartas necesarias
        return playerUI != null && playerUI.GetItemCount() >= requiredCards;
    }

    // Funci�n que verifica si el jugador tiene suficiente cordura
    private bool HasRequiredCordura()
    {
        // Comprobamos si el jugador tiene la cordura suficiente
        return corduraController != null && corduraController.corduraSlider.value >= requiredCordura;
    }
}
