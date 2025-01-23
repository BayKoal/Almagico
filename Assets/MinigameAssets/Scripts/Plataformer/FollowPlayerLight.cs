using UnityEngine;

public class FollowPlayerLight : MonoBehaviour
{
    public Transform player;  
    public Vector3 offset;    

    void Start()
    {
        if (player == null)
        {
            Debug.LogWarning("El jugador no está asignado en el Inspector.");
            return;
        }

        offset = new Vector3(0, 1f, -1f); 
    }

    void LateUpdate()
    {
        if (player != null)
        {

            transform.position = player.position + offset;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}