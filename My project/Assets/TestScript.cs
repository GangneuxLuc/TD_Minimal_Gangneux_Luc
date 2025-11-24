using Unity.VisualScripting;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public bool Istrigger = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Istrigger)
        {
            Debug.Log("Trigger activated");
            transform.position = new Vector2(0f, 0f);
        }
        else 
        {
           transform.Translate(new Vector2(-1f * Time.deltaTime , 0f));
        }

    }

    private void OnTriggerEnter(Collider other)
    {
      
        Istrigger = true;
    }
}
