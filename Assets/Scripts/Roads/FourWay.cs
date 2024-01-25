using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FourWay : Road
{
    // Start is called before the first frame update
    void Start()
    {
        up = true;
        down = true;
        left = true;
        right = true;
        rotation = 0;
        uniqueID = Guid.NewGuid();
    }

}
