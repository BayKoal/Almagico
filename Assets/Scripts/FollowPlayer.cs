using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Vector2 angle = new Vector2(180f, 0f);  // Inicializa la c�mara detr�s del jugador
    public Transform follow;  // Referencia al jugador (Transform)
    public float distanceFollow;  // Distancia de seguimiento de la c�mara
    public float rotationSpeed = 3f;  // Velocidad de rotaci�n de la c�mara
    private const float MIN_ANGLE = -80f;  // �ngulo m�nimo de rotaci�n vertical
    private const float MAX_ANGLE = 80f;   // �ngulo m�ximo de rotaci�n vertical
    private bool isRotating = false;  // Variable para detectar si se est� rotando
    private float mouseVal = 0f;
    private float mouseSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // Coloca la c�mara detr�s del jugador
        Vector3 initialPosition = follow.position - follow.forward * distanceFollow;
        initialPosition.y += 2f;  // Ajusta la altura de la c�mara
        transform.position = initialPosition;

        // Aseg�rate de que la c�mara mira al jugador
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

        



        // Activar/desactivar la rotaci�n con clic derecho
        if (Input.GetMouseButtonDown(1))  // 1 es el bot�n derecho del rat�n
        {
            isRotating = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            // Obt�n el movimiento del rat�n
            float hor = Input.GetAxis("Mouse X");
            float ver = Input.GetAxis("Mouse Y");

            // Actualiza los �ngulos de rotaci�n
            angle.x += hor * rotationSpeed;
            angle.y -= ver * rotationSpeed;

            // Limita la rotaci�n vertical para evitar que la c�mara se voltee
            angle.y = Mathf.Clamp(angle.y, MIN_ANGLE, MAX_ANGLE);
        }
    }

    // LateUpdate se llama despu�s de que todos los objetos hayan sido actualizados
    void LateUpdate()
    {
        // Convierte los �ngulos en radianes
        float radX = angle.x * Mathf.Deg2Rad;
        float radY = angle.y * Mathf.Deg2Rad;

        // Calcula la posici�n de la c�mara
        Vector3 orbit = new Vector3(Mathf.Cos(radY) * Mathf.Sin(radX), Mathf.Sin(radY), Mathf.Cos(radY) * Mathf.Cos(radX));

        // Coloca la c�mara detr�s del jugador
        transform.position = follow.position + orbit * distanceFollow;

        // Asegura que la c�mara siempre mire al jugador
        transform.rotation = Quaternion.LookRotation(follow.position - transform.position);

        // Rota al jugador en funci�n de la rotaci�n de la c�mara
        if (isRotating)
        {
            // Rota al jugador hacia la direcci�n de la c�mara (en el eje Y)
            Quaternion playerRotation = Quaternion.Euler(0, angle.x, 0);  // Mant�n la rotaci�n solo en el eje Y
            follow.rotation = Quaternion.Slerp(follow.rotation, playerRotation, Time.deltaTime * rotationSpeed);  // Rota suavemente
        }
    }
}
