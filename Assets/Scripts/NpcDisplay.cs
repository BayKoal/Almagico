using UnityEngine;
using TMPro;

public class NpcDisplay : MonoBehaviour
{
    public TextMeshProUGUI npcStatusText; // Referencia al TMP
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;

        if (npcStatusText == null)
        {
            Debug.LogError("TextMeshProUGUI no está asignado en el Inspector.");
            return;
        }

        UpdateNPCStatusText();
    }

    private void Update()
    {
        UpdateNPCStatusText();
    }

    private void UpdateNPCStatusText()
    {
        if (gameManager != null)
        {
            int completedNPCs = gameManager.npcsCompletados.Count;
            int totalNPCs = gameManager.npcsTotal;
            npcStatusText.text = $"Misiones {completedNPCs}/{totalNPCs}";
        }
    }
}
