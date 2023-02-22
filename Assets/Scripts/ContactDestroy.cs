using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDestroy : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        Destroy(gameObject);
        Destroy(other.gameObject);


    }
}
