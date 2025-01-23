using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class LobbyController : MonoBehaviour
{
    public GameObject[] npcs; // Lista de NPCs en la escena

    private void Start()
    {
        // Destruir NPCs completados
        foreach (var npc in npcs)
        {
            if (GameManager.Instance.npcsCompletados.Contains(npc.name))
            {
                Destroy(npc);
            }
        }

        // Mostrar cartas recolectadas
        Debug.Log("Cartas recolectadas: " + GameManager.Instance.cartasRecolectadas);
    }
}
