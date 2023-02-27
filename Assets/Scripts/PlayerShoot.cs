using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shootPoint;
    public Camera cam;
    public bool hitScan = true;
    public Mana mana;
    // Update is called once per frame
    public float range = 5;
    public float fireRate = 0.5f;
    public float lastFire;
    public bool spread = true;
    public float basicMulti = 1;



    private float damageBase= 1;
    public float[] cooldowns = {5f,10f,5f,1f};
    public float[] cost = {2f,1f,1f,3f};
    public float[] coolCount = {0f,0f,0f,0f};
    public GameObject[] aEffects;
    private float sharedCooldown = 0.5f;
    private Vector2 movementValue;

    private Vector3 spreadVector = new Vector3(0.1f, 0.1f, 0.1f);
    public LayerMask mask;
    private LineRenderer laserLine;
    private Animator animator;
    private Rigidbody rb;
    private Color hitColor = new Color(1,0,0,0.5f);
    private Color missColor = new Color(1,1,1,0.5f);
    void Awake(){
        laserLine = GetComponent<LineRenderer>();
        laserLine.SetPosition(0,shootPoint.transform.position);
        laserLine.SetPosition(1,shootPoint.transform.position);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    public void Update(){
        //Tick down cooldown
        for(int i = 0; i < 4; i++){
            coolCount[i] = Mathf.Clamp(coolCount[i] - Time.deltaTime,0,cooldowns[i]);
        }
    }
    public void OnA1(){
        if(coolCount[0] == 0 && mana.amount >= cost[0]){
            mana.amount -= cost[0];
            coolCount[0] = cooldowns[0];
            Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
            Physics.Linecast(shootPoint.transform.position, cameraRay.GetPoint(range*1.5f), out RaycastHit hit, mask);
            Instantiate(aEffects[0], hit.point, new Quaternion(0,0,0,0));
            Collider[] hits = Physics.OverlapSphere(hit.point,2,mask);
            for(int i = 0; i<hits.Length; i++){
                Life hitLife = hits[i].GetComponent<Life>();
                if(hitLife != null){
                            hitLife.amount -= damageBase * 2;
                }
            }
            
        }
    }
    public void OnA2(){
        if(coolCount[1] == 0 && mana.amount >= cost[1]){
            mana.amount -= cost[1];
            coolCount[1] = cooldowns[1];
            
        }
    }
    public void OnA3(){
        if(coolCount[2] == 0 && mana.amount >= cost[2]){
            mana.amount -= cost[2];
            coolCount[2] = cooldowns[2];
        }
    }
    public void OnMove(InputValue value){
        movementValue = value.Get<Vector2>();
    }
    public void OnA4(){
        if(coolCount[3] == 0 && mana.amount >= cost[3]){
            mana.amount -= cost[3];
            coolCount[3] = cooldowns[3];
            rb.AddRelativeForce(movementValue.x*200,0,movementValue.y*200 );
            /* 
            if(rb.SweepTest(vel, out RaycastHit hit, 2)){
                Debug.DrawLine(transform.position, hit.point, Color.green, 2);
                transform.position = hit.point;
            }else{
                transform.Translate(vel.normalized * 2, Space.Self);
            }*/
        }
    }

    public void OnFire(InputValue value){  
        //animator.SetBool("Shooting", true);
        //animator.SetFloat("FireRate",1/fireRate);
        if(value.isPressed){
            Shoot();
        }
        
    }
    public void Shoot(){
        if(Time.timeScale >0 && Time.time >= lastFire + fireRate){
            //print("pewpew"); 
            lastFire = Time.time;
            
            if(hitScan){
                laserLine.SetPosition(0,shootPoint.transform.position);
                Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
                if(Physics.Linecast(shootPoint.transform.position, cameraRay.GetPoint(range), out RaycastHit hit, mask)){
                        //Debug.DrawLine(shootPoint.transform.position,hit.point, Color.red);
                        
                        laserLine.startColor = hitColor;
                        laserLine.endColor = hitColor;
                        laserLine.SetPosition(1,hit.point);
                        
                        laserLine.startWidth = .2f;
                        Life hitLife = hit.collider.GetComponent<Life>();
                        if(hitLife != null){
                            hitLife.amount -= damageBase * basicMulti;
                            Debug.Log("hit");
                        }
                        

                }else{
                    //Debug.DrawLine(shootPoint.transform.position,cameraRay.GetPoint(range), Color.white);
                    laserLine.SetPosition(1, cameraRay.GetPoint(range));
                    laserLine.startColor = missColor;
                    laserLine.endColor = missColor;
                    Debug.Log("miss");

                }
                StartCoroutine(ShootLaser());



            }else{
                
                //if(value.isPressed){
                GameObject clone = Instantiate(bullet);
                clone.transform.position = shootPoint.transform.position;
                clone.transform.rotation = shootPoint.transform.rotation;
                //}
            }
            
        }
            
    }
    IEnumerator ShootLaser(){
        laserLine.enabled = true;
        yield return new WaitForSeconds(fireRate);
        laserLine.enabled = false;
        //animator.SetBool("Shooting", false);
    }
}
