using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casino : Road
{
    // Start is called before the first frame update
    void Start()
    {
        up    = new KeyValuePair<bool, GameObject>(true, null);
        down  = new KeyValuePair<bool, GameObject>(true, null);
        left  = new KeyValuePair<bool, GameObject>(true, null);
        right = new KeyValuePair<bool, GameObject>(true, null);
        rotation = 0;
    }
}
