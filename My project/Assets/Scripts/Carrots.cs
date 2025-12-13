using UnityEngine;

public class Carrots : EnnemyClass //Je crée une classe Bee qui hérite de la classe EnnemyClass
{
   
    private void Start()
    {

        originalColor = Color.orange;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = originalColor;
        HP = 10;
        range = 1.25f;
        AttackDmg = 3;
        

    }
   

    void Update()
    {
       Movement();
       Attack();

    }   
}