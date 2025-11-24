using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Spawner : MonoBehaviour //La classe Spawner permet de créer des ennemis et de les stocker dans une liste
{
    [Header("Prefabs des ennemis")]
    public GameObject beePrefab; //Je crée une variable pour stocker le prefab de l'ennemi
    public GameObject carrotPrefab; //Je crée une variable pour stocker le prefab de l'ennemi

    [Header("Liste des ennemis")]
    List<GameObject> ennemi = new List<GameObject>(); //Je crée une liste pour stocker les ennemis

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) //Si j'appuie sur la touche L, j'appelle la méthode ListerEnnemis
        {
            ListerEnnemis();
        }
       
        if (Input.GetKeyDown(KeyCode.V)) //Si j'appuie sur la touche V, je lance la coroutine Vagues
        {
            StartCoroutine(Vagues());
        }

        void ListerEnnemis() //Je crée une méthode pour lister les types d'ennemis dans la liste
        {
            foreach (GameObject Ennemi in ennemi)
            {
                Debug.Log("Type d'ennemi dans la liste : " + Ennemi.name);
            }
            
        }
    }

    IEnumerator Vagues() //Je crée une coroutine pour gérer les vagues d'ennemis
    {
        Debug.Log("Attention la première vague commence dans 2.5s");
        yield return new WaitForSeconds(2.5f); //J'attends 2.5 secondes avant de commencer la première vague
       for (int vague = 1; vague <= 3; vague++) //Je crée une boucle pour gérer les vagues
        {
            Debug.Log("Vague " + vague + " commencée !");
            for (int i = 0; i < 3; i++) //Je crée une boucle pour gérer le nombre d'ennemis par vague
            {
                GameObject newEnnemy;
                if (Random.value > 0.5f) //Je choisis aléatoirement le type d'ennemi à créer
                {
                    newEnnemy = carrotPrefab;
                }
                else
                {
                    newEnnemy = beePrefab;

                }

                GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(newEnnemy);
                instance.transform.position = transform.position;
                instance.transform.rotation = Quaternion.identity;
                ennemi.Add(instance);
                Debug.Log("Un nouvel ennemi " + instance.name + " a été créé !");
                yield return new WaitForSeconds(3f); //J'attends 1 seconde avant de créer le prochain ennemi
            }
            Debug.Log("Vague " + vague + " déployée");
            yield return new WaitForSeconds(10f); //J'attends 10 secondes avant de commencer la prochaine vague 
       
        }
    }
}


