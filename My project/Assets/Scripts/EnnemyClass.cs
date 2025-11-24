using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnnemyClass : MonoBehaviour //Je crée une classe de base pour les ennemis
{
    [Header("Statistiques de l'ennemi")]
    [SerializeField] protected int HP;
    [SerializeField] protected string Name;
    [SerializeField] protected int damage;
    [SerializeField] protected float speed = 1f;
    public GameObject player;
    [Header("Références distance joueur-ennemi")]
    [SerializeField] protected float range = 2f;
    float dst;

    public bool bDebugCanMove = true;
    public virtual void Attack() //Je crée une méthode virtuelle pour que les classes enfants puissent la redéfinir
    {
        Debug.Log("L'ennemi attaque !");
    }
    public virtual void Movement() //Je crée une méthode virtuelle pour que les classes enfants puissent la redéfinir
    {

        dst = CalculateDistanceXYPlane();
        if (dst < range)
        {
            Debug.Log("Enemy stopped");
            bDebugCanMove = false;
            transform.Translate(new Vector2(0f, 0f));
        }
        else 
        {
            bDebugCanMove = true;
            transform.Translate(new Vector2(-1f * speed * Time.deltaTime, 0f));
        }

    }

    private void Update()
    {
        Movement();
    }


    protected float CalculateDistanceXYPlane()
    {

        Vector2 V1 = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 V2 = new Vector2(transform.position.x,transform.position.y);
        //Debug.Log(player.transform.position);
        return Vector2.Distance(V1, V2);
    }

    //ça marche pas car il prend l'instance du clone et non le prefab
}