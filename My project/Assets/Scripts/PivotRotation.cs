using UnityEngine;
using System.Collections;

public class PivotRotation : MonoBehaviour
{
    GameObject player;
    PlayerProp playerProp;
    Quaternion InitialRotation;



    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerProp = player.GetComponent<PlayerProp>();
        InitialRotation = gameObject.transform.rotation;

    }

    void Update()
    {
       
       StartCoroutine(Rotation());

    }
    public IEnumerator Rotation()
    {
        if (playerProp.isAttacking)
        {
            transform.Rotate(0f, 0f, -1500 * Time.deltaTime);
            yield return new WaitForSeconds(player.GetComponent<PlayerProp>().attackSpeed );
            gameObject.transform.rotation = InitialRotation;
        }
    }
}
