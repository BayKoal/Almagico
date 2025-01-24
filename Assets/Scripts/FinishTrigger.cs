using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinishTrigger : MonoBehaviour
{
    public TextMeshProUGUI finishPrompt; // Referencia al TMP para el mensaje
    public string finalSceneName = "FinalScene"; // Nombre de la escena final
    private bool canFinish = false; // Si el jugador está en la zona de interacción

    public GameObject[] lightsToActivate; // Lista de luces a activar
    private bool lightsActivated = false; // Si las luces ya se activaron

    private void Start()
    {
        // Asegurarse de que las luces estén desactivadas al inicio
        ActivateLights(false);

        if (finishPrompt != null)
        {
            finishPrompt.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Revisar si todos los NPCs han sido completados y activar las luces si no están activadas
        if (!lightsActivated && AllNPCsCompleted())
        {
            ActivateLights(true);
            lightsActivated = true;
            Debug.Log("Todas las luces se han activado, todos los NPCs están completados.");
        }

        // Detectar la tecla "E" si el jugador puede finalizar
        if (canFinish && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(finalSceneName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && lightsActivated)
        {
            // Activar el mensaje si el jugador entra en la zona y las luces ya están activadas
            if (finishPrompt != null)
            {
                finishPrompt.gameObject.SetActive(true);
            }
            canFinish = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Desactivar el mensaje si el jugador sale de la zona
            if (finishPrompt != null)
            {
                finishPrompt.gameObject.SetActive(false);
            }
            canFinish = false;
        }
    }

    private bool AllNPCsCompleted()
    {
        // Comprobar si todos los NPCs están completados
        if (GameManager.Instance != null)
        {
            Debug.Log($"NPCs completados: {GameManager.Instance.npcsCompletados.Count}/{GameManager.Instance.npcsTotal}");
            return GameManager.Instance.npcsCompletados.Count >= GameManager.Instance.npcsTotal;
        }
        return false;
    }

    private void ActivateLights(bool state)
    {
        // Activar o desactivar todas las luces en la lista
        foreach (GameObject light in lightsToActivate)
        {
            if (light != null)
            {
                light.SetActive(state);
            }
        }
    }
}
