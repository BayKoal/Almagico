using UnityEngine;

public class FireflyMovement : MonoBehaviour
{
    // Configuraci�n
    public float moveSpeedMin = 0.1f;    // Velocidad m�nima de movimiento
    public float moveSpeedMax = 1f;      // Velocidad m�xima de movimiento
    public float moveDelayMin = 2f;      // Tiempo m�nimo entre movimientos
    public float moveDelayMax = 5f;      // Tiempo m�ximo entre movimientos
    public float maxDistance = 5f;       // M�xima distancia desde la posici�n inicial
    public float waveAmplitude = 0.5f;   // Amplitud de la onda (cu�nto se mover� arriba y abajo)
    public float waveFrequency = 1f;     // Frecuencia de la onda (la rapidez con la que se mueve arriba y abajo)
    public float rotationSpeed = 2f;     // Velocidad para suavizar la rotaci�n

    private Vector3 initialPosition;     // Posici�n inicial de la luci�rnaga
    private Vector3 targetPosition;      // Posici�n objetivo hacia donde se mover�
    private float moveTimer = 0f;        // Temporizador para el pr�ximo movimiento
    private float waveTimer = 0f;        // Temporizador para la onda
    private float moveSpeed;             // Velocidad actual de la luci�rnaga

    void Start()
    {
        // Guardamos la posici�n inicial
        initialPosition = transform.position;
        targetPosition = transform.position;

        // Asignar un tiempo de movimiento aleatorio
        moveTimer = Random.Range(moveDelayMin, moveDelayMax);

        // Asignar una velocidad aleatoria para cada luci�rnaga
        moveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
    }

    void Update()
    {
        // Controlar el tiempo para decidir cu�ndo generar un nuevo destino
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0f)
        {
            GenerateNewTarget();
            // Resetear el temporizador con un valor aleatorio
            moveTimer = Random.Range(moveDelayMin, moveDelayMax);
        }

        // Mover la luci�rnaga hacia la posici�n objetivo con movimiento ondulante
        MoveToTargetWithWaves();
    }

    void GenerateNewTarget()
    {
        // Generar una nueva posici�n dentro del rango permitido
        float offsetX = Random.Range(-maxDistance, maxDistance);
        float offsetY = Random.Range(-maxDistance, maxDistance);
        float offsetZ = Random.Range(-maxDistance, maxDistance);

        // Crear la nueva posici�n objetivo
        targetPosition = initialPosition + new Vector3(offsetX, offsetY, offsetZ);

        // Asegurarse de que la luci�rnaga no salga de los l�mites
        targetPosition = ClampToBounds(targetPosition);
    }

    void MoveToTargetWithWaves()
    {
        // Generar el movimiento ondulante (sin rotaci�n)
        waveTimer += Time.deltaTime * waveFrequency;
        float waveOffsetY = Mathf.Sin(waveTimer) * waveAmplitude;  // Movimiento vertical ondulante
        float waveOffsetX = Mathf.Cos(waveTimer) * waveAmplitude;  // Movimiento horizontal ondulante

        // Aplicar el movimiento ondulante al transform
        Vector3 waveMovement = new Vector3(waveOffsetX, waveOffsetY, 0);

        // Mover la luci�rnaga hacia la posici�n objetivo con el movimiento ondulante
        transform.position = Vector3.MoveTowards(transform.position, targetPosition + waveMovement, moveSpeed * Time.deltaTime);

        // Ajustar la rotaci�n hacia la posici�n objetivo
        Vector3 direction = targetPosition + waveMovement - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    Vector3 ClampToBounds(Vector3 position)
    {
        // Asegurar que la posici�n objetivo est� dentro de los l�mites
        float clampedX = Mathf.Clamp(position.x, initialPosition.x - maxDistance, initialPosition.x + maxDistance);
        float clampedY = Mathf.Clamp(position.y, initialPosition.y - maxDistance, initialPosition.y + maxDistance);
        float clampedZ = Mathf.Clamp(position.z, initialPosition.z - maxDistance, initialPosition.z + maxDistance);

        return new Vector3(clampedX, clampedY, clampedZ);
    }
}
