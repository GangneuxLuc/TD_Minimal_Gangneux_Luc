using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class EnnemyClass : MonoBehaviour //Classe de base pour les ennemis
{
    [Header("Statistiques de l'ennemi")]
    [SerializeField] protected int HP = 3;
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
       GetComponent<PlayerProp>();
        

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
       
        PlayerProp playerProp = collision.gameObject.GetComponent<PlayerProp>();
        if (playerProp != null)
        {
            HP -= playerProp.AttackDmg;
        }
        if (HP < 1)
        {
            Destroy(gameObject);
        }
    }

    protected float CalculateDistanceXYPlane()
    {
        Vector2 V1 = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 V2 = new Vector2(transform.position.x, transform.position.y);
        return Vector2.Distance(V1, V2);
    }
}