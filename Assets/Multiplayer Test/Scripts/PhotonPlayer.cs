using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.IO;

public class PhotonPlayer : MonoBehaviour
{
    #region Private fields
    
    private PhotonView photonView;
    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        int spawn = Random.Range(0, GameSetup.instance.spawnPoints.Length);

        if (photonView.IsMine)
        {
            // PhotonNetwork.Instantiate()
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
