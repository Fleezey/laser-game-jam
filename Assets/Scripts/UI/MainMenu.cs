using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("#TODO move to next scene");
        //SceneManager.LoadScene();
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
