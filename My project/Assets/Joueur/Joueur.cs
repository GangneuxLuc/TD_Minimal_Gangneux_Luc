using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Joueur : MonoBehaviour
{
    [Header("Propriétés du joueur")]
    [SerializeField] private float speed = 1.0f;
   // [SerializeField] private GameObject joueur;
   // [SerializeField] private EnnemyClass ennemy;
    [SerializeField] private bool CanMove = true;
   // [SerializeField] private float range = 1f;
    private Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            

            movement.x = Input.GetAxis("Horizontal");
            transform.Translate(movement * speed * Time.deltaTime);
        }
        // else transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

      



   /* protected float CalculateDistanceXYPlane()
    {
        Vector2 V1 = new Vector2(joueur.transform.position.x, joueur.transform.position.y);
        Vector2 V2 = new Vector2(ennemy.transform.position.x, ennemy.transform.position.y);
        //Debug.Log("V1: " + V1 + " V2: " + V2);
        return Vector2.Distance(V1, V2);
    } */
}


