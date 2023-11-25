using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseOverShrink : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private float shrink;

    [SerializeField]
    private CanvasScaler ui;

    void Start()
    {
        shrink = ui.scaleFactor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.scaleFactor = 1.0f;
        Debug.Log(ui.scaleFactor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.scaleFactor = shrink;
        Debug.Log(ui.scaleFactor);
    }
}
