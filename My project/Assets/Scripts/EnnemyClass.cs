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
    private Spawner spawner;
    public SpriteRenderer spriteRenderer;
    [SerializeField] protected float range = 0f;
    protected float dst;
    public bool bDebugCanMove = true;
    protected Color originalColor;



    private void Update() // Logique principale de l'ennemi
    {
        Movement();
        Attack();
    }

    public virtual void Attack() // Logique d'attaque de l'ennemi
    {
        if (dst < range && isAttacking == false)
        {
            Debug.Log("L'ennemi attaque !");
            StartCoroutine(AttackCoroutine());
        }
        
    }
   
    protected IEnumerator AttackCoroutine() // Logique d'attaque avec délai
    {
        
       if (isAttacking) yield break; // Empêche d'attaquer si déjà en train d'attaquer
        player.GetComponent<PlayerProp>().HP -= AttackDmg; 
        isAttacking = true;
        yield return new WaitForSeconds(AttackSpeed);
        isAttacking = false;

    }

    public virtual void Movement() // Logique de déplacement de l'ennemi vers le joueur
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

    private void OnCollisionEnter2D(Collision2D collision) // Détection des collisions avec le joueur
    {
        if (collision.gameObject.CompareTag("Player") && player.GetComponent<PlayerProp>().isAttacking)
        {
            HP -= player.GetComponent<PlayerProp>().AttackDmg; // Réduction des HP de l'ennemi
           
            StartCoroutine(Feedback());
            Debug.Log("L'ennemi a été touché ! HP restant : " + HP);
            if (HP <= 0) // Mort de l'ennemi
            {
                Die();
                Debug.Log("L'ennemi est mort !");
            }
        }
    }
    IEnumerator Feedback() // Feedback visuel lorsque l'ennemi est touché
    {
        Debug.Log("Feedback visuel de l'ennemi touché");
        spriteRenderer.color = Color.red;
        Debug.Log("Changement de couleur de l'ennemi en rouge");
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = originalColor;
        Debug.Log("Restauration de la couleur originale de l'ennemi");
     
  
    }
    protected float CalculateDistanceXYPlane() // Calcul de la distance entre l'ennemi et le joueur 
    {
        Vector2 V1 = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 V2 = new Vector2(transform.position.x, transform.position.y);
        return Vector2.Distance(V1, V2);
    }
    public void SetSpawner(Spawner s) => spawner = s; // Appeler depuis le Spawner après instantiation : ec.SetSpawner(this);
    void Die() // Logique de mort de l'ennemi
    {
        // ... logique de mort (sons, animations, désactivation)
        spawner?.UnregisterEnemy(gameObject);
        Destroy(gameObject);
    }

    // --- Accesseurs publics ajoutés pour que le Player puisse lire les stats de l'ennemi ---
    public int GetAttackDmg() => AttackDmg;
    public float GetAttackSpeed() => AttackSpeed;
    public float GetSpeed() => speed;
    public Sprite GetSprite() => spriteRenderer.sprite;
}