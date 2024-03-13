using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Animator anim;
    [SerializeField] GameObject p1Canvas;
    [SerializeField] GameObject p2Canvas;
    [SerializeField] GameObject p3Canvas;
    [SerializeField] GameObject p4Canvas;
    private bool p1Current = false;
    private bool p2Current = false;
    private bool p3Current = false;
    private bool p4Current = false;
    //private bool isPaused = false;
    private string currentScene;

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        // {
        //     Pause();
        // } else if (isPaused)
        // {
        //     if (Input.GetKeyDown(KeyCode.Escape))
        //     {
        //         Resume();
        //     }
        // }
    }

    public void Pause()
    {
        // anim.SetTrigger("Open");
        // if (p1Canvas.activeSelf){
        //     p1Canvas.SetActive(false);
        //     p1Current = true;
        // }
        // if (p2Canvas.activeSelf){
        //     p2Canvas.SetActive(false);
        //     p2Current = true;
        // }
        // Invoke(nameof(Stop), 0.5f);
        // isPaused = true;

        anim.SetTrigger("Open");

        if (p1Canvas.activeSelf){
            p1Canvas.SetActive(false);
            p1Current = true;
        } else if (p2Canvas.activeSelf){
            p2Canvas.SetActive(false);
            p2Current = true;
        } else if (p3Canvas.activeSelf){
            p3Canvas.SetActive(false);
            p3Current = true;
        } else if (p4Canvas.activeSelf){
            p4Canvas.SetActive(false);
            p4Current = true;
        }

        Invoke(nameof(Stop), 0.5f);
    }

    public void Resume()
    {
        // anim.SetTrigger("Close");
        // if (p1Current) 
        // {
        //     p1Canvas.SetActive(true);
        //     p1Current = false;
        // }
        // if (p2Current)
        // {
        //     p2Canvas.SetActive(true);
        //     p2Current = false;
        // }
        // Time.timeScale = 1f;
        // isPaused = false;

        anim.SetTrigger("Close");

        if (p1Current) 
        {
            p1Canvas.SetActive(true);
            p1Current = false;
        } else if (p2Current)
        {
            p2Canvas.SetActive(true);
            p2Current = false;
        } else if (p3Current)
        {
            p3Canvas.SetActive(true);
            p3Current = false;
        } else if (p2Current)
        {
            p4Canvas.SetActive(true);
            p4Current = false;
        }

        Time.timeScale = 1f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        currentScene = SceneManager.GetActiveScene().name; 
        SceneManager.LoadScene(currentScene);
    }

    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }

    public void Stop()
    {
        Time.timeScale = 0f;
    }
}
