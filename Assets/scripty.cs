using UnityEngine;

public class scripty : MonoBehaviour
{
    public Transform follow;  // El transform del jugador
    public float rotationSpeed = 3f;  // Velocidad de rotaci�n del objeto vac�o

    private Vector2 angle = new Vector2(180f, 0f);  // �ngulo inicial de la c�mara

    // Update se llama una vez por frame
    void Update()
    {
        // Detectar el movimiento del rat�n para rotar la c�mara
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        // Actualizar los �ngulos de rotaci�n
        angle.x += hor * rotationSpeed;
        angle.y -= ver * rotationSpeed;

        // Limitar el �ngulo vertical para evitar rotaciones extra�as
        angle.y = Mathf.Clamp(angle.y, -80f, 80f);

        // Convertir los �ngulos de grados a radianes
        float radX = angle.x * Mathf.Deg2Rad;
        float radY = angle.y * Mathf.Deg2Rad;

        // Calcular la rotaci�n en torno al eje Y (giro de la c�mara)
        Vector3 orbit = new Vector3(Mathf.Cos(radY) * Mathf.Sin(radX), Mathf.Sin(radY), Mathf.Cos(radY) * Mathf.Cos(radX));

        // Mover el objeto vac�o en funci�n de la c�mara (en el eje Y)
        transform.rotation = Quaternion.Euler(0, angle.x, 0);  // Solo rotaci�n en el eje Y (horizontal)
    }
}
