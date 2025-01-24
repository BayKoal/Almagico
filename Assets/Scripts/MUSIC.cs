using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Para manejar el Slider

public class MusicTransition : MonoBehaviour
{
    public AudioSource music1; // Música para el primer escenario (principal)
    public AudioSource music2; // Música para el segundo escenario
    private AudioSource currentMusic; // Música que está sonando actualmente

    private bool isMusic1Playing = true; // Variable para verificar cuál música está sonando actualmente

    public Slider volumeSlider; // Slider para controlar el volumen
    private float musicVolume = 1f; // Almacenará el volumen actual de la música

    private void Start()
    {
        // Al iniciar, la música por defecto es la música1
        currentMusic = music1;
        currentMusic.Play();
        currentMusic.volume = volumeSlider.value; // Ajusta el volumen según el valor del slider

        // Asegúrate de que el slider esté configurado en un rango adecuado
        volumeSlider.onValueChanged.AddListener(UpdateVolume); // Escucha los cambios en el slider
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

    // Método para actualizar el volumen cuando el slider cambie
    private void UpdateVolume(float volume)
    {
        musicVolume = volume;
        if (currentMusic != null)
        {
            currentMusic.volume = musicVolume;
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
        while (musicToPlay.volume < musicVolume) // Usa el volumen actual del slider
        {
            musicToPlay.volume += Time.deltaTime / fadeTime; // Aumenta el volumen poco a poco
            yield return null;
        }
        musicToPlay.volume = musicVolume; // Asegurarse de que termine al volumen configurado
    }
}
