using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class VerticalLook : MonoBehaviour
{
    public float lookSpeed;
    public float maxAngle;
    private float upValue;
    private Vector3 rotation;
    public void OnLook(InputValue value){
        upValue = value.Get<Vector2>().y;
    }
    public void Start(){
        rotation.x =0;
    }
    // Update is called once per frame
    public void ResetCamera(){
        transform.localRotation = Quaternion.Euler(0,0,0);
    }
    void Update()
    {
       rotation = transform.localEulerAngles;
       
       rotation.x += upValue* -1 * lookSpeed * Time.deltaTime;
       //Debug.Log(rotation.x);
       if(rotation.x >= maxAngle && rotation.x < 180 ){
           rotation.x = maxAngle;
       }else if(rotation.x <= 360 - maxAngle && rotation.x > 180){
           rotation.x = -maxAngle;
       }else if(rotation.x < 360-maxAngle-10 && rotation.x > maxAngle+10){
            rotation.x = 0;
       }
       //Debug.Log(rotation.x);
       transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }
}
