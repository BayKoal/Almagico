using UnityEngine;

public class MusicTransition : MonoBehaviour
{
    public AudioSource music1; // Música para el primer escenario
    public AudioSource music2; // Música para el segundo escenario

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra es el jugador
        if (other.CompareTag("Player"))
        {
            if (gameObject.name == "songOne")
            {
                // Cambiar a la música del segundo escenario
                music1.Stop();
                music2.Play();
            }
            else if (gameObject.name == "songTwo")
            {
                // Cambiar a la música del primer escenario
                music2.Stop();
                music1.Play();
            }
        }
    }
}
