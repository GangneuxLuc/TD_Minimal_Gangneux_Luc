using UnityEngine;

public class Carrots : EnnemyClass //Je crée une classe Bee qui hérite de la classe EnnemyClass
{
   
    public override void Attack() //Je redéfinis la méthode Attaquer pour le Squelette
    {

    }
    private void Start()
    {
        HP = 7;


    }
   

    void Update()
    {
        Movement();
    }   
}