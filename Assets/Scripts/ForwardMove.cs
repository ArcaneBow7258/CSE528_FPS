using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMove : MonoBehaviour
{
    public int axis;
    public float speed;
    

    // Update is called once per frame
    void Update()
    {   //test
        switch(axis)
        {
            case 1:
                transform.Translate(speed*Time.deltaTime,0,0 );
                break;
            case 2:
                transform.Translate(0,speed*Time.deltaTime,0 );
                break;
            case 3:
                transform.Translate(0,0,speed*Time.deltaTime );
                break;

        }
    }
}
