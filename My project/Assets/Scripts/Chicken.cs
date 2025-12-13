using System.Collections;
using System.Net.NetworkInformation;
using UnityEditor;
using UnityEngine;

public class Chicken : EnnemyClass //Je crée une classe Chicken qui hérite de la classe EnnemyClass
{
    [SerializeField] GameObject prefabOeuf;
    
    private void Start() // Initialisation des statistiques spécifiques au poulet
    {
        originalColor = Color.white;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = originalColor;
 
        range = 4f;
        HP = 3;
        AttackSpeed = 3f;
    }
    private void Update()
    {
        Movement();
        Attack();
    }
    public override void Attack() //Override de la méthode Attack de la classe EnnemyClass pour la modifier
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
