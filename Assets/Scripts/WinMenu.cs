using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinMenu : MonoBehaviour
{
    private string currentScene;

    public void PlayAgain()
    {
        currentScene = SceneManager.GetActiveScene().name; 
        SceneManager.LoadScene(currentScene);
    }

    public void Home(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
