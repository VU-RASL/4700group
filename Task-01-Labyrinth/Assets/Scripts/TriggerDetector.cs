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
        else if(gameObject.name.Contains("Move-") && collider.gameObject.name.Contains("Player-"))
        {
            //add new Special Move to Player's bank of available Special Moves
            //TODO: Find another way to cleanly handle a variety of moves
            if (gameObject.name == "Move-Jump")
                playerController.AddSpecialMove(new SMJump(collider.transform));
            else if (gameObject.name == "Move-Grow")
                playerController.AddSpecialMove(new SMGrow(collider.transform));
            else if (gameObject.name == "Move-Shrink")
                playerController.AddSpecialMove(new SMShrink(collider.transform));
            else if (gameObject.name == "Move-Thomas")
                playerController.AddSpecialMove(new SMThomas(collider.transform));

            //de-activate Special Move game object
            Destroy(gameObject);
        }
    }
}
