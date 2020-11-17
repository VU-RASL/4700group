using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGrow : SMove
{
    public SMGrow(Transform _playerTransform_) :
         base("grow", _playerTransform_)
    {

    }

    public override void Activate()
    {
        float growthScalar = 2F;

        m_playerTransform.localScale = m_playerTransform.localScale *= growthScalar;
    }
}
