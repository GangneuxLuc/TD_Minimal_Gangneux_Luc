using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs des ennemis")]
    [SerializeField] GameObject beePrefab;
    [SerializeField] GameObject carrotPrefab;

    [Header("Liste des ennemis")]
    List<GameObject> ennemi = new List<GameObject>();

    // Référence à l'instance du joueur en scène (recherchée au démarrage)
    GameObject playerInstance;

    void Start()
    {
        playerInstance = GameObject.FindGameObjectWithTag("Player");
        if (playerInstance == null) Debug.LogWarning("Spawner: aucun GameObject avec le tag 'Player' trouvé.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) ListerEnnemis();
        if (Input.GetKeyDown(KeyCode.V)) StartCoroutine(Vagues());
    }

    void ListerEnnemis()
    {
        foreach (GameObject Ennemi in ennemi)
        {
            Debug.Log("Type d'ennemi dans la liste : " + Ennemi.GetType());
        }
    }

    IEnumerator Vagues()
    {
        Debug.Log("Attention la première vague commence dans 5s");
        yield return new WaitForSeconds(5f);
        for (int vague = 1; vague <= 3; vague++)
        {
            Debug.Log("Vague " + vague + " commencée !");
            for (int i = 0; i < 3; i++)
            {
                GameObject prefab = (Random.value > 0.5f) ? carrotPrefab : beePrefab;
                GameObject instance = Instantiate(prefab, transform.position, Quaternion.identity);

                // Ajouter l'instance à la liste
                ennemi.Add(instance);

                // IMPORTANT : assigner l'instance du player (pas le prefab)
                var ec = instance.GetComponent<EnnemyClass>();
                if (ec != null && playerInstance != null)
                {
                    ec.player = playerInstance;
                }

                Debug.Log("Un nouvel ennemi de type " + instance.name + " a été créé !");
                yield return new WaitForSeconds(3f);
            }
            Debug.Log("Vague " + vague + " déployée");
            yield return new WaitForSeconds(10f);
        }
    }
}