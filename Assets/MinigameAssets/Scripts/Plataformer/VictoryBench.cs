using UnityEngine;
using TMPro; 

public class VictoryBench : MonoBehaviour
{
    public GameObject redLight;
    public GameObject greenLight;
    public GameObject exitLogic;
    public TextMeshProUGUI uiText; 
    private MinigameManager minigameManager;
    private bool playerInRange = false;

    private void Start()
    {
        minigameManager = FindObjectOfType<MinigameManager>();

        redLight.SetActive(true);
        greenLight.SetActive(false);
        exitLogic.SetActive(false);

        if (uiText != null)
        {
            uiText.text = ""; // Inicializa el texto como vacío
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (minigameManager.listTorches == minigameManager.totalTorches)
        {
            redLight.SetActive(false);
            greenLight.SetActive(true);
            exitLogic.SetActive(true);

            if (uiText != null)
            {
                uiText.text = "¡Minijuego completado!"; 
            }
        }
        else
        {
            // Si faltan antorchas
            if (uiText != null)
            {
                uiText.text = "No has recolectado todas las antorchas, regresa y busca las que faltan."; 
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (uiText != null)
            {
                uiText.text = "Press'E'"; 
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (uiText != null)
            {
                uiText.text = ""; 
            }
        }
    }
}
