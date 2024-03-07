using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject p1Canvas;
    [SerializeField] GameObject p2Canvas;
    private bool p1Current = false;
    private bool p2Current = false;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            Pause();
        } else if (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        if (p1Canvas.activeSelf){
            p1Canvas.SetActive(false);
            p1Current = true;
        }
        if (p2Canvas.activeSelf){
            p2Canvas.SetActive(false);
            p2Current = true;
        }
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        if (p1Current) 
        {
            p1Canvas.SetActive(true);
            p1Current = false;
        }
        if (p2Current)
        {
            p2Canvas.SetActive(true);
            p2Current = false;
        }
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }

}
