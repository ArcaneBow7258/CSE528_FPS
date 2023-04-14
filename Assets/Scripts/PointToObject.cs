using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToObject : MonoBehaviour
{
    public string target;
    private GameObject t;
     Vector3 directionToPosition;
    void Start(){
        t = GameObject.Find(target);
    }
    // Update is called once per frame
    void Update()
    {
        directionToPosition = Vector3.Normalize(t.transform.position - transform.position);
        directionToPosition.y = 0;
        transform.forward = directionToPosition;
    }
}
