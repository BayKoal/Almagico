using System.Linq;
using UnityEngine;

public class RecargaCordura : MonoBehaviour
{
    public float distanciaDeInteraccion = 3f; // Distancia a la que el jugador puede interactuar con el objeto.

    private void Update()
    {
        // Verificar si el jugador está cerca del objeto.
        if (EsJugadorCercano() && Input.GetKeyDown(KeyCode.E))
        {
            RecargarCorduraDelJugador();
        }
    }

    private bool EsJugadorCercano()
    {
        // Verificar si el jugador está cerca del objeto (dentro de la distancia de interacción).
        Collider playerCollider = Physics.OverlapSphere(transform.position, distanciaDeInteraccion).FirstOrDefault(collider => collider.CompareTag("Player"));
        return playerCollider != null;
    }

    private void RecargarCorduraDelJugador()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            CorduraController corduraController = player.GetComponent<CorduraController>();
            if (corduraController != null)
            {
                corduraController.RecargarCordura();
                Debug.Log("Cordura recargada.");
            }
        }
    }
}
