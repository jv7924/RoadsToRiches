using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoadBlock : Road
{
    // Start is called before the first frame update
    void Start()
    {
        up    = new KeyValuePair<bool, Road>(false, null);
        down  = new KeyValuePair<bool, Road>(true, null);
        left  = new KeyValuePair<bool, Road>(false, null);
        right = new KeyValuePair<bool, Road>(false, null);
        rotation = 0;
        uniqueID = Guid.NewGuid();
    }

}
