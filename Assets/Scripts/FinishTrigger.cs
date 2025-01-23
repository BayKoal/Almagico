using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinishTrigger : MonoBehaviour
{
    public TextMeshProUGUI finishPrompt; // Referencia al TMP para el mensaje
    public string finalSceneName = "FinalScene"; // Nombre de la escena final
    private bool canFinish = false; // Si el jugador está en la zona de interacción

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && AllNPCsCompleted())
        {
            // Activar el mensaje si el jugador entra en la zona y se completaron todos los NPCs
            finishPrompt.gameObject.SetActive(true);
            canFinish = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Desactivar el mensaje si el jugador sale de la zona
            finishPrompt.gameObject.SetActive(false);
            canFinish = false;
        }
    }

    private void Update()
    {
        // Detectar la tecla "E" si el jugador puede finalizar
        if (canFinish && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(finalSceneName);
        }
    }

    private bool AllNPCsCompleted()
    {
        // Comprobar si todos los NPCs están completados
        if (GameManager.Instance != null)
        {
            return GameManager.Instance.npcsCompletados.Count >= GameManager.Instance.npcsTotal;
        }
        return false;
    }
}
