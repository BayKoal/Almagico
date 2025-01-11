

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float velocidad;
    public float fuerzaDeSalto;

    [SerializeField] LayerMask ground;
    private bool isGrounded;
    private Rigidbody rb;

    public Animator animaciones;
    public Transform head;  // El objeto vac�o que sigue la c�mara

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animaciones = GetComponent<Animator>();
    }

    void Update()
    {
        // Verifica si el jugador est� tocando el suelo
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

        // Obt�n las entradas del jugador (movimiento horizontal y vertical)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool espacio = Input.GetKeyDown(KeyCode.Space);
        bool correr = Input.GetKeyDown(KeyCode.LeftShift);
        bool noCorrer = Input.GetKeyUp(KeyCode.LeftShift);

        // Actualizaci�n de animaciones
        if (horizontal > 0f && animaciones != null)
        {
            animaciones.SetBool("runR", true);
        }
        else { animaciones.SetBool("runR", false); }

        if (horizontal < 0f && animaciones != null)
        {
            animaciones.SetBool("runL", true);
        } else { animaciones.SetBool("runL", false); }

        if (vertical < 0f && animaciones != null && horizontal > 0f)
        {
            animaciones.SetBool("runBL", true);
        } else { animaciones.SetBool("runBL", false); }

        if (vertical > 0f && animaciones != null)
        {
            animaciones.SetBool("run", true);
        } else { animaciones.SetBool("run", false); }

        if (vertical < 0f && animaciones != null)
        {
            animaciones.SetBool("runBack", true);
        }
        else
        {
            animaciones.SetBool("runBack", false);
        }

        
        if (espacio == true) 
        {
            animaciones.SetBool("Jump", true);
           
        }
        if (correr)
        {
            animaciones.SetBool("Sprint", true);
            velocidad = 8f;
        }
        else if (noCorrer)
        {
            velocidad = 5f;
            animaciones.SetBool("Sprint", false);
        }
      
        // Movimiento basado en la direcci�n del objeto vac�o (head) con respecto a la c�mara
        Vector3 direccion = (head.forward * vertical + head.right * horizontal).normalized;

        // Aseg�rate de que la direcci�n sea v�lida
        if (direccion.magnitude > 0.1f)
        {
            direccion.y = 0f; // Mantener en el plano X-Z

            // Rotar el personaje solo si se mueve hacia adelante o lateralmente
            if (vertical > 0 || horizontal != 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direccion);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }

        // Aplicar el movimiento al Rigidbody
        Vector3 movimiento = direccion * velocidad;
        rb.velocity = new Vector3(movimiento.x, rb.velocity.y, movimiento.z);

        // Salto del jugador
        if (isGrounded && espacio)
        {
            rb.AddForce(Vector3.up * fuerzaDeSalto, ForceMode.Impulse);

        }
     
    }
}