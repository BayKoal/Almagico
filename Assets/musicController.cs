using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importa TextMeshPro

public class MusicVolumeControl : MonoBehaviour
{
    public Slider volumeSlider; // El slider de volumen
    public AudioSource music1; // Referencia a la m�sica 1
    public AudioSource music2; // Referencia a la m�sica 2
    public TextMeshProUGUI volumeText; // TextMeshPro para mostrar el volumen

    private AudioSource currentMusic; // La m�sica que est� sonando actualmente

    void Start()
    {
        // Establece la m�sica inicial (aseg�rate de que una est� sonando al inicio)
        currentMusic = music1.isPlaying ? music1 : music2;

        // Inicia el volumen del slider seg�n la m�sica actual
        volumeSlider.value = currentMusic.volume;

        // Establece el texto inicial del volumen
        UpdateVolumeText(currentMusic.volume);

        // Aseg�rate de que el slider tiene un valor entre 0 y 1
        volumeSlider.minValue = 0;
        volumeSlider.maxValue = 1;

        // Asocia el evento OnValueChanged con la funci�n OnVolumeChange
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
    }

    // Llamado cuando el usuario ajusta el slider
    public void OnVolumeChange(float value)
    {
        // Cambia el volumen de la m�sica actual seg�n el valor del slider
        if (currentMusic != null)
        {
            currentMusic.volume = value;
            // Actualiza el texto del volumen
            UpdateVolumeText(value);
        }
    }

    // M�todo para actualizar el texto del volumen
    private void UpdateVolumeText(float volume)
    {
        volumeText.text = $"Volumen: {Mathf.RoundToInt(volume * 100)}%";
    }

    // M�todo para cambiar la m�sica (deber�s llamarlo desde el c�digo que cambia las canciones)
    public void ChangeMusic(AudioSource newMusic)
    {
        // Det�n la m�sica actual
        if (currentMusic != null)
        {
            currentMusic.Stop();
        }

        // Cambia a la nueva m�sica
        currentMusic = newMusic;
        currentMusic.Play();

        // Establece el volumen al valor del slider actual
        currentMusic.volume = volumeSlider.value;

        // Actualiza el texto de volumen
        UpdateVolumeText(currentMusic.volume);
    }
}
