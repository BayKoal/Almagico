using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MinigameManager minigameManager;
    

    void Start()
    {
        minigameManager = FindObjectOfType<MinigameManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void Interact()
    {
        Torch[] torches = FindObjectsOfType<Torch>();
        foreach (Torch torch in torches)
        {
            if (torch != null)
            {
                torch.Interact();
            }
        }
    }

    // Método para reiniciar el jugador en el checkpoint actual
    public void Respawn()
    {
        if (minigameManager != null)
        {
            transform.position = minigameManager.GetCurrentCheckpoint().position; // Respawn en el checkpoint guardado
        }
        else
        {
            Debug.LogWarning("MinigameManager no encontrado.");
        }
    }

    // Método para manejar el game over (por si se necesita en algún momento)
    private void GameOver()
    {
        Destroy(gameObject);
        Debug.Log("¡Game Over!");
    }
}
