using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerMovement : MonoBehaviour
{
    
    public float speed;
    public float rotationSpeed;
    private int mouseLock = 1;
    void Start(){
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0,0,mouseX * rotationSpeed * Time.deltaTime);
        if(Input.GetKey(KeyCode.W)){transform.Translate(0, -speed * Time.deltaTime, 0); }
        if(Input.GetKey(KeyCode.S)){transform.Translate(0, speed * Time.deltaTime, 0); }
        if(Input.GetKey(KeyCode.A)){transform.Translate(-speed * Time.deltaTime, 0, 0); }
        if(Input.GetKey(KeyCode.D)){transform.Translate(speed * Time.deltaTime, 0, 0); }
        if(Input.GetKeyDown(KeyCode.T)){
            if(mouseLock ==1){
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                mouseLock = (mouseLock + 1 )%2;
            }else{
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                mouseLock += 1;

            }

        };
    }
}
