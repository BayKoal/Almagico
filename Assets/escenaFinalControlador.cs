using UnityEngine;
using TMPro;
using System.Collections;

public class FinalSceneController : MonoBehaviour
{
    public TextMeshProUGUI mensajeFinal1; // Primer mensaje
    public TextMeshProUGUI mensajeFinal2; // Segundo mensaje (Créditos)
    public TextMeshProUGUI mensajeFinal3; // Tercer mensaje (Último mensaje)
    public float tiempoMensajeFinal1 = 3f; // Tiempo de aparición del primer mensaje
    public float tiempoMensajeFinal2 = 10f; // Tiempo de los créditos (el segundo mensaje)
    public float tiempoFade = 1f; // Tiempo de fade
    public float velocidadDesplazamientoCreditos = 50f; // Velocidad de desplazamiento de los créditos

    private void Start()
    {
        // Desactivar todos los mensajes al inicio
        mensajeFinal1.gameObject.SetActive(false);
        mensajeFinal2.gameObject.SetActive(false);
        mensajeFinal3.gameObject.SetActive(false);

        // Comienza la secuencia de los mensajes
        StartCoroutine(MostrarSecuenciaFinal());
    }

    private IEnumerator MostrarSecuenciaFinal()
    {
        // Mostrar el primer mensaje con fade
        mensajeFinal1.gameObject.SetActive(true);
        yield return StartCoroutine(FadeInOut(mensajeFinal1, tiempoMensajeFinal1, tiempoFade));

        // Mostrar el segundo mensaje (créditos) con fade-in
        mensajeFinal2.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn(mensajeFinal2, tiempoFade));

        // Desplazamiento hacia arriba de los créditos
        Vector3 posicionOriginal = mensajeFinal2.transform.position;
        Vector3 posicionFinal = new Vector3(posicionOriginal.x, posicionOriginal.y + 500f, posicionOriginal.z);

        float tiempoDesplazamiento = tiempoMensajeFinal2; // Cuánto tiempo se desplazará
        float t = 0f;

        while (t < tiempoDesplazamiento)
        {
            t += Time.deltaTime;
            mensajeFinal2.transform.position = Vector3.Lerp(posicionOriginal, posicionFinal, t / tiempoDesplazamiento);
            yield return null;
        }

        // Desaparecer el segundo mensaje (créditos) con fade-out
        yield return StartCoroutine(FadeOut(mensajeFinal2, tiempoFade));

        // Mostrar el tercer mensaje (último mensaje) con fade-in
        mensajeFinal3.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn(mensajeFinal3, tiempoFade));

        // Esperar el tiempo que debe permanecer visible el último mensaje
        yield return new WaitForSeconds(tiempoMensajeFinal1);

        // Desaparecer el tercer mensaje (último mensaje) con fade-out
        yield return StartCoroutine(FadeOut(mensajeFinal3, tiempoFade));

        // Cerrar el juego después de mostrar los mensajes
        Application.Quit();
    }

    // Coroutine para el efecto de fade in
    private IEnumerator FadeIn(TextMeshProUGUI texto, float tiempoFade)
    {
        float t = 0f;
        Color colorInicial = texto.color;
        colorInicial.a = 0f;
        texto.color = colorInicial;

        while (t < tiempoFade)
        {
            t += Time.deltaTime;
            colorInicial.a = Mathf.Lerp(0f, 1f, t / tiempoFade);
            texto.color = colorInicial;
            yield return null;
        }
    }

    // Coroutine para el efecto de fade out
    private IEnumerator FadeOut(TextMeshProUGUI texto, float tiempoFade)
    {
        float t = 0f;
        Color colorFinal = texto.color;
        while (t < tiempoFade)
        {
            t += Time.deltaTime;
            colorFinal.a = Mathf.Lerp(1f, 0f, t / tiempoFade);
            texto.color = colorFinal;
            yield return null;
        }
        texto.gameObject.SetActive(false); // Desactivar después del fade out
    }

    // Coroutine para el efecto de fade in y fade out combinado
    private IEnumerator FadeInOut(TextMeshProUGUI texto, float tiempoMostrar, float tiempoFade)
    {
        yield return StartCoroutine(FadeIn(texto, tiempoFade));
        yield return new WaitForSeconds(tiempoMostrar);
        yield return StartCoroutine(FadeOut(texto, tiempoFade));
    }
}
