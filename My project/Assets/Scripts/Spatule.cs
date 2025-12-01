using UnityEngine;
using System.Collections;

public class Spatule : MonoBehaviour
{
    [SerializeField] private float swaySpeed;
    [SerializeField] private float minSway = 0f;
    [SerializeField] private float maxSway = -0.1f;
    private float swayAmount;

    Quaternion originalRotation;
    GameObject player;
    PlayerProp playerProp;
    public Transform pivotPoint;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerProp = player.GetComponent<PlayerProp>();
        swaySpeed = playerProp.AttackSpeed;
        originalRotation = transform.rotation;
    }

    void Update()
    {
       StartCoroutine(Sway());

    }


    IEnumerator Sway()
    {
        if (playerProp.isAttacking)
        {
            yield return new WaitForSeconds(00.1f);
            transform.RotateAround(pivotPoint.position, Vector3.up, swayAmount);
            yield return new WaitForSeconds(swaySpeed / 2);
        }
    }
}
    


