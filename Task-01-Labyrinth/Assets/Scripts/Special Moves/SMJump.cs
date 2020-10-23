using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMJump : SMove
{
    public SMJump(Transform _playerTransform_) :
        base("jump", _playerTransform_)
    {

    }

    public override void Activate()
    {
        //access Rigidbody component associated with the player Transform
        Rigidbody rb = m_playerTransform.GetComponent<Rigidbody>();

        //create an upward force vector
        float forceScalar = 2F;
        Vector3 up = Vector3.up * forceScalar;

        //apply the upward force on the player game object
        rb.AddForce(up, ForceMode.Impulse);
    }
}
