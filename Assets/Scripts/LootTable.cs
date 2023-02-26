using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    
    public GameObject powerUp;
    private void Awake(){
        var life = GetComponent<Life>();
        life.onDeath.AddListener(spawnItem);
        

    }
    void spawnItem(){
         Debug.Log("Item Created");
         Instantiate(powerUp, transform.position, transform.rotation);
    }
   

}
