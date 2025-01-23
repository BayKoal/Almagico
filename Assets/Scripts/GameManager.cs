using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<string> cartasRecolectadas = new List<string>(); // Lista de cartas recolectadas
    public List<string> npcsCompletados = new List<string>(); // IDs de NPCs completados
    public int npcsTotal = 5; // Número total de NPCs en la escena (ajústalo según el caso)

    // Método para agregar NPC completados
    public void AddCompletedNPC(string npcID)
    {
        if (!npcsCompletados.Contains(npcID))
        {
            npcsCompletados.Add(npcID);
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}