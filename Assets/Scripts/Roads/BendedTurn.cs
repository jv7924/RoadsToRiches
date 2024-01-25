using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BendedTurn : Road
{
    // Start is called before the first frame update
    void Start()
    {
        up = true;
        down = false;
        left = true;
        right = false;
        rotation = 0;
        uniqueID = Guid.NewGuid();
    }

}
