using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    private Dictionary<string, GameObject> m_portals;
    private PlayerController m_playerController;
    
    void Start()
    {
        m_portals = new Dictionary<string, GameObject>();
        foreach (Transform child in GameObject.Find("LabyrinthObjects/Portals").transform)
        {
            child.gameObject.AddComponent<Portal>();
            m_portals.Add(child.name, child.gameObject);
        }
        
        //TODO: "Player-Default" below is hardcoded; make dynamic
        m_playerController = GameObject.Find("Player-Default").GetComponent<PlayerController>();
    }
    
    public void Teleport(string _currPortal_)
    {
        string nextPortal = "Portal (" + this.NextPortalName(_currPortal_) + ")";

        if(m_portals.ContainsKey(nextPortal))
        {
            Vector3 newPos = m_portals[nextPortal].transform.position;
            m_playerController.Teleport(newPos);
        }
    }

    private string NextPortalName(string _currPortalName_)
    {
        int indexOpenParen = _currPortalName_.IndexOf('(')+1;
        int indexCloseParen = _currPortalName_.IndexOf(')');
        string portalNumber = _currPortalName_.Substring(indexOpenParen,indexCloseParen-indexOpenParen);
        
        return (Int32.Parse(portalNumber)+1).ToString();
    }
}
