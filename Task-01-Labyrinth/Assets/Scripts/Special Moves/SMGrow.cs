using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGrow : SMove
{
    private float growthScalar = 2F;
    public SMGrow(Transform _playerTransform_) :
         base("grow", _playerTransform_)
    {

    }
    public float growth_Scale
    {
        set { this.growthScalar = value; }
    }
    public override void Activate()
    {
    
        m_playerTransform.localScale = m_playerTransform.localScale *= growthScalar;
    }
}
