using System.Collections;
using System.Linq;
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
    public float fireRate = 1f;
    public float lastFire;
    public bool spread = true;
    public float basicMulti = 1;
    public LayerMask mask;

    internal Dictionary<Status, float> statuses = new Dictionary<Status, float>();
    private float damageBase= 1;
    public Ability[] abilities =  new Ability[4];//{new Explosion(), new Blank(), new Blank(), new Dash()};
    private float sharedCooldown = 0.5f;
    internal Vector2 movementValue;

    private Vector3 spreadVector = new Vector3(0.1f, 0.1f, 0.1f);
    
    internal LineRenderer laserLine;
    internal Animator animator;
    internal Rigidbody rb;
    private Color hitColor = new Color(1,0,0,0.5f);
    private Color missColor = new Color(1,1,1,0.5f);
    void Awake(){
        laserLine = GetComponent<LineRenderer>();
        laserLine.SetPosition(0,shootPoint.transform.position);
        laserLine.SetPosition(1,shootPoint.transform.position);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        abilities[0] = new Explosion();
        abilities[1] = new PoisonCloud();
        abilities[2] = new Accelerate();
        abilities[3] = new Dash();


        
    }
    public void Update(){
        //Tick down cooldown
        for(int i = 0; i < abilities.Length; i++){
           abilities[i].coolCount = Mathf.Clamp(abilities[i].coolCount - Time.deltaTime,0,abilities[i].cooldown);
        }
       
        foreach(var s in  statuses.Keys.ToList()){
            if(s.trigger(Time.deltaTime)){
                //Debug.Log("remove");
                s.end();
                statuses.Remove(s);
            }

        }
        
    }
    public void OnA1(){
        if(abilities[0].CanCast(ref mana.amount)){
            abilities[0].Activate(this);
        }
        else return;
    }
    public void OnA2(){
        if(abilities[1].CanCast(ref mana.amount)){
            abilities[1].Activate(this);
        }
        else return;
    }
    public void OnA3(){
        if(abilities[2].CanCast(ref mana.amount)){
            abilities[2].Activate(this);
        }
        else return;
    }
    public void OnA4(){
        if(abilities[3].CanCast(ref mana.amount)){
            abilities[3].Activate(this);
        }
        else return;
    }

    public void OnMove(InputValue value){
        movementValue = value.Get<Vector2>();
    }
 
    public void OnFire(InputValue value){  
        //animator.SetBool("Shooting", true);
        //animator.SetFloat("FireRate",1/fireRate);
        if(value.isPressed){
            Shoot();
        }
        
    }
    public void Shoot(){
        if(Time.timeScale >0 && Time.time >= lastFire + 1f /fireRate){
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
        yield return new WaitForSeconds(1f/fireRate);
        laserLine.enabled = false;
        //animator.SetBool("Shooting", false);
    }
}


