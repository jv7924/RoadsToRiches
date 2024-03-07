using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void LoadOfflineScene()
    {
        SceneManager.LoadScene("Classic 2P");
    }

    public void LoadOnlineScene()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene("Title");
    }


    // New Scenes
    public void LoadClassic2PScene()
    {
        SceneManager.LoadScene("Classic 2P");
    }

    public void LoadClassic4PScene()
    {
        SceneManager.LoadScene("Classic 4P");
    }

    public void LoadSharedHand2PScene()
    {
        SceneManager.LoadScene("SingleHand");
    }
}
