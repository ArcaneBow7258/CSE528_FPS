using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability {
    public float cooldown;
    public float manaCost;
    public float coolCount = 0;
    public float damageBase;
    public float aoe;
    public float range;
    public float duration;
    public GameObject effect;
    public abstract void Activate(PlayerShoot original);
    public bool CanCast(ref float amount){
        if(amount >= manaCost && coolCount == 0){
            coolCount = cooldown;
            amount -= manaCost;
            return true;
        }else{
            return false;
        }
    }

}
public class Blank : Ability{
    public Blank(){
        manaCost = 0f;
        cooldown = 0f;
    }
    public override void Activate(PlayerShoot original)
    {
        return;
    }
}
public class Explosion : Ability {
    public Explosion(){
        cooldown = 5f;
        manaCost = 2f;
        damageBase = 3f;
        range = 2f;
        aoe = 1f;
        
        effect = Resources.Load<GameObject>("Explosion");
    }
    public override void Activate(PlayerShoot original){
        Ray cameraRay = original.cam.ScreenPointToRay(Input.mousePosition);
        Vector3 point = cameraRay.GetPoint(range);
        if(Physics.Linecast(original.shootPoint.transform.position, point, out RaycastHit hit, original.mask)){
            point=hit.point; 
        }
            PlayerShoot.Instantiate(effect, point, new Quaternion(0,0,0,0));
            Collider[] hits = Physics.OverlapSphere(point,2,original.mask);
            for(int i = 0; i<hits.Length; i++){
                Life hitLife = hits[i].GetComponent<Life>();
                if(hitLife != null){
                            hitLife.amount -= damageBase * original.basicMulti;
                }
            }
    }

}
public class PoisonCloud : Ability{
    DOT dot;
    public PoisonCloud(){
        cooldown = 10f;
        manaCost = 5f;
        damageBase = 1f;
        range = 3f;
        aoe = 2f;
        duration = 5f;
        effect = Resources.Load<GameObject>("Poison");
        dot = effect.GetComponent<DOT>();
        dot.radius = aoe;
        dot.dps = damageBase;
        dot.duration = duration;
        
    }
    public override void Activate(PlayerShoot original)
    {
        Ray cameraRay = original.cam.ScreenPointToRay(Input.mousePosition);
        Vector3 point = cameraRay.GetPoint(range);
        if(Physics.Linecast(original.shootPoint.transform.position, point, out RaycastHit hit, original.mask)){
            point=hit.point; 
        }  
        dot.mask = original.mask;
        PlayerShoot.Instantiate(effect, point, new Quaternion(0,0,0,0)).GetComponent<DOT>().dps *= original.basicMulti;                                    
    }
}

public class Accelerate : Ability{
    public Accelerate(){
        cooldown = 6f;
        manaCost = 1f;
        duration = 2f;
    }
    public override void Activate(PlayerShoot original)
    {
        if(!original.statuses.TryAdd(new Stim(original.gameObject,  duration,2f), duration)){
        }
    }
}

public class Dash : Ability {
    public Dash(){
        cooldown = 1f;
        manaCost = 3f;
    }
    public override void Activate(PlayerShoot original)
    {
        original.rb.AddRelativeForce(original.movementValue.x*200,0,original.movementValue.y*200 );
    }
}
