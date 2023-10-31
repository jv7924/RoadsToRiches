using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;


public class PhotonLobby : MonoBehaviourPunCallbacks
{
    #region Public fields

    public static PhotonLobby Lobby;
    public GameObject battleButton;
    public GameObject cancelButton;

    #endregion

    #region MonoBehaviour callbacks

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        Lobby = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();   // Connects to Master photon server
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Pun Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to Photon master server");

        PhotonNetwork.AutomaticallySyncScene = true;
        battleButton.SetActive(true);
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join a room but failed");
        
        CreateRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create a room but failed");
        
        CreateRoom();
    }

    #endregion

    #region Private Methods

    private void CreateRoom()
    {
        Debug.Log("Trying to create a new room");

        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 };
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOptions);
    }

    #endregion

    #region Public Methods

    public void OnBattleButtonClicked()
    {
        Debug.Log("Play button clicked");

        battleButton.SetActive(false);
        cancelButton.SetActive(true);

        PhotonNetwork.JoinRandomRoom();
    }

    public void OnCancelButtonClicked()
    {
        Debug.Log("Cancel button clicked");

        cancelButton.SetActive(false);
        battleButton.SetActive(true);

        PhotonNetwork.LeaveRoom();
    }

    #endregion
}
