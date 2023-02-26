using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float hpUp;
    public float manaUp;
    void OnTriggerEnter(Collider other){
        Destroy(gameObject);
        Life life = other.GetComponent<Life>();

        if(life != null){
            life.max += hpUp;
            life.amount += hpUp;
        }
        Mana mana = other.GetComponent<Mana>();

        if(mana != null){
            mana.max += manaUp;
            mana.amount += manaUp;
        }

        


    }
}
