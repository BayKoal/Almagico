using System.Collections;
using UnityEngine;

public class MusicTransition : MonoBehaviour
{
    public AudioSource music1; // Música para el primer escenario (principal)
    public AudioSource music2; // Música para el segundo escenario
    private AudioSource currentMusic; // Música que está sonando actualmente

    private bool isMusic1Playing = true; // Variable para verificar cuál música está sonando actualmente

    private void Start()
    {
        // Al iniciar, la música por defecto es la música1
        currentMusic = music1;
        currentMusic.Play();
        currentMusic.volume = 1f; // Asegurarse que la música inicial esté al volumen máximo
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra es el jugador
        if (other.CompareTag("Player"))
        {
            // Si la música que suena es la primera, cambiamos a la segunda
            if (isMusic1Playing)
            {
                StartCoroutine(FadeMusic(currentMusic, music2));
                currentMusic = music2;
                isMusic1Playing = false;
            }
            // Si la música que suena es la segunda, cambiamos a la primera
            else
            {
                StartCoroutine(FadeMusic(currentMusic, music1));
                currentMusic = music1;
                isMusic1Playing = true;
            }
        }
    }

    // Coroutine para hacer un fade entre dos músicas
    IEnumerator FadeMusic(AudioSource musicToStop, AudioSource musicToPlay)
    {
        float fadeTime = 2f; // Tiempo para hacer el fade (ajústalo según lo que desees)

        // Fade out de la música actual
        float startVolume = musicToStop.volume;
        while (musicToStop.volume > 0)
        {
            musicToStop.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        musicToStop.Stop();

        // Fade in de la nueva música
        musicToPlay.Play();
        musicToPlay.volume = 0f; // Comienza en volumen cero
        while (musicToPlay.volume < 1)
        {
            musicToPlay.volume += Time.deltaTime / fadeTime; // Aumenta el volumen poco a poco
            yield return null;
        }
        musicToPlay.volume = 1f; // Asegurarse de que termine al máximo volumen
    }
}

