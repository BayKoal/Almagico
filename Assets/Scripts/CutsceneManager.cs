using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    public TextMeshProUGUI cutsceneText; // TextMeshPro para el texto
    public string[] messages; // Mensajes que se mostrarán
    public float fadeDuration = 1f; // Duración del fade-in y fade-out
    public string nextSceneName; // Nombre de la próxima escena

    private int currentMessageIndex = 0;
    private bool isTransitioning = false; // Para evitar múltiples clics rápidos

    void Start()
    {
        cutsceneText.alpha = 0; // Asegúrate de que el texto empiece invisible
        StartCoroutine(ShowMessage());
    }

    void Update()
    {
        // Detectar clic del jugador para avanzar
        if (Input.GetMouseButtonDown(0) && !isTransitioning) // Botón izquierdo del mouse
        {
            StartCoroutine(NextMessage());
        }
    }

    IEnumerator ShowMessage()
    {
        isTransitioning = true;
        yield return FadeText(1); // Fade-in
        isTransitioning = false;
    }

    IEnumerator NextMessage()
    {
        if (currentMessageIndex < messages.Length)
        {
            isTransitioning = true;

            // Fade-out del mensaje actual
            yield return FadeText(0);

            // Avanzar al siguiente mensaje
            currentMessageIndex++;
            if (currentMessageIndex < messages.Length)
            {
                cutsceneText.text = messages[currentMessageIndex];
                yield return FadeText(1); // Fade-in del nuevo mensaje
            }
            else
            {
                // Si no hay más mensajes, cargar la siguiente escena
                SceneManager.LoadScene(nextSceneName);
            }

            isTransitioning = false;
        }
    }

    IEnumerator FadeText(float targetAlpha)
    {
        float startAlpha = cutsceneText.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            cutsceneText.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        cutsceneText.alpha = targetAlpha; // Asegurar el valor final
    }
}
