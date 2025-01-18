using UnityEngine;

public class LookAtMovingPoint : MonoBehaviour
{
    public Transform targetPoint;  // Referencia al punto en movimiento (puede ser un objeto o cualquier punto)
    private Transform tr;          // Transform del objeto que debe mirar al punto

    void Start()
    {
        // Obtener el transform del objeto al inicio
        tr = transform;
    }

    void Update()
    {
        if (targetPoint != null)
        {
            // Calcular la direcci�n hacia el punto en movimiento
            Vector3 directionToTarget = targetPoint.position - tr.position;
            directionToTarget.y = 0;  // Mantener la rotaci�n solo en el plano XZ (sin afectaci�n vertical)

            // Calcular la rotaci�n que debe tener el objeto para mirar al punto
            Quaternion rotation = Quaternion.LookRotation(directionToTarget);

            // Aplicar solo la rotaci�n, sin cambiar la posici�n del objeto
            tr.rotation = rotation;
        }
    }
}
