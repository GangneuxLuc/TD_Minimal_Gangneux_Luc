using UnityEngine;

public class CameraScript : MonoBehaviour // Script de caméra suivant le joueur
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 3f, -6f);
    public float mouseX;
    public float mouseY;
    public float smoothLerp = 0.1f;
  
   
    float RotationX;
    float RotationY;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    void LateUpdate() // Utilisation de LateUpdate pour s'assurer que la caméra suit le joueur après que celui-ci ait bougé
    {
        transform.position = Vector3.Lerp(transform.position, player.position + offset,smoothLerp);
        Quaternion rotation = Quaternion.Euler(RotationY, RotationX, 0f);
        transform.position = player.position + rotation * offset;


         //regarde le joueur (on peut ajuster la hauteur du point visé si nécessaire)
        transform.LookAt(player.position + Vector3.up * 1.5f);
       
    }

}