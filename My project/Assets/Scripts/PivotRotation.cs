using UnityEngine;
using System.Collections;

public class PivotRotation : MonoBehaviour
{
    GameObject player;
    PlayerProp playerProp;



    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerProp = player.GetComponent<PlayerProp>();

    }

    void Update()
    {
       
       StartCoroutine(Rotation());

    }
    public IEnumerator Rotation()
    {

       
        
        if (playerProp.isAttacking)
        {
            transform.Rotate(0f, 0f, -10f);
            yield return new WaitForSeconds(player.GetComponent<PlayerProp>().attackSpeed /2);
            transform.Rotate(0f, 0f, 10f);


        }
    }
}
