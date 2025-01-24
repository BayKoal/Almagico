using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSubwaySurfers : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento hacia adelante
    public float velocidadLateral = 5f; // Velocidad de movimiento lateral

    private Rigidbody rb;
    public Animator animaciones;

    private bool canMove = true; // Variable para controlar si el jugador puede moverse

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animaciones = GetComponent<Animator>();
    }

    void Update()
    {
        // Solo permitir movimiento si canMove es verdadero
        if (!canMove) return;

        // Movimiento hacia adelante
        Vector3 movimientoFrontal = Vector3.forward * velocidad * Time.deltaTime;

        // Movimiento lateral
        float movimientoHorizontal = Input.GetAxis("Horizontal") * velocidadLateral * Time.deltaTime;
        Vector3 movimientoLateral = Vector3.right * movimientoHorizontal;

        // Combinar movimientos
        Vector3 movimiento = movimientoFrontal + movimientoLateral;
        rb.MovePosition(rb.position + movimiento);

        // Actualizar animaciones con base en el movimiento
        ActualizarAnimaciones(movimientoHorizontal, movimientoFrontal);
    }

    private void ActualizarAnimaciones(float movimientoHorizontal, Vector3 movimientoFrontal)
    {
        // Animación de movimiento hacia adelante
        if (movimientoFrontal != Vector3.zero)
        {
            animaciones.SetBool("run", true);
            animaciones.SetBool("runBack", false);
        }
        else
        {
            animaciones.SetBool("run", false);
        }

        // Animación de correr hacia la derecha
        if (movimientoHorizontal > 0)
        {
            animaciones.SetBool("runR", true);
            animaciones.SetBool("runL", false);
        }
        // Animación de correr hacia la izquierda
        else if (movimientoHorizontal < 0)
        {
            animaciones.SetBool("runL", true);
            animaciones.SetBool("runR", false);
        }
        else
        {
            animaciones.SetBool("runL", false);
            animaciones.SetBool("runR", false);
        }

        // Detener animación de correr hacia atrás
        if (movimientoHorizontal == 0)
        {
            animaciones.SetBool("runBack", false);
        }
    }

    // Método que se llama cuando el jugador colisiona con algo
    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si la colisión es con el objeto de tipo "village"
        if (collision.gameObject.CompareTag("Finish"))
        {
            DetenerMovimiento(); // Detener el movimiento si colisiona con "village"
        }
    }

    // Método que se llama cuando el jugador entra en un área de trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            DetenerMovimiento(); // Detener el movimiento si entra en el trigger de "village"
        }
    }

    // Método para detener el movimiento del jugador
    private void DetenerMovimiento()
    {
        canMove = false; // Dejar de mover al jugador
        animaciones.SetBool("run", false); // Detener la animación de correr
        animaciones.SetBool("runR", false); // Detener la animación de correr a la derecha
        animaciones.SetBool("runL", false); // Detener la animación de correr a la izquierda
    }

    // Método para reanudar el movimiento del jugador (puedes llamarlo desde otro script o evento)
    public void ReanudarMovimiento()
    {
        canMove = true; // Activar el movimiento nuevamente
    }
}