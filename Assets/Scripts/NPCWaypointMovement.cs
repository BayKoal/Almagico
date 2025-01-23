
//using UnityEngine.UI;
//using UnityEngine.AI;
//using UnityEngine;

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

//    // Para mostrar el texto de interacción
//    public Text npcText;  // Referencia al Text en la UI
//    public string[] interactionMessages; // Mensajes de la interacción
//    private int currentMessageIndex = 0; // Índice del mensaje actual

//    private Camera mainCamera; // Referencia a la cámara principal

//    // Referencia al PlayerUI donde se guarda el número de cartas
//    public PlayerUI playerUI;

//    // Referencia al CorduraController para obtener la cordura
//    public CorduraController corduraController;

//    // Variable modificable desde el Inspector para definir la cantidad de cartas necesarias
//    public int requiredCards = 3;  // Número de cartas necesarias para desbloquear el último mensaje

//    // Umbral de cordura necesario para acceder al último mensaje
//    public int requiredCordura = 50;  // Umbral de cordura necesario

//    private bool hasRequiredCards = false;  // Estado que indica si el jugador tiene las cartas necesarias
//    private bool hasRequiredCordura = false;  // Estado que indica si el jugador tiene suficiente cordura

//    private void Start()
//    {
//        agent = GetComponent<NavMeshAgent>();
//        player = GameObject.FindGameObjectWithTag("Player");
//        mainCamera = Camera.main; // Asegurarse de obtener la cámara principal

//        if (interactionIndicator != null)
//        {
//            interactionIndicator.SetActive(false); // Ocultar el indicador al inicio
//        }

//        if (npcText != null)
//        {
//            npcText.gameObject.SetActive(false); // Asegurarse de que el texto esté oculto al inicio
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

//            // Hacer que el NPC siempre mire al jugador durante la interacción
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
//            // Mostrar indicador si está cerca
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
//            // Ocultar indicador si está lejos
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
//            interactionIndicator.SetActive(false); // Ocultar indicador durante la interacción
//        }

//        // Verificar si el jugador tiene las cartas y cordura necesarias
//        hasRequiredCards = HasRequiredCards();
//        hasRequiredCordura = HasRequiredCordura();

//        // Reiniciar el índice de los mensajes cada vez que el jugador interactúe
//        currentMessageIndex = 0;  // Reiniciar el flujo del diálogo para mostrar desde el primer mensaje

//        // Mostrar el primer mensaje
//        ShowNextMessage();
//    }


//    private void DetectPlayerDistanceAndEndInteraction()
//    {
//        if (player == null) return;

//        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
//        if (distanceToPlayer > interactionRange) // Si el jugador se aleja más allá del rango de interacción
//        {
//            animacionesNPC.SetBool("idle", false);
//            EndInteraction();
//        }
//    }

//    private void LookAtPlayer()
//    {
//        if (player != null)
//        {
//            // Hacer que el NPC mire hacia la posición del jugador en el plano XZ
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
//            // Asegúrate de que el indicador siempre mire hacia la cámara principal
//            Vector3 directionToCamera = mainCamera.transform.position - interactionIndicator.transform.position;
//            directionToCamera.y = 0; // Mantén solo el plano horizontal
//            Quaternion rotation = Quaternion.LookRotation(directionToCamera); // Apuntar hacia la cámara
//            interactionIndicator.transform.rotation = Quaternion.Slerp(interactionIndicator.transform.rotation, rotation, Time.deltaTime * 5f);
//        }
//    }

//    public void EndInteraction()
//    {
//        isInteracting = false;
//        agent.isStopped = false;

//        if (interactionIndicator != null)
//        {
//            interactionIndicator.SetActive(false); // Ocultar indicador al finalizar la interacción
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
//            // Si es el último mensaje y no tiene las cartas necesarias o suficiente cordura, mostrar el mensaje de advertencia
//            if (currentMessageIndex == interactionMessages.Length - 1)
//            {
//                if (!hasRequiredCards)
//                {
//                    npcText.text = "Te faltan " + (requiredCards - playerUI.GetItemCount()) + " cartas para continuar...";
//                    return;
//                }

//                if (!hasRequiredCordura)
//                {
//                    // Aquí mostramos la cantidad exacta de cordura que falta
//                    float corduraFaltante = requiredCordura - corduraController.corduraSlider.value;
//                    npcText.text = "Tu cordura está demasiado baja. Te faltan " + Mathf.CeilToInt(corduraFaltante) + " puntos de cordura para continuar.";
//                    return;
//                }
//            }

//            npcText.gameObject.SetActive(true); // Asegurarse de que el texto se muestre
//            npcText.text = interactionMessages[currentMessageIndex]; // Mostrar el mensaje actual

//            currentMessageIndex++;

//            if (currentMessageIndex >= interactionMessages.Length)
//            {
//                currentMessageIndex = 0; // Volver al primer mensaje si ya se mostró todos
//            }
//        }
//    }

//    // Función que verifica si el jugador tiene las cartas necesarias
//    private bool HasRequiredCards()
//    {
//        // Comprobamos si el jugador tiene las cartas necesarias
//        return playerUI != null && playerUI.GetItemCount() >= requiredCards;
//    }

