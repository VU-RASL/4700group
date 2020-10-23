using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SplBtnMangr: Button
{
  private bool m_buttonPressed;

    protected override void Start()
    {
        base.Start();
        m_buttonPressed = false;
        this.onClick.AddListener(SplButtonClick);
    }

    
   public void SplButtonClick()
    {
       
        m_buttonPressed = true;
    }

    public bool ButtonPressed
    {
        get { return m_buttonPressed; }
        set { m_buttonPressed = value; }
    }
}
