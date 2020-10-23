using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AdversaryController : MonoBehaviour
{
    private GameObject m_player;
    private NavMeshAgent m_agent;
    
    void Start()
    {
        //TODO: Player-Default below is hardcoded; make dynamic
        m_player = GameObject.Find("PlayerObjects/Player-Default");
        m_agent = GameObject.Find("Adversary").GetComponent<NavMeshAgent>();
    }
     
    void Update()
    {
        if (!LabyrinthComplete.isComplete) //do not chase player after labyrinth is completed
        {
            Vector3 playerPos = m_player.transform.position;
            m_agent.SetDestination(playerPos);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Contains("Player"))
            LaunchGame.RestartLevel();
    }
}
