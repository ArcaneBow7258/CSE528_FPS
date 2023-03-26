using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MenuScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public void Start(){
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    // Update is called once per frame
    //togglePuase
    public void OnStop(InputValue value)
    {
        Debug.Log(Time.timeScale);
        switch(Time.timeScale){
            case(0):
                
                pauseMenu.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
                break;
            case(1):
                
                pauseMenu.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                break;
            default:

                break;
        }
        
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ChangeVolume()
    {

    }
}
