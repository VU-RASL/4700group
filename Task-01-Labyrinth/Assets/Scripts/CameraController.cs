using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform m_playerTransform;

    void Start()
    {
        //TODO: "Player-Default" below is hardcoded; make dynamic
        m_playerTransform = GameObject.Find("PlayerObjects/Player-Default").GetComponent<Transform>();
    }
    
    void Update()
    {
        //Camera.main.transform.LookAt(m_playerTransform);
    }
}
