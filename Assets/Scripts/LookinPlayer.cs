using UnityEngine;

using TMPro;

public class TextLookAtPlayer : MonoBehaviour
{
    private Transform player; // Referencia al jugador

    private void Start()
    {
        // Encuentra el jugador en la escena
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player != null)
        {
            // Hacer que el texto mire hacia el jugador en el plano XZ
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.y = 0; // Evitar que mire hacia arriba o abajo (solo en el plano horizontal)

            Quaternion rotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = rotation;
        }
    }
}
