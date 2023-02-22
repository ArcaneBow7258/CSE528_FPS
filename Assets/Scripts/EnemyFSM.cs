using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState{GoToBase, AttackBase, ChasePlayer, AttackPlayer};
    public EnemyState currentState;
    public Sight sightSensor;
    
    public Transform baseTransform;
    public float baseAttackDistance;

    public float playerAttackDistance; 

    public GameObject bullet;
    public int fireRate;
    public float lastShootTime;

    private UnityEngine.AI.NavMeshAgent agent;
    private void Awake(){
        baseTransform = GameObject.Find("BaseDamagePoint").transform;
        agent = GetComponentInParent<UnityEngine.AI.NavMeshAgent>();
    }
    void Update(){
        if(currentState == EnemyState.GoToBase){GoToBase();}
        else if (currentState == EnemyState.AttackBase){AttackBase();}
        else if (currentState == EnemyState.ChasePlayer){ChasePlayer();}
        else {AttackPlayer();}
    }
    void GoToBase(){
        agent.isStopped = false;
        agent.SetDestination(baseTransform.position);
        if(sightSensor.detectedObject != null){
            currentState = EnemyState.ChasePlayer;
        }
        float distanceToBase = Vector3.Distance(transform.position, baseTransform.position);
        if (distanceToBase < baseAttackDistance){
            currentState = EnemyState.AttackBase;
        }
        
        
    }
    void AttackBase(){
        agent.isStopped = true;
        LookTo(baseTransform.position);
        Shoot();
    }
    void ChasePlayer(){
        agent.isStopped = false;
        agent.SetDestination(sightSensor.detectedObject.transform.position);
        if(sightSensor.detectedObject == null){
            currentState = EnemyState.ChasePlayer;
            return;
        }
        
        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
        if (distanceToPlayer < playerAttackDistance){
            currentState = EnemyState.AttackPlayer;
        }
        
    }
    void AttackPlayer(){
        agent.isStopped = true;
        if(sightSensor.detectedObject == null){
            currentState = EnemyState.GoToBase;
            return;
        }
        LookTo(sightSensor.transform.position);
        Shoot();
        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
        if (distanceToPlayer > playerAttackDistance * 1.1f){
            currentState = EnemyState.ChasePlayer;
        }


    }
    void Shoot(){
        var timeSinceLastShoot  = Time.time - lastShootTime;
        if (timeSinceLastShoot > fireRate){
            lastShootTime = Time.time;
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }
    void LookTo(Vector3 targetPosition){
        Vector3 directionToPosition = Vector3.Normalize(targetPosition - transform.parent.position);
        directionToPosition.y = 0;
        transform.parent.forward = directionToPosition;

    }
}
