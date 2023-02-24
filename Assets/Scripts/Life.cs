using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Life : MonoBehaviour
{
    public float amount;
    public float max;
    
    public UnityEvent onDeath;
    void Awake(){
        amount = max;
    }
    void Update(){
        if(amount <= 0){
            
            onDeath.Invoke();
            Destroy(gameObject);
        }
        if(amount > max){
            amount = max;
        }


    }
}
