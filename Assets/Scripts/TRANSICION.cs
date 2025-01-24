using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneFadeManager : MonoBehaviour
{
    public Image fadeImage; // La imagen de fade que debe estar en el Canvas
    public float fadeDuration = 1f; // Duración del fade
    private bool isFading = false;

    private void Start()
    {
        // Asegúrate de que la imagen de fade comience completamente negra
        fadeImage.color = new Color(0f, 0f, 0f, 1f);
    }

    // Método para cargar una nueva escena con fade out y fade in
    public void LoadSceneWithFade(string sceneName)
    {
        if (isFading) return; // Evita que inicie múltiples fades al mismo tiempo

        StartCoroutine(FadeOutAndIn(sceneName));
    }

    // Corutina que maneja el fade de salida y entrada
    private IEnumerator FadeOutAndIn(string sceneName)
    {
        isFading = true;

        // Fade out
        yield return StartCoroutine(Fade(1f));

        // Cargar la escena
        SceneManager.LoadScene(sceneName);

        // Esperar a que la escena se cargue
        yield return null;

        // Fade in
        yield return StartCoroutine(Fade(0f));

        isFading = false;
    }

    // Corutina para hacer el fade (de 0 a 1 o de 1 a 0)
    private IEnumerator Fade(float targetAlpha)
    {
        float timeElapsed = 0f;
        float startingAlpha = fadeImage.color.a;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startingAlpha, targetAlpha, timeElapsed / fadeDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetAlpha);
    }
}

