using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Cutscenes"); // Cambia a la escena de las transiciones
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!"); // Solo visible en el Editor
        Application.Quit();      // Salir del juego en builds
    }
}

