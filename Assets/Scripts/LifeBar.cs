using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeBar : MonoBehaviour
{
    public Life targetLife;
    public bool enemy;
    public float timeOut;
    public bool dynamic;
    Image image;

    private Transform backT;
    private float baseLife;

    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
        if(dynamic){
            backT = transform.parent.transform.GetChild(0).transform;
            baseLife = targetLife.max;

        }
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = targetLife.amount / targetLife.max;
        if(dynamic){
            image.transform.localScale = new Vector3(targetLife.max / baseLife, 1,1);
            backT.localScale = new Vector3(targetLife.max / baseLife,1,1);
        }
    }
}
