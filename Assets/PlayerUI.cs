using UnityEngine;
using TMPro;  // Aseg�rate de importar el espacio de nombres de TextMeshPro

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // Referencia al componente TextMeshProUGUI en la UI
    public static int itemCount = 0;  // Contador de objetos recolectados

    void Start()
    {
        // Iniciar el texto con el valor inicial
        UpdateScoreText();
    }

    // Funci�n para a�adir al valor de los objetos recolectados
    public void AddItem(int amount)
    {
        itemCount += amount;  // Sumar el valor recolectado
        UpdateScoreText();     // Actualizar el texto en la UI
    }

    // Funci�n para actualizar el texto en la UI
    private void UpdateScoreText()
    {
        scoreText.text = "Cantidad de cartas encontradas: " + itemCount.ToString();  // Mostrar el valor en el texto
    }

    // Esta funci�n puede ser utilizada para obtener el n�mero actual de cartas
    public int GetItemCount()
    {
        return itemCount;
    }
}
