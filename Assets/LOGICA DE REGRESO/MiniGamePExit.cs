using UnityEngine;
using UnityEngine.SceneManagement; // Para cambiar de escena

public class MiniGamePExit : MonoBehaviour
{
    private string mainSceneName = "ALMA ESCENA PRINCIPAL";
    private bool isPlayerInside = false;
    public MinigameManager minigameManager;
    public GameObject victoryBench;
    public GameObject exitLogic;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            Debug.Log("Presiona E para regresar a la escena principal");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    private void Update()
    {
        if (minigameManager.listTorches == minigameManager.totalTorches)
        {
            if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
            {
                ReturnToMainScene();
            }

        }
        else
        {
            exitLogic.SetActive(false);
        }

    }
    private void ReturnToMainScene()
    {
        SceneManager.LoadScene(mainSceneName);
    }
}
