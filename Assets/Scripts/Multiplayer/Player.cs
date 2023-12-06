using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        OnlineTurnSystem.instance.AddPlayersToList(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
