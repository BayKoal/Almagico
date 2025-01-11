using UnityEngine;

public class scripty : MonoBehaviour
{
    public Transform follow;  // El transform del jugador
    public float rotationSpeed = 3f;  // Velocidad de rotación del objeto vacío

    private Vector2 angle = new Vector2(180f, 0f);  // Ángulo inicial de la cámara

    // Update se llama una vez por frame
    void Update()
    {
        // Detectar el movimiento del ratón para rotar la cámara
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        // Actualizar los ángulos de rotación
        angle.x += hor * rotationSpeed;
        angle.y -= ver * rotationSpeed;

        // Limitar el ángulo vertical para evitar rotaciones extrañas
        angle.y = Mathf.Clamp(angle.y, -80f, 80f);

        // Convertir los ángulos de grados a radianes
        float radX = angle.x * Mathf.Deg2Rad;
        float radY = angle.y * Mathf.Deg2Rad;

        // Calcular la rotación en torno al eje Y (giro de la cámara)
        Vector3 orbit = new Vector3(Mathf.Cos(radY) * Mathf.Sin(radX), Mathf.Sin(radY), Mathf.Cos(radY) * Mathf.Cos(radX));

        // Mover el objeto vacío en función de la cámara (en el eje Y)
        transform.rotation = Quaternion.Euler(0, angle.x, 0);  // Solo rotación en el eje Y (horizontal)
    }
}
