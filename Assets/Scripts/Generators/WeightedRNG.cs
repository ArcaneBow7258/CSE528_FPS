using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weightedRNG{
    private float[] chances;
    private GameObject[] prefabs;
    private float totalChance = 0; 
    private SortedList<float, GameObject> table = new SortedList<float, GameObject>(); // relative weights please
    public weightedRNG(float[] c, GameObject[] g){
        chances = c;
        prefabs = g;
        if(c.Length != g.Length){
            throw new UnityException("You do not have a room for every chance or a chance for each room");
        }else{
            for(int i = 0; i < c.Length; i++)
            {
                table.Add(c[i],g[i]);
                totalChance += c[i];
            }
        }
    }
    public GameObject gen(){
        float random = Random.Range(0,totalChance);
        //Debug.Log("Init random " + random);
        GameObject room = null;
        foreach(var entry in table){
            if(random <= entry.Key){
                room = entry.Value;
                break;
            }else{
                random -= entry.Key;
                //Debug.Log("next random" + totalChance);
            }
        }
        return room;
    }
}
