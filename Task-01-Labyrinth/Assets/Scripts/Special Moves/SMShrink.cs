using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMShrink : SMove
{
    public SMShrink(Transform _playerTransform_) :
         base("shrink", _playerTransform_)
    {

    }

    public override void Activate()
    {
        float growthScalar = 0.2F;

        m_playerTransform.localScale = m_playerTransform.localScale *= growthScalar;
    }
}