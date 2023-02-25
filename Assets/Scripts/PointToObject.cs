using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToObject : MonoBehaviour
{
    public GameObject target;
     Vector3 directionToPosition;
    // Update is called once per frame
    void Update()
    {
        directionToPosition = Vector3.Normalize(target.transform.position - transform.position);
        directionToPosition.y = 0;
        transform.forward = directionToPosition;
    }
}
