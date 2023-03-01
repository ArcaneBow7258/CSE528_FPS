using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Status{
    public float duration;
    public float rate;
    public float counter;
    public abstract bool trigger(float dec);
    
    public abstract void end();


}

public class Stim : Status {
    playerMovementNew move;
    PlayerShoot shoot;
    float increase;
    float baseS;
    float baseM;
    public Stim(GameObject o ,float d, float inc){
        duration = d;
        increase = inc;
        counter = d;
        move = o.GetComponent<playerMovementNew>();
        baseM= move.speed ;
        shoot = o.GetComponent<PlayerShoot>();
        baseS = shoot.fireRate;
    }
    public override bool trigger(float dec){
        counter -= dec;
        if(counter <= 0){
            return true;
        }else{
            move.speed =  baseM*increase;
            shoot.fireRate = baseS*increase;
            return false;
        }
    }
    public override void end(){
        move.speed = baseM;
        shoot.fireRate = baseS;
    }
}
