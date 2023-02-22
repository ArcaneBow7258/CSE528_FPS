using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public List<Enemy> enemies;
    public UnityEvent onChanged;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null){
            instance = this;
        }
        else{
            Debug.LogError("enemy manager duplicated, ignoring this one", gameObject);
        }
    }
    public void AddEnemy(Enemy enemy){
        enemies.Add(enemy);
        onChanged.Invoke();
    }
    public void RemoveEnemy(Enemy enemy){
        enemies.Remove(enemy);
        onChanged.Invoke();
    }
}
