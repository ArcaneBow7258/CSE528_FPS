using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManaBar : MonoBehaviour
{

    public Mana targetMana;
    Image image;
    public bool dynamic;
    private Transform backT;
    private float baseMana;
    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
        if(dynamic){
            backT = transform.parent.transform.GetChild(0).transform;
            baseMana = targetMana.max;

        }
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = targetMana.amount / targetMana.max;
        if(dynamic){
            image.transform.localScale = new Vector3(targetMana.max / baseMana,1,1);
            backT.localScale = new Vector3(targetMana.max / baseMana,1,1);
        }
    }
}
