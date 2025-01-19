using System.Collections;
using UnityEngine;

public class MusicTransition : MonoBehaviour
{
    public AudioSource music1; // M�sica para el primer escenario (principal)
    public AudioSource music2; // M�sica para el segundo escenario
    private AudioSource currentMusic; // M�sica que est� sonando actualmente

    private bool isMusic1Playing = true; // Variable para verificar cu�l m�sica est� sonando actualmente

    private void Start()
    {
        // Al iniciar, la m�sica por defecto es la m�sica1
        currentMusic = music1;
        currentMusic.Play();
        currentMusic.volume = 1f; // Asegurarse que la m�sica inicial est� al volumen m�ximo
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra es el jugador
        if (other.CompareTag("Player"))
        {
            // Si la m�sica que suena es la primera, cambiamos a la segunda
            if (isMusic1Playing)
            {
                StartCoroutine(FadeMusic(currentMusic, music2));
                currentMusic = music2;
                isMusic1Playing = false;
            }
            // Si la m�sica que suena es la segunda, cambiamos a la primera
            else
            {
                StartCoroutine(FadeMusic(currentMusic, music1));
                currentMusic = music1;
                isMusic1Playing = true;
            }
        }
    }

    // Coroutine para hacer un fade entre dos m�sicas
    IEnumerator FadeMusic(AudioSource musicToStop, AudioSource musicToPlay)
    {
        float fadeTime = 2f; // Tiempo para hacer el fade (aj�stalo seg�n lo que desees)

        // Fade out de la m�sica actual
        float startVolume = musicToStop.volume;
        while (musicToStop.volume > 0)
        {
            musicToStop.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        musicToStop.Stop();

        // Fade in de la nueva m�sica
        musicToPlay.Play();
        musicToPlay.volume = 0f; // Comienza en volumen cero
        while (musicToPlay.volume < 1)
        {
            musicToPlay.volume += Time.deltaTime / fadeTime; // Aumenta el volumen poco a poco
            yield return null;
        }
        musicToPlay.volume = 1f; // Asegurarse de que termine al m�ximo volumen
    }
}

