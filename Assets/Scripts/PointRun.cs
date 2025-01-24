using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Para reiniciar o cargar una escena

public class PlayerPoints : MonoBehaviour
{
    public int puntos = 2; // Puntos iniciales

    // Método para sumar puntos
    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad;
        Debug.Log("Puntos ganados: " + cantidad + ". Total: " + puntos);
    }

    // Método para restar puntos
    public void RestarPuntos(int cantidad)
    {
        puntos -= cantidad;
        puntos = Mathf.Max(puntos, 0); // Asegurarse de que no baje de 0
        Debug.Log("Puntos perdidos: " + cantidad + ". Total: " + puntos);

        if (puntos == 0)
        {
            TerminarJuego();
        }
    }

    // Método para finalizar el juego
    private void TerminarJuego()
    {
        Debug.Log("¡Game Over! Tus puntos llegaron a 0.");
        // Aquí puedes agregar lógica adicional, como pausar el juego o mostrar una pantalla de Game Over.

        // Ejemplo: Reiniciar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}