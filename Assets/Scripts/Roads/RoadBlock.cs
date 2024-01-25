using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoadBlock : Road
{
    // Start is called before the first frame update
    void Start()
    {
        up = false;;
        down = true;
        left = false;
        right = false;
        rotation = 0;
        uniqueID = Guid.NewGuid();
    }

}
