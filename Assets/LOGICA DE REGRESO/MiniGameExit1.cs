using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameExit : MonoBehaviour
{
    public string mainSceneName = "MainScene";  // Nombre de la escena principal
    private bool isPlayerInside = false;

    // Variable para verificar si la misión está completada
    public bool hasCompletedMission = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            Debug.Log("Presiona E para completar la misión y regresar a la escena principal");
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
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            // Completa la misión cuando el jugador presiona la tecla E dentro del área
            hasCompletedMission = true;
            Debug.Log("Misión completada. Ahora puedes regresar a la escena principal.");

            // Ahora que la misión está completada, se puede regresar
            ReturnToMainScene();
        }
    }

    private void ReturnToMainScene()
    {
        // Solo se regresa a la escena principal si la misión fue completada
        if (hasCompletedMission)
        {
            SceneManager.LoadScene(mainSceneName);
        }
        else
        {
            Debug.Log("La misión aún no está completada.");
        }
    }
}