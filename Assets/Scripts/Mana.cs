using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public float amount;
    public float max;
    public float regen;
    // Start is called before the first frame update
    void Wake()
    {
        amount = max;
    }

    // Update is called once per frame
    void Update()
    {
        if(amount < max){
            float predictor = amount + (Time.deltaTime * regen);
            amount = Mathf.Clamp(predictor, 0, max);
        }
    }
}
