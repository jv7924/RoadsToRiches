using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casino : Road
{
    // Start is called before the first frame update
    void Start()
    {
        up    = new KeyValuePair<bool, Road>(true, null);
        down  = new KeyValuePair<bool, Road>(true, null);
        left  = new KeyValuePair<bool, Road>(true, null);
        right = new KeyValuePair<bool, Road>(true, null);
        rotation = 0;
    }
}
