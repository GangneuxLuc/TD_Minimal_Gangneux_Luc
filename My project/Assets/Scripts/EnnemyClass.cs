using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Unity.VisualScripting;

public class EnnemyClass : MonoBehaviour //Classe de base pour les ennemis
{
    [Header("Statistiques de l'ennemi")]
    [SerializeField] public int HP = 3;
    [SerializeField] protected string Name;
    [SerializeField] protected int damage;
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected int AttackDmg;
    [SerializeField] protected float AttackSpeed;

    [Header("Références")]
    [Tooltip("Assignez l'instance du joueur dans l'inspecteur, ou laissez vide pour recherche par tag")]
    public GameObject player;

    [SerializeField] protected float range = 2f;
    float dst;
    public bool bDebugCanMove = true;

   

    void Start()
    {
  
    }

    private void Update()
    {
        Movement();
        Debug.Log("on rentre ici");
       
    }

    public virtual void Attack()
    {
        Debug.Log("L'ennemi attaque !");
    }

    public virtual void Movement()
    {
        dst = CalculateDistanceXYPlane();
        if (dst < range)
        {
            bDebugCanMove = false;
            // arrêt
        }
        else
        {
            bDebugCanMove = true;
            // déplacement vers la position du player (direction correcte)
            Vector2 posEnemy = transform.position;
            Vector2 posPlayer = player.transform.position;
            Vector2 next = Vector2.MoveTowards(posEnemy, posPlayer, speed * Time.deltaTime);
            transform.position = new Vector3(next.x, next.y, transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("Collision détectée avec : " + collision.gameObject.name);
        /* if (collision.gameObject.CompareTag("Player"))
         {
             Attack();
         }
         */

        if (collision.gameObject.CompareTag("Player") && player.GetComponent<PlayerProp>().isAttacking)
        {
            Debug.Log("L'ennemi a été touché par une spatule !");
            HP -= player.GetComponent<PlayerProp>().AttackDmg;
            Debug.Log("L'ennemi a été touché ! HP restant : " + HP);
            if (HP < 1)
            {
                Destroy(gameObject);
                Debug.Log("L'ennemi est mort !");
            }
        }
    }




    protected float CalculateDistanceXYPlane()
    {
        Vector2 V1 = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 V2 = new Vector2(transform.position.x, transform.position.y);
        return Vector2.Distance(V1, V2);
    }
}