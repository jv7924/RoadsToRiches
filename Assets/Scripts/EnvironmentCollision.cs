using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Hell");
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Road")    || 
            other.CompareTag("Player1") || 
            other.CompareTag("Player2") || 
            other.CompareTag("Player3") || 
            other.CompareTag("Player4"))
            Destroy(gameObject);
    }
}
