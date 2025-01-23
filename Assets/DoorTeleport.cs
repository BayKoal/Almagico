using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTeleport : MonoBehaviour
{
    public Transform puertaInicio;  // Asigna la Puerta Inicio en el Inspector
    private bool playerInRange = false;  // Para detectar si el jugador está dentro del rango de la "Puerta de regreso"
    private GameObject player;  // Referencia al jugador

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))  // Si el jugador está en rango y presiona "E"
        {
            Teleport();  // Llama al método Teleport para teletransportar al jugador
        }
    }

    private void Teleport()
    {
        // Si el jugador ha sido detectado, se teletransporta a la posición de la Puerta Inicio
        if (player != null && puertaInicio != null)
        {
            player.transform.position = puertaInicio.position;
            Debug.Log("Jugador teletransportado a la Puerta Inicio");
        }
    }

    // Cuando el jugador entra en el área de la Puerta de regreso (trigger)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Asegúrate de que el jugador tenga el tag "Player"
        {
            playerInRange = true;  // El jugador está dentro del área de interacción
            player = other.gameObject;  // Guarda la referencia al jugador
        }
    }

    // Cuando el jugador sale del área de la Puerta de regreso
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // Si el jugador sale del área
        {
            playerInRange = false;  // Deja de permitir la interacción
            player = null;  // Borra la referencia al jugador
        }
    }
}
