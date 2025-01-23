using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<string> cartasRecolectadas = new List<string>(); // Lista de cartas recolectadas
    public List<string> npcsCompletados = new List<string>(); // IDs de NPCs completados

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
