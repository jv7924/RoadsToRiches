using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    // References to the menu canvases
    [SerializeField] private GameObject GameModeSelectionMenu;
    [SerializeField] private GameObject PlayerCountSelectionMenu;
    [SerializeField] private GameObject HowToPlayMenu;
    [SerializeField] private GameObject GameModeDetailsMenu;

    // Variables used for saving the options selected by the player
    private string gameMode = "";
    private string playerCount = "";


    // Enable and disable the appropriate canvases at the start
    void Start()
    {
        GameModeSelectionMenu.SetActive(true);
        PlayerCountSelectionMenu.SetActive(false);
        HowToPlayMenu.SetActive(false);
        GameModeDetailsMenu.SetActive(false);
    }
    
    // Used to enable and disable canvases
    public void MoveMenuForward()
    {
        GameModeSelectionMenu.SetActive(false);
        PlayerCountSelectionMenu.SetActive(true);
    }

    public void MoveMenuBackward()
    {
        GameModeSelectionMenu.SetActive(true);
        PlayerCountSelectionMenu.SetActive(false);
    }

    public void OpenTutorialMenu()
    {
        GameModeSelectionMenu.SetActive(false);
        HowToPlayMenu.SetActive(true);
    }

    public void MoveTutorialMenuForward()
    {
        HowToPlayMenu.SetActive(false);
        GameModeDetailsMenu.SetActive(true);
    }

    public void MoveTutorialMenuBackward()
    {
        HowToPlayMenu.SetActive(true);
        GameModeDetailsMenu.SetActive(false);
    }

    public void CloseTutorialMenu()
    {
        GameModeSelectionMenu.SetActive(true);
        HowToPlayMenu.SetActive(false);
    }

    public void LoadSelectedScene()
    {
        if (gameMode == "Classic")
        {
            if (playerCount == "2")
            {
                SceneManager.LoadScene("Classic 2P");
            }
            else if (playerCount == "4")
            {
                SceneManager.LoadScene("Classic 4P");
            }
        }

        else if (gameMode == "Speed")
        {
            if (playerCount == "2")
            {
                SceneManager.LoadScene("Speed 2P");
            }
            else if (playerCount == "4")
            {
                SceneManager.LoadScene("Speed 4P");
            }
        }

        else if (gameMode == "Shared")
        {
            if (playerCount == "2")
            {
                SceneManager.LoadScene("SingleHand 2P");
            }
            else if (playerCount == "4")
            {
                SceneManager.LoadScene("SingleHand 4P");
            }
        }
    }

    // Used for the GameModeSelectionMenu to save the option chosen
    public void ChooseClassic()
    {
        gameMode = "Classic";
        MoveMenuForward();
    }

    public void ChooseSpeed()
    {
        gameMode = "Speed";
        MoveMenuForward();
    }

    public void ChooseShared()
    {
        gameMode = "Shared";
        MoveMenuForward();
    }

    public void ChooseTutorial()
    {
        OpenTutorialMenu();
    }
    
    // Used for the GameModeSelectionMenu to save the option chosen
    public void Choose2Player()
    {
        playerCount = "2";
        LoadSelectedScene();
    }

    public void Choose4Player()
    {
        playerCount = "4";
        LoadSelectedScene();
    }

    public void ChooseBack()
    {
        MoveMenuBackward();
    }

    // Functions for loading scenes
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("Title");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
