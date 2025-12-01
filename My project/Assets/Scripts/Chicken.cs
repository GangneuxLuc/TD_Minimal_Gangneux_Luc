using UnityEngine;

public class Chicken : EnnemyClass //Je crée une classe Bee qui hérite de la classe EnnemyClass
{
    private void Start()
    {
        range = 4f;
        HP = 2;
    }

    
    private void Update()
    {
        Movement();
    }
}
