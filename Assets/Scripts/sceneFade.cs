using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Necesaria para IEnumerator y corrutinas

public class SceneFade : MonoBehaviour
{
    public Image fadeImage; // Asigna aquí el Panel de la UI (Image o cualquier UI que utilices)
    public float fadeDuration = 2f; // Duración del fade

    private void Start()
    {
        // Asegura que el panel esté activo al comenzar
        fadeImage.gameObject.SetActive(true);

        // Inicia el fade de negro a visible
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float timer = 0f;

        // Comienza con el panel completamente negro
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);

        // Fade de opaco (1) a transparente (0)
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, Mathf.Lerp(1, 0, timer / fadeDuration));
            yield return null;
        }

        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0f); // Asegurarse de que el alpha sea 0 al final
        fadeImage.gameObject.SetActive(false); // Desactiva el panel una vez el fade se complete
    }
}
