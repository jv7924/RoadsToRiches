using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody playerRB;
    public float dir;
    public PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
            dir = Input.GetAxisRaw("Horizontal");
            playerRB.MovePosition(transform.position + new Vector3(dir, 0f, 0f) * Time.deltaTime * 6);
    }
}
