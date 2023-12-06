using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseOverShrink : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private float shrinkSize, shrinkRate, shrinkInterval;

    [SerializeField]
    private CanvasScaler ui;

    private bool hovered;

    void Start()
    {
        hovered = false;
        shrinkSize = ui.scaleFactor;
    }

    private IEnumerator Enlarge()
    {
        while(hovered)
        {
            if(ui.scaleFactor < 1.0f)
            {
                ui.scaleFactor += shrinkRate;
            }
            else
            {
                ui.scaleFactor = 1.0f;
            }
            yield return new WaitForSeconds(shrinkInterval);
        }
    }

    private IEnumerator Shrink()
    {
        while(!hovered)
        {
            if(ui.scaleFactor > shrinkSize)
            {
                ui.scaleFactor -= shrinkRate;
            }
            else
            {
                ui.scaleFactor = shrinkSize;
            }
            yield return new WaitForSeconds(shrinkInterval);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
        StartCoroutine(Enlarge());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
        StartCoroutine(Shrink());
    }
}
