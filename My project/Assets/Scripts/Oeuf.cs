using UnityEngine;

public class Oeuf : MonoBehaviour
{
    float speed = 2f;
    int AttackDmg = 2;
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
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
