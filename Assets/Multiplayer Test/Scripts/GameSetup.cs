using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    #region Public fields
    
    public static GameSetup instance;
    public Transform[] spawnPoints;
    
    #endregion

    #region MonoBehaviour Callbacks
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    #endregion
    
}
