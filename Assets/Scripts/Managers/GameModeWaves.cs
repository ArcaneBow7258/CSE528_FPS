using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class GameModeWaves : MonoBehaviour
{   
    [SerializeField] Life playerLife;
    [SerializeField] Life baseLife;
    void Start(){
        playerLife.onDeath.AddListener(OnPlayerDeath);
        baseLife.onDeath.AddListener(OnPlayerDeath);
        WavesManager.instance.onChanged.AddListener(CheckWinCondition);
        EnemyManager.instance.onChanged.AddListener(CheckWinCondition);
        
    }
    void CheckWinCondition()
    {
        if(EnemyManager.instance.enemies.Count <= 0 && WavesManager.instance.waves.Count <= 0){
            SceneManager.LoadScene("WinScene");
        }
        
    }
    void OnPlayerDeath(){
        SceneManager.LoadScene("LoseScreen");
    }
}
