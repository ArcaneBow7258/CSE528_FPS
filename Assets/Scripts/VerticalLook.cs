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
    // Update is called once per frame
    void Update()
    {
       rotation = transform.localEulerAngles;
       //Debug.Log(upValue* -1 * lookSpeed * Time.deltaTime);
       rotation.x += upValue* -1 * lookSpeed * Time.deltaTime;
       if(rotation.x >= maxAngle && rotation.x < 180 ){
           rotation.x = maxAngle;
       }else if(rotation.x <= 360 - maxAngle && rotation.x > 180){
           rotation.x = -maxAngle;
       }
       //Debug.Log(rotation.x);
       transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }
}
