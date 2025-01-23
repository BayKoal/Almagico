using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MinigameManager manager = FindObjectOfType<MinigameManager>();
            if (manager != null)
            {
                manager.SaveCheckpoint(transform); // Guarda el transform del checkpoint actual
            }
        }
    }
}
