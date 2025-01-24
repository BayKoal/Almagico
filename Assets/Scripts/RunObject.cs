using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeductPointsOnTrigger : MonoBehaviour
{
    public int puntosADescontar = 1; // Cantidad de puntos a descontar por colisión

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisiona tiene el script de puntos
        PlayerPoints playerPoints = other.GetComponent<PlayerPoints>();
        if (playerPoints != null)
        {
            playerPoints.RestarPuntos(puntosADescontar);
            Debug.Log("Colisión con objeto. Puntos descontados: " + puntosADescontar);
        }
    }
}