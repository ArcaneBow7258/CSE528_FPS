using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int amount;
    void Awake()
    {
        if(instance == null){
            instance = this;
        }
        else{
            Debug.LogError("score manager duplicated, ignoring this one", gameObject);
        }


    }
}
