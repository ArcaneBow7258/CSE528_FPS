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
    // Update is called once per frame
    public float range = 5;
    public float fireRate = 0.5f;
    public float lastFire;
    public bool spread = true;
    private Vector3 spreadVector = new Vector3(0.1f, 0.1f, 0.1f);
    public LayerMask mask;
    private LineRenderer laserLine;

    private Color hitColor = new Color(1,0,0,0.5f);
    private Color missColor = new Color(1,1,1,0.5f);
    void Start(){
        laserLine = GetComponent<LineRenderer>();
        laserLine.SetPosition(0,shootPoint.transform.position);
        laserLine.SetPosition(1,shootPoint.transform.position);
    }

    public void OnFire(InputValue value){  
        if(Time.time > lastFire +fireRate){
            lastFire = Time.time;
            print("pewpew"); 
            if(hitScan){
                laserLine.SetPosition(0,shootPoint.transform.position);
                Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
                if(Physics.Linecast(shootPoint.transform.position, cameraRay.GetPoint(range), out RaycastHit hit, mask)){
                        //Debug.DrawLine(shootPoint.transform.position,hit.point, Color.red);
                        
                        laserLine.startColor = hitColor;
                        laserLine.endColor = hitColor;
                        laserLine.SetPosition(1,hit.point);
                        
                        laserLine.startWidth = .2f;
                        Debug.Log("hit");

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
    }
}
