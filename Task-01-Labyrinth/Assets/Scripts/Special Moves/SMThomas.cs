using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMThomas : SMove
{
    public SMThomas(Transform _playerTransform_) :
         base("thomas", _playerTransform_)
    {

    }

    public override void Activate()
    {
        //step 1. change player model to Thomas
        GameObject.Find("PlayerObjects/Player-Default").SetActive(false);
        PlayerController.PlayerName = "Player-Thomas";
        GameObject.Find("PlayerObjects").transform.FindChild(PlayerController.PlayerName).gameObject.SetActive(true);
        
        //step 2. start playing Thomas' theme song
        GameObject.Find("PlayerObjects/Player-Thomas").GetComponent<AudioSource>().Play();

        //step 3. enable Thomas to knock down walls
        GameObject.Find("LabyrinthObjects/Walls").GetComponent<WallManager>().EnableToppleWalls();
    }
}