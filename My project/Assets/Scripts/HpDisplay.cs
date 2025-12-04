using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpDisplay : MonoBehaviour
{
    
 public TMP_Text textHp;
 float PlayerHp;

    private void Start()
    {
       
    }

    void Update()
    {
        PlayerHp = GameObject.FindWithTag("Player").GetComponent<PlayerProp>().HP;
        textHp.text = "HP : " + PlayerHp.ToString();
    }

}

