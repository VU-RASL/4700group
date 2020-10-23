using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavBtnManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool m_buttonPressed;

    void Start()
    {
        m_buttonPressed = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_buttonPressed = true;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        m_buttonPressed = false;
    }

    public bool ButtonPressed
    {
        get { return m_buttonPressed; }
    }
}
