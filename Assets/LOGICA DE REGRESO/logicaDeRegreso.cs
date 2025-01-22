using UnityEngine;
using UnityEngine.SceneManagement; // Para cambiar de escena

public class MiniGameExit : MonoBehaviour
{
    // Nombre de la escena principal a la que deseas regresar
    public string mainSceneName = "MainScene";

    // Detecta si el jugador está dentro del collider
    private bool isPlayerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en el trigger es el jugador (por ejemplo, su etiqueta es "Player")
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            Debug.Log("Presiona E para regresar a la escena principal");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Si el jugador sale del trigger, deja de estar dentro
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    private void Update()
    {
        // Si el jugador está dentro del collider y presiona E, regresar a la escena principal
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            ReturnToMainScene();
        }
    }

    // Método para cargar la escena principal
    private void ReturnToMainScene()
    {
        SceneManager.LoadScene(mainSceneName);
    }
}
