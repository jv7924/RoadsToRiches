using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Material baseColor, offsetColor;

    [SerializeField]
    private MeshRenderer renderer;

    [SerializeField]
    private GameObject[] environmentModels;

    public void Init(bool isOffset)
    {
        renderer.material = isOffset ? offsetColor : baseColor;
    }
}
