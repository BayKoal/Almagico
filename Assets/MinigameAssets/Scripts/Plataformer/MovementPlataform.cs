using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementPlataform : MonoBehaviour
{
    public float velocidad;
    public float fuerzaDeSalto;

    [SerializeField] LayerMask ground;
    private bool isGrounded;
    private Rigidbody rb;

    public Animator animaciones;
    public Transform head;

    private bool canMove = true;  // Variable para controlar el movimiento del jugador

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animaciones = GetComponent<Animator>();
    }

    void Update()
    {
        if (!canMove) return;  // Si no se puede mover, salir de la función Update

        RaycastHit hit;
        Ray detecion = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(detecion, out hit, 0.1f, ground))
        {
            isGrounded = true;
            animaciones.SetBool("IsGround", false);
            animaciones.SetBool("Jump", false);
        }
        else
        {
            isGrounded = false;
            animaciones.SetBool("IsGround", true);
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        bool espacio = Input.GetKeyDown(KeyCode.Space);


        // Animaciones para movimiento horizontal
        if (horizontal > 0f && animaciones != null)
        {
            animaciones.SetBool("runR", true);
        }
        else { animaciones.SetBool("runR", false); }

        if (horizontal < 0f && animaciones != null)
        {
            animaciones.SetBool("runL", true);
        }
        else { animaciones.SetBool("runL", false); }

        // Animación para salto
        if (espacio == true)
        {
            animaciones.SetBool("Jump", true);
        }

        // Movimiento restringido al eje horizontal (izquierda y derecha)
        Vector3 direccion = new Vector3(horizontal, 0f, 0f).normalized;

        if (direccion.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direccion.x, 0f, 0f));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        Vector3 movimiento = direccion * velocidad;
        rb.velocity = new Vector3(movimiento.x, rb.velocity.y, 0f); // Z fijo en 0 para movimiento 2D

        if (isGrounded && espacio)
        {
            rb.AddForce(Vector3.up * fuerzaDeSalto, ForceMode.Impulse);
        }
    }

    // Método para inmovilizar al jugador
    public void InmovilizarJugador()
    {
        canMove = false; // Desactivar el movimiento
    }

    // Método para reanudar el movimiento del jugador
    public void ReanudarMovimiento()
    {
        canMove = true; // Activar el movimiento
    }
}
