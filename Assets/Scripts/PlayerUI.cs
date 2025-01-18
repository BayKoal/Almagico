using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // Referencia al componente TextMeshProUGUI en la UI
    public static int itemCount = 0;   // Contador de cartas recolectadas
    public TextMeshProUGUI sanityText; // Referencia al texto que muestra la cordura en la UI

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

    // Funci�n para actualizar el texto en la UI sobre las cartas
    private void UpdateScoreText()
    {
        scoreText.text = "Cantidad de cartas encontradas: " + itemCount.ToString();  // Mostrar el valor en el texto
    }

    // Esta funci�n puede ser utilizada para obtener el n�mero actual de cartas
    public int GetItemCount()
    {
        return itemCount;
    }

    // Funci�n para actualizar la cordura (aunque ya no ser� usada por el NPC)
    public void UpdateSanityText()
    {
        // Eliminar la referencia a la cordura ya que no se est� utilizando
    }

    // Esta funci�n puede ser utilizada para obtener el valor de la cordura (aunque ya no se usa en este contexto)
    public int GetSanity()
    {
        return 50;  // Valor predeterminado de cordura
    }
}
