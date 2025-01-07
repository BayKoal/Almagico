using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Vector2 angle = new Vector2(180f, 0f);  // Inicializa la cámara detrás del jugador
    public Transform follow;  // Referencia al jugador (Transform)
    public float distanceFollow;  // Distancia de seguimiento de la cámara
    public float rotationSpeed = 3f;  // Velocidad de rotación de la cámara
    private const float MIN_ANGLE = -80f;  // Ángulo mínimo de rotación vertical
    private const float MAX_ANGLE = 80f;   // Ángulo máximo de rotación vertical
    private bool isRotating = false;  // Variable para detectar si se está rotando
    private float mouseVal = 0f;
    private float mouseSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // Coloca la cámara detrás del jugador
        Vector3 initialPosition = follow.position - follow.forward * distanceFollow;
        initialPosition.y += 2f;  // Ajusta la altura de la cámara
        transform.position = initialPosition;

        // Asegúrate de que la cámara mira al jugador
        transform.LookAt(follow);
    }

    // Update is called once per frame
    void Update()
    {
        float upAndDown = Input.GetAxis("Mouse ScrollWheel");
        
        if (upAndDown != 0)
        {
            mouseVal -= upAndDown * mouseSpeed;
            mouseVal = Mathf.Clamp(mouseVal, 1, 14);
            distanceFollow = mouseVal;
        }

        



        // Activar/desactivar la rotación con clic derecho
        if (Input.GetMouseButtonDown(1))  // 1 es el botón derecho del ratón
        {
            isRotating = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            // Obtén el movimiento del ratón
            float hor = Input.GetAxis("Mouse X");
            float ver = Input.GetAxis("Mouse Y");

            // Actualiza los ángulos de rotación
            angle.x += hor * rotationSpeed;
            angle.y -= ver * rotationSpeed;

            // Limita la rotación vertical para evitar que la cámara se voltee
            angle.y = Mathf.Clamp(angle.y, MIN_ANGLE, MAX_ANGLE);
        }
    }

    // LateUpdate se llama después de que todos los objetos hayan sido actualizados
    void LateUpdate()
    {
        // Convierte los ángulos en radianes
        float radX = angle.x * Mathf.Deg2Rad;
        float radY = angle.y * Mathf.Deg2Rad;

        // Calcula la posición de la cámara
        Vector3 orbit = new Vector3(Mathf.Cos(radY) * Mathf.Sin(radX), Mathf.Sin(radY), Mathf.Cos(radY) * Mathf.Cos(radX));

        // Coloca la cámara detrás del jugador
        transform.position = follow.position + orbit * distanceFollow;

        // Asegura que la cámara siempre mire al jugador
        transform.rotation = Quaternion.LookRotation(follow.position - transform.position);

        // Rota al jugador en función de la rotación de la cámara
        if (isRotating)
        {
            // Rota al jugador hacia la dirección de la cámara (en el eje Y)
            Quaternion playerRotation = Quaternion.Euler(0, angle.x, 0);  // Mantén la rotación solo en el eje Y
            follow.rotation = Quaternion.Slerp(follow.rotation, playerRotation, Time.deltaTime * rotationSpeed);  // Rota suavemente
        }
    }
}
