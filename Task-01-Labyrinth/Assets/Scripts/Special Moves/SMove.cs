using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMove
{
    protected string m_name;
    protected Transform m_playerTransform;

    public SMove(string _name_, Transform _playerTransform_)
    {
        m_name = _name_;
        m_playerTransform = _playerTransform_;
    }

    public virtual void Activate()
    {
    }

    public string Name
    {
        get { return m_name; }
    }
}
