using UnityEngine;
using TMPro; // Para TextMeshPro

public class FadeText : MonoBehaviour
{
    public TextMeshProUGUI texto; 
    public float duracion = 8f; 
    public float fadeSpeed = 2f; 

    private float tiempoMostrar;

    void Start()
    {
        tiempoMostrar = Time.time + duracion;
        texto.alpha = 1f; 
    }

    void Update()
    {
        if (Time.time > tiempoMostrar)
        {
            // Desvanece el texto lentamente
            texto.alpha -= fadeSpeed * Time.deltaTime;

            // Desactiva el texto cuando sea completamente transparente
            if (texto.alpha <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
