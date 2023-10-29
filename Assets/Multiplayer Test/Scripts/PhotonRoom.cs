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
    
    public static PhotonRoom photonRoom;

    public int currentScene = 0;
    
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
        if (photonRoom == null)
        {
            photonRoom = this;
        }
        else
        {
            if (photonRoom != this)
            {
                Destroy(photonRoom.gameObject);
                photonRoom = this;
            }
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            CreatePlayer();
        }
    }

    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(Random.Range(0, 5), 0, 0), Quaternion.identity, 0);
    }
    
    #endregion
}
