using UnityEngine;

public class Carrots : EnnemyClass //Je crée une classe Bee qui hérite de la classe EnnemyClass
{
   
    private void Start()
    {
        HP = 7;
        range = 1.25f;
        AttackDmg = 1;
        

    }
   

    void Update()
    {
       Movement();
       Attack();

    }   
}