//    // Función que verifica si el jugador tiene suficiente cordura
//    private bool HasRequiredCordura()
//    {
//        // Comprobamos si el jugador tiene la cordura suficiente
//        return corduraController != null && corduraController.corduraSlider.value >= requiredCordura;
//    }
//}
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCNavMeshMovement : MonoBehaviour
{
    public Transform[] waypoints;
    private NavMeshAgent agent;

    public float waitTime = 2f;
    private float waitTimer = 0f;

    public float interactionRange = 2f;
    private GameObject player;

    public Animator animacionesNPC;

    public GameObject interactionIndicator;

    private bool isInteracting = false;

    // Para mostrar el texto de interacción
    public Text npcText;
    public string[] interactionMessages;
    public string specialMessage; // Mensaje especial al cumplir las condiciones
    private int currentMessageIndex = 0;

    private Camera mainCamera;

    // Referencia al PlayerUI donde se guarda el número de cartas
    public PlayerUI playerUI;

    // Referencia al CorduraController para obtener la cordura
    public CorduraController corduraController;

    public int requiredCards = 3;
    public int requiredCordura = 50;

    private bool hasRequiredCards = false;
    private bool hasRequiredCordura = false;

    // Nombre de la escena a cargar
    public string nextSceneName;

    // Desvanecimiento de la escena
    public Image fadeImage; // Imagen negra usada para el efecto de desvanecimiento
    public float fadeDuration = 1.5f;

    public string npcID; // ID único del NPC

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main;

        if (interactionIndicator != null)
        {
            interactionIndicator.SetActive(false);
        }

        if (npcText != null)
        {
            npcText.gameObject.SetActive(false);
        }

        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(false);
        }

        if (waypoints.Length > 0)
        {
            SelectRandomWaypoint();
        }

        if (GameManager.Instance.npcsCompletados.Contains(npcID))
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        if (isInteracting)
        {
            DetectPlayerDistanceAndEndInteraction();
            LookAtPlayer();

            if (Input.GetMouseButtonDown(0))
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
        UpdateInteractionIndicator();
    }

    private void DestroyNPC()
    {
        // Llamar al GameManager para agregar este NPC como completado
        GameManager.Instance.AddCompletedNPC(npcID);

        // Ahora destruimos el NPC
        Destroy(gameObject);
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
            interactionIndicator.SetActive(false);
        }

        hasRequiredCards = HasRequiredCards();
        hasRequiredCordura = HasRequiredCordura();
        currentMessageIndex = 0;
        ShowNextMessage();
    }

    private void DetectPlayerDistanceAndEndInteraction()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer > interactionRange)
        {
            animacionesNPC.SetBool("idle", false);
            EndInteraction();
        }
    }

    private void LookAtPlayer()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;
            directionToPlayer.y = 0;
            Quaternion rotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
        }
    }

    private void UpdateInteractionIndicator()
    {
        if (interactionIndicator != null && mainCamera != null)
        {
            Vector3 directionToCamera = mainCamera.transform.position - interactionIndicator.transform.position;
            directionToCamera.y = 0;
            Quaternion rotation = Quaternion.LookRotation(directionToCamera);
            interactionIndicator.transform.rotation = Quaternion.Slerp(interactionIndicator.transform.rotation, rotation, Time.deltaTime * 5f);
        }
    }

    public void EndInteraction()
    {
        isInteracting = false;
        agent.isStopped = false;

        if (interactionIndicator != null)
        {
            interactionIndicator.SetActive(false);
        }

        if (npcText != null)
        {
            npcText.gameObject.SetActive(false);
        }
    }

    private void ShowNextMessage()
    {
        if (npcText != null && interactionMessages.Length > 0)
        {
            if (currentMessageIndex == interactionMessages.Length - 1)
            {
                if (!hasRequiredCards)
                {
                    npcText.text = "Te faltan " + (requiredCards - playerUI.GetItemCount()) + " cartas para continuar...";
                    return;
                }

                if (!hasRequiredCordura)
                {
                    float corduraFaltante = requiredCordura - corduraController.corduraSlider.value;
                    npcText.text = "Tu cordura está demasiado baja. Te faltan " + Mathf.CeilToInt(corduraFaltante) + " puntos de cordura para continuar.";
                    return;
                }

                // Cumple las condiciones: marcar como completado
                GameManager.Instance.AddCompletedNPC(npcID);

                // Mostrar mensaje especial y cambiar de escena
                npcText.text = specialMessage;
                StartCoroutine(FadeAndChangeScene());
                return;
            }

            npcText.gameObject.SetActive(true);
            npcText.text = interactionMessages[currentMessageIndex];
            currentMessageIndex++;
        }
    }
private bool HasRequiredCards()
    {
        return playerUI != null && playerUI.GetItemCount() >= requiredCards;
    }

    private bool HasRequiredCordura()
    {
        return corduraController != null && corduraController.corduraSlider.value >= requiredCordura;
    }

    private IEnumerator FadeAndChangeScene()
    {
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            Color fadeColor = fadeImage.color;

            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                fadeColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
                fadeImage.color = fadeColor;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            fadeColor.a = 1f;
            fadeImage.color = fadeColor;
        }

        SceneManager.LoadScene(nextSceneName);
    }
}

