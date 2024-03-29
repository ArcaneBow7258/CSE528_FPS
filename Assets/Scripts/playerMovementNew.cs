using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovementNew : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float rotationSpeed;
    //private int mouseLock = 1;
    private Vector2 movementValue;
    private float lookValue;
    private Animator animator;
    



    void Start(){
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

    }
    // Update is called once per frame
    public void OnMove(InputValue value){
        movementValue = value.Get<Vector2>() * speed;
    }
    public void OnLook(InputValue value){
        lookValue = value.Get<Vector2>().x * rotationSpeed;
       
    }
    
    public void Update(){
        //Debug.Log(movementValue.y * Time.deltaTime);
        rb.AddRelativeForce(movementValue.x * Time.deltaTime, 0,movementValue.y * Time.deltaTime );
        rb.AddRelativeTorque(0,lookValue * Time.deltaTime,0);
        animator.SetFloat("Velocity",Mathf.Abs(movementValue.x * Time.deltaTime)+Mathf.Abs(movementValue.y * Time.deltaTime) );

    }
    
}
