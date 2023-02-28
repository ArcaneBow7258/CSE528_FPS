using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOT : MonoBehaviour
{
    public float duration;
    public float dps;
    public float radius;
    public LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,duration + 1f/60f);
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position,radius,mask);
            for(int i = 0; i<hits.Length; i++){
                Life hitLife = hits[i].GetComponent<Life>();
                if(hitLife != null){
                            hitLife.amount -= dps *Time.deltaTime;
                }
            }
    }
}
