using System.Collections;
using UnityEditor;
using UnityEngine;

public class Chicken : EnnemyClass //Je crée une classe Bee qui hérite de la classe EnnemyClass
{
    [SerializeField] GameObject prefabOeuf;
    private void Start()
    {
        range = 4f;
        HP = 3;
        AttackSpeed = 3f;
    }

    
    private void Update()
    {
        Movement();
        Attack();
    }
    public override void Attack()
    {
        if (dst < range && isAttacking == false)
        {
            Debug.Log("Le poulet attaque !");
            StartCoroutine(AttackOeufCoroutine());
           
        }

    }
    
    IEnumerator AttackOeufCoroutine()
    {
        if (isAttacking) yield break; // Empêche d'attaquer si déjà en train d'attaquer
        isAttacking = true;
        Instantiate(prefabOeuf, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(AttackSpeed);
        isAttacking = false;
    }


}
