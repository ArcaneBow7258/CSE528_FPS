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
    public float damage;
    public float fireRate;
    public float lastShootTime;
    public GameObject shootpoint;
    private UnityEngine.AI.NavMeshAgent agent;


    private Animator animator;
    private void Awake(){
        agent = GetComponentInParent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    void Update(){
        if (currentState == EnemyState.Sit){
            animator.SetBool("Moving", false);
            animator.SetBool("Attacking", false);
            DoNothing();}
        else if (currentState == EnemyState.ChasePlayer){
            animator.SetBool("Moving", true);
            animator.SetBool("Attacking", false);
            ChasePlayer();
            }
        else {
            
             animator.SetBool("Attacking", true);
             animator.SetBool("Moving", false);
             AttackPlayer();
        }
    }
    void DoNothing(){
        
        if(sightSensor.detectedObject != null){
            currentState = EnemyState.ChasePlayer;
            return;
        }
    }
    void ChasePlayer(){
        agent.isStopped = false;
        
        if(sightSensor.detectedObject == null){
            currentState = EnemyState.Sit;
            return;
        }
        agent.SetDestination(sightSensor.detectedObject.transform.position);
        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
        if (distanceToPlayer < playerAttackDistance){
            currentState = EnemyState.AttackPlayer;
            lastShootTime = Time.time; //finish animation before attacking, not on contact.
        }
        
    }
    void AttackPlayer(){
        agent.isStopped = true;
        if(sightSensor.detectedObject == null){
            currentState = EnemyState.Sit;
            return;
        }
        //Debug.Log(sightSensor.detectedObject.transform.position);
        LookTo(sightSensor.detectedObject.transform.position);
        //Shoot();
        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
        if (distanceToPlayer > playerAttackDistance * 1.1f){
            currentState = EnemyState.ChasePlayer;
        }else{
            if(Time.time > lastShootTime + fireRate){
                lastShootTime = Time.time;
                if(bullet != null && shootpoint != null){
                    GameObject b = Instantiate(bullet, shootpoint.transform);
                    b.transform.LookAt(sightSensor.detectedObject.transform.position + Vector3.up*1);
                    b.GetComponent<ContactDamage>().damage = damage;
                }else{
                    sightSensor.detectedObject.GetComponent<Life>().amount -= damage;
                }
            }
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
        //Debug.Log(transform.position);
        Vector3 directionToPosition = Vector3.Normalize(targetPosition - transform.position);
        directionToPosition.y = 0;
        transform.forward = directionToPosition;

    }


    private void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);
   
    }
}
