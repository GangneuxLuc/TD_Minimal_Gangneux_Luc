using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerProp : MonoBehaviour
{
    [Header("Propriétés du joueur")]
    [SerializeField] private float speed = 1.0f;
   // [SerializeField] private GameObject joueur;
    [SerializeField] private bool CanMove = true;
    [SerializeField] private float range = 1f;
    [SerializeField] public float AttackSpeed = 1f;
    [SerializeField] private int HP = 10;
    [SerializeField] public int AttackDmg = 1;
    [SerializeField] public bool isAttacking = false;

    private Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        Movement();
        Attack();

    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isAttacking == false)
        {
            Debug.Log("Le joueur attaque !");
            StartCoroutine(AttackCoroutine());
        }
      
    }

    IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        yield return new WaitForSeconds(AttackSpeed);
        isAttacking = false;
        yield return new WaitForSeconds(AttackSpeed * 1.5f);
    }
    public virtual void Movement()
    {
        if (CanMove)
        {
            movement.x = Input.GetAxis("Horizontal");
            transform.Translate(movement * speed * Time.deltaTime);
        }
    }

}


