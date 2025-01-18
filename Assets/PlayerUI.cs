using UnityEngine;
using TMPro;  // Asegúrate de importar el espacio de nombres de TextMeshPro

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // Referencia al componente TextMeshProUGUI en la UI
    public static int itemCount = 0;  // Contador de objetos recolectados

    void Start()
    {
        // Iniciar el texto con el valor inicial
        UpdateScoreText();
    }

    // Función para añadir al valor de los objetos recolectados
    public void AddItem(int amount)
    {
        itemCount += amount;  // Sumar el valor recolectado
        UpdateScoreText();     // Actualizar el texto en la UI
    }

    // Función para actualizar el texto en la UI
    private void UpdateScoreText()
    {
        scoreText.text = "Cantidad de cartas encontradas: " + itemCount.ToString();  // Mostrar el valor en el texto
    }

    // Esta función puede ser utilizada para obtener el número actual de cartas
    public int GetItemCount()
    {
        return itemCount;
    }
}
