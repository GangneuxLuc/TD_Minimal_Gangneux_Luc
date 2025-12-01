using UnityEngine;
using System.Collections;
public class Spatule : MonoBehaviour
{
    GameObject player;
    PlayerProp playerProp;
    [SerializeField] Transform pivotPoint;
    private float AttackSpeed;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerProp = player.GetComponent<PlayerProp>();
        AttackSpeed = playerProp.AttackSpeed;

    }

    void Update()
    {
        if (playerProp.isAttacking)
        {
            Debug.Log("La spatule attaque !");
            transform.RotateAround(pivotPoint.position, Vector3.up, AttackSpeed * Time.deltaTime);
        }
    }
}