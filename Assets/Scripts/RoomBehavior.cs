using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehavior : MonoBehaviour
{
    public GameObject[] walls; // 0 - Up 1 -Down 2 - Right 3- Left
    public GameObject[] doors;
    
    public void UpdateRoom(bool[] status)
    {
        bool access = false;
        for (int i = 0; i < status.Length; i++)
        {
            access = access || status[i];
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
        if(!access){
            Destroy(gameObject);
        }
    }
}