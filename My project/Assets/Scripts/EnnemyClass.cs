using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Unity.VisualScripting;

public class EnnemyClass : MonoBehaviour //Classe de base pour les ennemis
{
    [Header("Statistiques de l'ennemi")]
    [SerializeField] public int HP;
    [SerializeField] protected string Name;
    [SerializeField] protected float speed;
    [SerializeField] protected int AttackDmg;
    [SerializeField] protected float AttackSpeed = 2f;
    [SerializeField] protected bool isAttacking = false;

    [Header("Références")]
    public GameObject player;

    SpriteRenderer spriteRenderer;
    [SerializeField] protected float range = 0f;
    protected float dst;
    public bool bDebugCanMove = true;

   void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
    }

    private void Update()
    {
        Movement();
        Attack();

    }

    public virtual void Attack()
    {
        if (dst < range && isAttacking == false)
        {
            Debug.Log("L'ennemi attaque !");
            StartCoroutine(AttackCoroutine());
        }
        
    }
   
    protected IEnumerator AttackCoroutine()
    {
        
       if (isAttacking) yield break; // Empêche d'attaquer si déjà en train d'attaquer
        player.GetComponent<PlayerProp>().HP -= AttackDmg;
        isAttacking = true;
        yield return new WaitForSeconds(AttackSpeed);
        isAttacking = false;

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
        if (collision.gameObject.CompareTag("Player") && player.GetComponent<PlayerProp>().isAttacking)
        {
            HP -= player.GetComponent<PlayerProp>().AttackDmg;
            ResetColor();

            Debug.Log("L'ennemi a été touché ! HP restant : " + HP);
            if (HP <= 0)
            {
                Destroy(gameObject);
                Debug.Log("L'ennemi est mort !");
            }
        }
    }

    IEnumerator ResetColor()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }


    protected float CalculateDistanceXYPlane()
    {
        Vector2 V1 = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 V2 = new Vector2(transform.position.x, transform.position.y);
        return Vector2.Distance(V1, V2);
    }
}