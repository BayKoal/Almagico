using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    public float floatHeight = 0.5f;  // Altura del flotado
    public float floatSpeed = 2f;     // Velocidad del flotado
    private Vector3 startPosition;    // Posición inicial del objeto

    public int itemValue = 1;         // Valor que el jugador obtiene al recolectar

    private bool playerInRange = false; // ¿Está el jugador cerca del objeto?

    void Start()
    {
        // Guardar la posición inicial
        startPosition = transform.position;
    }

    void Update()
    {
        // Hacer que el objeto flote arriba y abajo
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPosition.x, startPosition.y + newY, startPosition.z);

        // Verificar si el jugador está cerca y presiona "E"
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Llamar a la función de recolectar el item en el PlayerUI
            FindObjectOfType<PlayerUI>().AddItem(itemValue);

            // Destruir el objeto para simular que ha sido recogido
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Si el jugador entra en el trigger, permitir que recoja el objeto
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Si el jugador sale del trigger, desactivar la posibilidad de recoger el objeto
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
