using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFade : MonoBehaviour
{
    public AudioSource musicSource; // Asigna el Audio Source aquí
    public float fadeDuration = 2f; // Duración del fade

    private void Start()
    {
        StartCoroutine(FadeInMusic());
    }

    private IEnumerator FadeInMusic()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0, 1, timer / fadeDuration); // Lerp del volumen
            yield return null;
        }

        musicSource.volume = 1; // Asegurarse de que el volumen sea 1 al final
    }
}
