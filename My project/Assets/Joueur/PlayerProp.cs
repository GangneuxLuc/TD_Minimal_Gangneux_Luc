using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerProp : MonoBehaviour
{
    [Header("Propriétés du joueur")]
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private bool CanMove = true;
    [SerializeField] public float attackSpeed = 0.075f;
    [SerializeField] public int HP = 20;
    [SerializeField] public int AttackDmg = 1;
    [SerializeField] public bool isAttacking = false;
    [SerializeField] public bool isSpecialAttacking = false;

    private Vector2 movement;
    GameObject ennemy;
    public SpriteRenderer spriteRenderer;
    Color originalColor;

    // suivi des PV pour détecter une diminution
    private int lastHP;
    private Coroutine flashCoroutine;

    void Start()
    {
        ennemy = GameObject.FindWithTag("Ennemy");
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer != null ? spriteRenderer.color : Color.white;
        lastHP = HP;
    }
    void Update() // Logique principale du joueur
    {
        Movement();
        Attack();
        SpecialAttack();
        Hit();
        if (HP <= 0)
        {
            Debug.Log("Le joueur est mort !");
            Destroy(gameObject);
        }

    } 

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isAttacking == false)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackSpeed);
        isAttacking = false;
        yield return new WaitForSeconds(attackSpeed * 1.5f);
    }
    public virtual void Movement() // Logique de déplacement du joueur
    {
        if (CanMove)
        {
            movement.x = Input.GetAxis("Horizontal");
            transform.Translate(movement * speed * Time.deltaTime);
        }
    }

    void SpecialAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSpecialAttacking = true;

            Debug.Log("Attaque spéciale du joueur !");
        }
    }
    void Hit()
    {
        // si les PV ont diminué, lancer le feedback visuel
        if (HP < lastHP)
        {
            if (flashCoroutine != null) StopCoroutine(flashCoroutine);
            flashCoroutine = StartCoroutine(Feedback());
        }
        // mettre à jour lastHP (aussi si PV augmentent ou restent identiques)
        lastHP = HP;
    }
    IEnumerator Feedback() // Feedback visuel lorsque le joueur est touché
    {
        Debug.Log("Feedback visuel du joueur touché");
        if (spriteRenderer == null) yield break;

        spriteRenderer.color = Color.white;
        yield return new WaitForSecondsRealtime(0.05f);
        spriteRenderer.color = originalColor;
        yield return new WaitForSecondsRealtime(0.05f);
        flashCoroutine = null;
    }
}


