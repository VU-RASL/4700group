using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    private LabyrinthComplete labyrinthCompleteScript;
    private PlayerController playerController;

    void Start()
    {
        labyrinthCompleteScript = GameObject.Find("ScriptHub").GetComponent<LabyrinthComplete>();
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collision occurred on GameObject " + gameObject.name);

        playerController = GameObject.Find("PlayerObjects/" + PlayerController.PlayerName).GetComponent<PlayerController>();

        if (gameObject.name == "End" && collider.gameObject.name.Contains("Player-"))
            labyrinthCompleteScript.Completed();
    }
}
