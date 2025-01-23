using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public Transform defaultCheckpoint;
    private Vector3 currentCheckpoint;

    public int totalTorches = 5;
    public int listTorches = 0;

    public GameObject redLight;
    public GameObject greenLight; 

    void Start()
    {
        // Configuración inicial de checkpoint
        if (PlayerPrefs.HasKey("CheckpointX"))
        {
            currentCheckpoint = new Vector3(
                PlayerPrefs.GetFloat("CheckpointX"),
                PlayerPrefs.GetFloat("CheckpointY"),
                PlayerPrefs.GetFloat("CheckpointZ")
            );
        }
        else
        {
            currentCheckpoint = defaultCheckpoint.position;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = currentCheckpoint;
        }

        // Configuración inicial de luces
        redLight.SetActive(true);
        greenLight.SetActive(false);
    }

    public void TorchList()
    {
        listTorches++;
        Debug.Log("Antorchas encendidas: " + listTorches + "/" + totalTorches);

        if (listTorches >= totalTorches)
        {
            CompleteMinigame();
        }
    }

    private void CompleteMinigame()
    {
        Debug.Log("¡Minijuego completado!");

        // Cambiar las luces al completar el minijuego
        redLight.SetActive(false);
        greenLight.SetActive(true);
    }

    public void SaveCheckpoint(Vector3 checkpointPosition)
    {
        PlayerPrefs.SetFloat("CheckpointX", checkpointPosition.x);
        PlayerPrefs.SetFloat("CheckpointY", checkpointPosition.y);
        PlayerPrefs.SetFloat("CheckpointZ", checkpointPosition.z);

        currentCheckpoint = checkpointPosition;
    }

    public Vector3 GetCurrentCheckpoint()
    {
        return currentCheckpoint;
    }
}
