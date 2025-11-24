
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnnemyClass : MonoBehaviour //Classe de base pour les ennemis
{
    [Header("Statistiques de l'ennemi")]
    [SerializeField] protected int HP;
    [SerializeField] protected string Name;
    [SerializeField] protected int damage;
    [SerializeField] protected float speed = 1f;

    [Header("Références")]
    [Tooltip("Assignez l'instance du joueur dans l'inspecteur, ou laissez vide pour recherche par tag")]
    public GameObject player;

    [SerializeField] protected float range = 2f;
    float dst;
    public bool bDebugCanMove = true;
    [SerializeField] string playerTag = "Player";

    void Start()
    {
        // Ne pas écraser si assigné dans l'inspecteur — seulement fallback
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag(playerTag);
            if (p != null) player = p;
            else Debug.LogWarning($"EnnemyClass: aucun GameObject avec le tag '{playerTag}' trouvé. Assignez 'player' depuis le Spawner ou l'inspecteur.");
        }
    }

    public virtual void Attack()
    {
        Debug.Log("L'ennemi attaque !");
    }

    public virtual void Movement()
    {
        if (player == null)
        {
            // On ne peut pas calculer la distance sans référence au player
            return;
        }

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

    private void Update()
    {
        if (player != null)
            Debug.Log("Distance au joueur : " + CalculateDistanceXYPlane());
        Movement();
    }

    protected float CalculateDistanceXYPlane()
    {
        if (player == null) return float.PositiveInfinity;
        Vector2 V1 = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 V2 = new Vector2(transform.position.x, transform.position.y);
        return Vector2.Distance(V1, V2);
    }
}