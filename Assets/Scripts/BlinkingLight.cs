using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    [SerializeField] private Material whiteMat;
    [SerializeField] private Material yellowMat;
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
            rend.material = whiteMat;
            yield return new WaitForSeconds(blinkInterval);
            rend.material = yellowMat;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
