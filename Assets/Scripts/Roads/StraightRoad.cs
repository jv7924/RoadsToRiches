using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StraightRoad : Road
{
    // Start is called before the first frame update
    void Start()
    {
        up = false;
        down = false;
        left = true;
        right = true;
        rotation = 0;
        uniqueID = Guid.NewGuid();
    }

}
