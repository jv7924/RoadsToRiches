using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TIntersection : Road
{
    // Start is called before the first frame update
    void Start()
    {
        up    = new KeyValuePair<bool, Road>(false, null);
        down  = new KeyValuePair<bool, Road>(true, null);
        left  = new KeyValuePair<bool, Road>(true, null);
        right = new KeyValuePair<bool, Road>(true, null);
        rotation = 0;
    }

}
