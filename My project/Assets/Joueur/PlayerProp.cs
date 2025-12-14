using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

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

    // Référence à la spatule (à assigner dans l'inspecteur si nécessaire)
    [SerializeField] private Transform spatulaTransform;
    private Vector3 originalSpatulaScale;

    private Vector2 movement;
    GameObject ennemy;
    public SpriteRenderer spriteRenderer;
    Color originalColor;

    // durée du spécial 
    [SerializeField] private float specialDuration = 1f;
    // cooldown du spécial 
    [SerializeField] private float specialCooldown = 10f;
    private float lastSpecialTime = -Mathf.Infinity;

    // suivi des PV pour détecter une diminution
    private int lastHP;
    private Coroutine flashCoroutine;

    // sauvegarde du sprite original pour restauration après spécial
    private Sprite originalSprite;

    // sauvegardes de stats pour restauration
    private float originalSpeed;
    private float originalAttackSpeed;
    private int originalAttackDmg;

    void Start()
    {
        ennemy = GameObject.FindWithTag("Ennemy");
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        originalSprite = spriteRenderer.sprite;
        lastHP = HP;

        // sauvegarder stats initiales
        originalAttackSpeed = attackSpeed;
        originalAttackDmg = AttackDmg;

        if (spatulaTransform != null)
            originalSpatulaScale = spatulaTransform.localScale;
        else
            originalSpatulaScale = Vector3.one;
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
        if (Input.GetMouseButton(0) && isAttacking == false)
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
            transform.Translate(movement * speed* Time.deltaTime);
        }
    }

    void SpecialAttack()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isSpecialAttacking)
            {
                Debug.Log("Spécial déjà actif.");
                return;
            }

            // vérifie le cooldown avant de lancer le spécial
            if (Time.time < lastSpecialTime + specialCooldown)
            {
                float remaining = (lastSpecialTime + specialCooldown) - Time.time;
                Debug.Log($"Spécial en cooldown: {remaining:F1}s restants");
                return;
            }

            lastSpecialTime = Time.time; // démarre le cooldown
            StartCoroutine(SpecialAttackCoroutine());
        }
    }

    IEnumerator SpecialAttackCoroutine()
    {
        if (isSpecialAttacking) yield break; // Empêche d'activer le spécial si déjà en train de l'utiliser
        isSpecialAttacking = true;

        // sauvegarde déjà faite dans Start, mais on s'assure ici également
        if (spatulaTransform != null)
        {
            if (originalSpatulaScale == Vector3.zero) originalSpatulaScale = spatulaTransform.localScale;
            spatulaTransform.localScale = originalSpatulaScale * 3f;
        }
        
        // Augmenter les stats du joueur pendant la durée du spécial
        attackSpeed *= 0.5f;
        AttackDmg *= 2;
        yield return new WaitForSeconds(specialDuration);
        Debug.Log("Fin du spécial du joueur");

        // Restaurer les stats originales
        attackSpeed = originalAttackSpeed;
        AttackDmg = originalAttackDmg;
        if (spatulaTransform != null) spatulaTransform.localScale = originalSpatulaScale;
        Debug.Log("Stats du joueur restaurées");

        isSpecialAttacking = false;

        // NOTE: le cooldown est géré par lastSpecialTime + specialCooldown.
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


