using UnityEngine;

public class Bee : EnnemyClass //Je crée une classe Bee qui hérite de la classe EnnemyClass
{
    public override void Attack() //Je redéfinis la méthode Attaquer pour le Squelette
    {
        //s'arrete et tire de loin
    }

    
    private void Update()
    {
        Movement();
    }
}
