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
            // Calcular la dirección hacia el punto en movimiento
            Vector3 directionToTarget = targetPoint.position - tr.position;
            directionToTarget.y = 0;  // Mantener la rotación solo en el plano XZ (sin afectación vertical)

            // Calcular la rotación que debe tener el objeto para mirar al punto
            Quaternion rotation = Quaternion.LookRotation(directionToTarget);

            // Aplicar solo la rotación, sin cambiar la posición del objeto
            tr.rotation = rotation;
        }
    }
}
