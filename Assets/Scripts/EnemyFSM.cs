using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState{Sit, Roam, ChasePlayer, AttackPlayer};
    public EnemyState currentState;
    public Sight sightSensor;
    

    public float playerAttackDistance; 

    public GameObject bullet;
    public int fireRate;
    public float lastShootTime;

    private UnityEngine.AI.NavMeshAgent agent;
    private void Awake(){
        agent = GetComponentInParent<UnityEngine.AI.NavMeshAgent>();
    }
    void Update(){
        if (currentState == EnemyState.Sit){DoNothing();}
        else if (currentState == EnemyState.ChasePlayer){ChasePlayer();}
        else {AttackPlayer();}
    }
    void DoNothing(){
        if(sightSensor.detectedObject != null){
            currentState = EnemyState.ChasePlayer;
            return;
        }
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
            currentState = EnemyState.ChasePlayer;
            return;
        }
        LookTo(sightSensor.detectedObject.transform.position);
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
