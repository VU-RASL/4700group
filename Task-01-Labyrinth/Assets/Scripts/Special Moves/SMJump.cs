using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class SMJump : SMove 
{
    private float m_height=10F;
    private float m_velocity=10F;
    public void update()
    {
        Debug.Log("You are here now");
    }
    public SMJump(Transform _playerTransform_) :
        base("jump", _playerTransform_)
    {

    }
    public float height
    {
        set { this.m_height=value; }
    }
    public float velocity
    {
        set { this.m_velocity=value; }
    }
    public override void Activate()
    {
        //access Rigidbody component associated with the player Transform
        Rigidbody rb = m_playerTransform.GetComponent<Rigidbody>();

        Vector3 transform = rb.transform.position + Vector3.up * this.m_height;

        Vector3 direction = rb.transform.position - transform;

        rb.velocity = new Vector3(0, this.m_velocity, 0);

        rb.AddForceAtPosition(direction.normalized, transform);
    }
}
