using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    #region Private fields
    
    [Tooltip("Player prefab to be instantiated"), SerializeField]
    private GameObject playerPrefab;

    [Tooltip("Multiplayer scene index"), SerializeField]
    private int multiplayerScene = 1;
    
    #endregion

    #region Public fields
    
    public static PhotonRoom Room;
    public int currentScene = 0;

    public OnlineTurnSystem turnSystem;
    
    #endregion

    #region MonoBehaviour
    
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        /* 
            Set up singleton, sets photonRoom to this instance, if not this instance delete and set to this instance
        */
        if (Room == null)
        {
            Room = this;
        }
        else
        {
            if (Room != this)
            {
                Destroy(Room.gameObject);
                Room = this;
            }
        }

        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    #region MonoBehaviourPunCallbacks

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined a room");

         if (!PhotonNetwork.IsMasterClient)
            return;

        StartGame();
    }

    #endregion

    #region Private Methods
    
    private void StartGame()
    {
        Debug.Log("Loading level");
       
        PhotonNetwork.LoadLevel(multiplayerScene);
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;

        if (currentScene == multiplayerScene)
        {
            turnSystem = FindObjectOfType<OnlineTurnSystem>();
            // Instantiate player prefab then set this instance of the prefab as the local player for this device
            Invoke("CreatePlayer", 2f);
        }
    }

    private void CreatePlayer()
    {
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 0, 0), Quaternion.identity, 0);
        // OnlineTurnSystem turnSystem = new OnlineTurnSystem();
        turnSystem.SetLocalPlayer(player.transform.GetChild(0).gameObject);
    }
    
    #endregion
}
