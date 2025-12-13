using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs des ennemis")]
    [SerializeField] GameObject chickenPrefab;
    [SerializeField] GameObject carrotPrefab;

    [Header("Liste des ennemis")]
    List<GameObject> ennemi = new List<GameObject>();

    // Référence à l'instance du joueur en scène (recherchée au démarrage)
    GameObject playerInstance;

    private bool VagueActive = false; // Indique si une vague est en cours

    void Start()
    {
        playerInstance = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) ListerEnnemis();
        
        if (Input.GetKeyDown(KeyCode.V) && !VagueActive) StartCoroutine(Vagues()); // Empêche de démarrer plusieurs fois la coroutine
    }

    void ListerEnnemis()
    {
        foreach (GameObject Ennemi in ennemi)
        {
            Debug.Log("Type d'ennemi dans la liste : " + Ennemi.GetType());
        }
    }

   
    public void UnregisterEnemy(GameObject enemy)  // Méthode publique pour que les ennemis se désinscrivent quand ils meurent
    {
        if (enemy == null) return;
        if (ennemi.Contains(enemy)) ennemi.Remove(enemy);
    }

    IEnumerator Vagues()
    {
        // Empêche de lancer plusieurs instances simultanées
        if (VagueActive) yield break;
        VagueActive = true;

        Debug.Log("Attention la première vague commence dans 2.5s");
        yield return new WaitForSeconds(2.5f);

        const int totalVagues = 5;
        const int ennemisParVague = 3;

        for (int vague = 1; vague <= totalVagues; vague++)
        {
            Debug.Log("Vague " + vague + " commencée !");
            for (int i = 0; i < ennemisParVague; i++)
            {
                GameObject prefab = (Random.value > 0.5f) ? carrotPrefab : chickenPrefab;
                GameObject instance = Instantiate(prefab, transform.position, Quaternion.identity);

                // Ajouter l'instance à la liste
                ennemi.Add(instance);

                // Assigne l'instance du player (pas le prefab)
                var ec = instance.GetComponent<EnnemyClass>();
                if (ec != null && playerInstance != null)
                {
                    ec.player = playerInstance;
                    // Optionnel : stocker la référence au spawner pour se désinscrire proprement
                    ec.SetSpawner(this);
                }
                yield return new WaitForSeconds(2f);
            }

            Debug.Log("Vague " + vague + " déployée");

            // Si ce n'est pas la dernière vague, attendre que tous les ennemis actuels soient éliminés
            if (vague < totalVagues)
            {
                yield return new WaitUntil(() => ennemi.Count == 0);
                // petite pause avant la vague suivante
                yield return new WaitForSeconds(3f);
            }
        }

        Debug.Log("Toutes les vagues terminées.");
        VagueActive = false;
    }
}