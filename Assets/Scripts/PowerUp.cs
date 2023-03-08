using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public float[] hpRange = {0,0}; //0 being lwoer and 1 being upper bound
    public float[] manaRange = {0,0};
    public float[] damageRange = {0,0};
    public float[] speedRange = {0,0};
    public float[] healRange = {0,0};
    void OnTriggerEnter(Collider other){
        Destroy(gameObject);
        Life life = other.GetComponent<Life>();

        if(life != null && (hpRange[1] != 0 || healRange[1] != 0)){
            var increase = Random.Range(hpRange[0],hpRange[1]);
            life.max += increase;
            life.amount += increase;
            increase = Random.Range(hpRange[0],hpRange[1]);
        }
        Mana mana = other.GetComponent<Mana>();

        if(mana != null && manaRange[1] != 0){
            var increase = Random.Range(manaRange[0],manaRange[1]);
            mana.max += increase;
            mana.amount += increase;
        }
        PlayerShoot ps = other.GetComponent<PlayerShoot>();
        if(ps != null && manaRange[1] != 0){
            var increase = Random.Range(damageRange[0], damageRange[1]);
            ps.basicMulti += increase;
        }
       
        playerMovementNew m = other.GetComponent<playerMovementNew>();
        if(m != null && speedRange[1] != 0){
            var increase = Random.Range(speedRange[0], speedRange[1]);
            m.speed += increase;
        }

        


    }
}
