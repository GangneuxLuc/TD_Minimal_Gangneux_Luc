using UnityEngine;

public class Oeuf : MonoBehaviour // Script pour le comportement de l'œuf lancé par le poulet
{
    float speed = 2f;
    int AttackDmg = 2;
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime); // Déplacement de l'œuf vers la gauche
    }

    void OnTriggerEnter2D(Collider2D other) // Détection des collisions avec le joueur et la spatule 
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerProp>().HP -= AttackDmg;
            Destroy(gameObject);
        }
        if (other.CompareTag("Spatule"))
        {
            Destroy(gameObject);
        }
    }
}
