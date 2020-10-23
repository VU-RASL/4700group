using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private PortalManager m_portalManager;

    void Start()
    {
        m_portalManager = GameObject.Find("ScriptHub").GetComponent<PortalManager>();
    }
    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggering portal " + gameObject.name + "...");
        m_portalManager.Teleport(gameObject.name);
    }
}
