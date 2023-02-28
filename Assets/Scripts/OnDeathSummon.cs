using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathSkull : MonoBehaviour
{
    public GameObject particles;
    public float yOffset;
    private void Awake(){
        var life = GetComponent<Life>();
        life.onDeath.AddListener(onDead);
    }
    void onDead(){
         Instantiate(particles, new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z), transform.rotation);
    }
}
