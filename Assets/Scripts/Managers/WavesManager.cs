using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WavesManager : MonoBehaviour
{
    public static WavesManager instance;
    public List<WaveSpawner> waves;
    public UnityEvent onChanged;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null){
            instance = this;
        }else
            Debug.LogError("WavesManager duplicated", gameObject);
    }
    public void AddWave(WaveSpawner wave){
        waves.Add(wave);
        onChanged.Invoke();
    }
    public void RemoveWave(WaveSpawner wave){
        waves.Remove(wave);
        onChanged.Invoke();
    }


}
