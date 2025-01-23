using UnityEngine;

public class Torch : MonoBehaviour
{
    public GameObject LightPoint;  
    private bool playerInRange = false;  
    private bool isLit = false; // Indica si la antorcha ya está encendida

    private MinigameManager minigameManager;

    private void Start()
    {
        minigameManager = FindObjectOfType<MinigameManager>();
    }

    public void Interact()
    {
        if (playerInRange && LightPoint != null && !isLit)
        {
            LightPoint.SetActive(true);
            isLit = true;
            Debug.Log("Antorcha encendida");
            minigameManager.TorchList();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            playerInRange = false;
        }
    }
}
