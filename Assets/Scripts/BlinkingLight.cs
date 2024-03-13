using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    [SerializeField] private Material firstMat;
    [SerializeField] private Material secondMat;
    [SerializeField] private float blinkInterval = 0.75f; // Time between color changes

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            // Toggle between white and yellow materials
            rend.material = firstMat;
            yield return new WaitForSeconds(blinkInterval);
            rend.material = secondMat;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
