using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSetup : MonoBehaviour
{
    void Start()
    {
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.AddComponent<TriggerDetector>();
            if(gameObject.name == "Triggers") //ADD TO CHANGE LOG
                child.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
