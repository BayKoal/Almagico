using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importa TextMeshPro

public class MusicVolumeControl : MonoBehaviour
{
    public Slider volumeSlider; // El slider de volumen
    public AudioSource music1; // Referencia a la música 1
    public AudioSource music2; // Referencia a la música 2
    public TextMeshProUGUI volumeText; // TextMeshPro para mostrar el volumen

    private AudioSource currentMusic; // La música que está sonando actualmente

    void Start()
    {
        // Establece la música inicial (asegúrate de que una esté sonando al inicio)
        currentMusic = music1.isPlaying ? music1 : music2;

        // Inicia el volumen del slider según la música actual
        volumeSlider.value = currentMusic.volume;

        // Establece el texto inicial del volumen
        UpdateVolumeText(currentMusic.volume);

        // Asegúrate de que el slider tiene un valor entre 0 y 1
        volumeSlider.minValue = 0;
        volumeSlider.maxValue = 1;

        // Asocia el evento OnValueChanged con la función OnVolumeChange
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
    }

    // Llamado cuando el usuario ajusta el slider
    public void OnVolumeChange(float value)
    {
        // Cambia el volumen de la música actual según el valor del slider
        if (currentMusic != null)
        {
            currentMusic.volume = value;
            // Actualiza el texto del volumen
            UpdateVolumeText(value);
        }
    }

    // Método para actualizar el texto del volumen
    private void UpdateVolumeText(float volume)
    {
        volumeText.text = $"Volumen: {Mathf.RoundToInt(volume * 100)}%";
    }

    // Método para cambiar la música (deberás llamarlo desde el código que cambia las canciones)
    public void ChangeMusic(AudioSource newMusic)
    {
        // Detén la música actual
        if (currentMusic != null)
        {
            currentMusic.Stop();
        }

        // Cambia a la nueva música
        currentMusic = newMusic;
        currentMusic.Play();

        // Establece el volumen al valor del slider actual
        currentMusic.volume = volumeSlider.value;

        // Actualiza el texto de volumen
        UpdateVolumeText(currentMusic.volume);
    }
}
