using UnityEngine;

public class FireflyMovement : MonoBehaviour
{
    // Configuración
    public float moveSpeedMin = 0.1f;    // Velocidad mínima de movimiento
    public float moveSpeedMax = 1f;      // Velocidad máxima de movimiento
    public float moveDelayMin = 2f;      // Tiempo mínimo entre movimientos
    public float moveDelayMax = 5f;      // Tiempo máximo entre movimientos
    public float maxDistance = 5f;       // Máxima distancia desde la posición inicial
    public float waveAmplitude = 0.5f;   // Amplitud de la onda (cuánto se moverá arriba y abajo)
    public float waveFrequency = 1f;     // Frecuencia de la onda (la rapidez con la que se mueve arriba y abajo)
    public float rotationSpeed = 2f;     // Velocidad para suavizar la rotación

    private Vector3 initialPosition;     // Posición inicial de la luciérnaga
    private Vector3 targetPosition;      // Posición objetivo hacia donde se moverá
    private float moveTimer = 0f;        // Temporizador para el próximo movimiento
    private float waveTimer = 0f;        // Temporizador para la onda
    private float moveSpeed;             // Velocidad actual de la luciérnaga

    void Start()
    {
        // Guardamos la posición inicial
        initialPosition = transform.position;
        targetPosition = transform.position;

        // Asignar un tiempo de movimiento aleatorio
        moveTimer = Random.Range(moveDelayMin, moveDelayMax);

        // Asignar una velocidad aleatoria para cada luciérnaga
        moveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
    }

    void Update()
    {
        // Controlar el tiempo para decidir cuándo generar un nuevo destino
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0f)
        {
            GenerateNewTarget();
            // Resetear el temporizador con un valor aleatorio
            moveTimer = Random.Range(moveDelayMin, moveDelayMax);
        }

        // Mover la luciérnaga hacia la posición objetivo con movimiento ondulante
        MoveToTargetWithWaves();
    }

    void GenerateNewTarget()
    {
        // Generar una nueva posición dentro del rango permitido
        float offsetX = Random.Range(-maxDistance, maxDistance);
        float offsetY = Random.Range(-maxDistance, maxDistance);
        float offsetZ = Random.Range(-maxDistance, maxDistance);

        // Crear la nueva posición objetivo
        targetPosition = initialPosition + new Vector3(offsetX, offsetY, offsetZ);

        // Asegurarse de que la luciérnaga no salga de los límites
        targetPosition = ClampToBounds(targetPosition);
    }

    void MoveToTargetWithWaves()
    {
        // Generar el movimiento ondulante (sin rotación)
        waveTimer += Time.deltaTime * waveFrequency;
        float waveOffsetY = Mathf.Sin(waveTimer) * waveAmplitude;  // Movimiento vertical ondulante
        float waveOffsetX = Mathf.Cos(waveTimer) * waveAmplitude;  // Movimiento horizontal ondulante

        // Aplicar el movimiento ondulante al transform
        Vector3 waveMovement = new Vector3(waveOffsetX, waveOffsetY, 0);

        // Mover la luciérnaga hacia la posición objetivo con el movimiento ondulante
        transform.position = Vector3.MoveTowards(transform.position, targetPosition + waveMovement, moveSpeed * Time.deltaTime);

        // Ajustar la rotación hacia la posición objetivo
        Vector3 direction = targetPosition + waveMovement - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    Vector3 ClampToBounds(Vector3 position)
    {
        // Asegurar que la posición objetivo esté dentro de los límites
        float clampedX = Mathf.Clamp(position.x, initialPosition.x - maxDistance, initialPosition.x + maxDistance);
        float clampedY = Mathf.Clamp(position.y, initialPosition.y - maxDistance, initialPosition.y + maxDistance);
        float clampedZ = Mathf.Clamp(position.z, initialPosition.z - maxDistance, initialPosition.z + maxDistance);

        return new Vector3(clampedX, clampedY, clampedZ);
    }
}
