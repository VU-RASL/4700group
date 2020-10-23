using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    private MeshRenderer m_renderer;
    private Vector3 m_initialPosition;

    public void Start()
    {
        m_renderer = gameObject.GetComponent<MeshRenderer>();
        m_initialPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name.Contains("Player"))
        {
            if (Vector3.Distance(m_initialPosition, transform.position) > .05F)
            {
                this.DeleteWall();
            }
        }
    }

    public void DeleteWall()
    {
        StartCoroutine(WallFlickerAndDelete());
    }
    
    IEnumerator WallFlickerAndDelete()
    {
        //TODO: do this in a loop
        m_renderer.enabled = false;
        yield return new WaitForSeconds(0.2F);
        m_renderer.enabled = true;
        yield return new WaitForSeconds(0.2F);
        m_renderer.enabled = false;
        yield return new WaitForSeconds(0.2F);
        m_renderer.enabled = true;
        yield return new WaitForSeconds(0.2F);
        gameObject.SetActive(false);
    }
}
