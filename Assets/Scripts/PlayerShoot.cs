using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public GameObject prefab;
    public GameObject shootPoint;
    // Update is called once per frame
    public void OnFire(InputValue value)
    {   
        print("pewpew");
        //if(value.isPressed){
        GameObject clone = Instantiate(prefab);
        clone.transform.position = shootPoint.transform.position;
        clone.transform.rotation = shootPoint.transform.rotation;
        //}

            
        
    }
}
