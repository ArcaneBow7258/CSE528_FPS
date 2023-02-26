using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeBar : MonoBehaviour
{
    public Life targetLife;
    public float timeOut;
    Image image;
    Image back;
    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
        //back = transform.parent.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = targetLife.amount / targetLife.max;
        
    }
}
