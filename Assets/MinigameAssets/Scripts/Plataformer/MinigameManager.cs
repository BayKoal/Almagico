using UnityEngine;
using TMPro;  // Necesario para trabajar con TextMesh Pro

public class MinigameManager : MonoBehaviour
{
    public Transform[] checkpoints; 
    private Transform currentCheckpoint; 

    public int totalTorches = 5;
    public int listTorches = 0;  

    public GameObject redLight;
    public GameObject greenLight;

    public TextMeshProUGUI torchCounterText; 

    void Start()
    {
     
        currentCheckpoint = checkpoints[0]; 

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = currentCheckpoint.position; // Mueve al jugador al primer checkpoint
        }

        redLight.SetActive(true);
        greenLight.SetActive(false);

        UpdateTorchCounter();
    }

    public void TorchList()
    {
        listTorches++;  

        // Actualiza el contador de antorchas en la UI
        UpdateTorchCounter();

        // Si el jugador ha encendido todas las antorchas, completa el minijuego
        if (listTorches >= totalTorches)
        {
            CompleteMinigame();
        }
    }

    private void CompleteMinigame()
    {
        //Debug.Log("¡Minijuego completado!");

        redLight.SetActive(false);
        greenLight.SetActive(true);
    }

    public void SaveCheckpoint(Transform checkpointTransform)
    {
        currentCheckpoint = checkpointTransform; // Guarda el nuevo checkpoint al que llegó el jugador
    }

    public Transform GetCurrentCheckpoint()
    {
        return currentCheckpoint; // Devuelve el checkpoint actual
    }

    // Método para actualizar el contador de antorchas en la UI
    private void UpdateTorchCounter()
    {
        if (torchCounterText != null)
        {
            torchCounterText.text =  + listTorches + "/" + totalTorches;
        }
    }
}
