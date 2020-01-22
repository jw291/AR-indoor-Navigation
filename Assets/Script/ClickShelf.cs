using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;

public class ClickShelf : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private bool isBtnDown = false;
    public GameObject text;

    void Start()
    {

    }

    void Update()
    {
        if (isBtnDown)
        {
            text.SetActive(true);
        }
        else
        {
            text.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isBtnDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isBtnDown = false;
    }
}
