using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxLives = 5; 
    private int currentLives;

    private MinigameManager minigameManager;

    void Start()
    {
        currentLives = maxLives;
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

    public void TakeDamage()
    {
        currentLives--; 

        if (currentLives > 0)
        {
            Respawn();
        }
        else
        {
            GameOver();
        }
    }

    private void Respawn()
    {
        if (minigameManager != null)
        {
            transform.position = minigameManager.GetCurrentCheckpoint();
        }
        else
        {
            Debug.LogWarning("MinigameManager no encontrado.");
        }
    }

    private void GameOver()
    {
        Destroy(gameObject);
        Debug.Log("¡Game Over!");

    }
}