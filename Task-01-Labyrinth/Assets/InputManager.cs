using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class InputManager : MonoBehaviour
{
    Dictionary<string, KeyCode> input_Actions = new Dictionary<string, KeyCode>();
    void OnEnable()
    {
        input_Actions["Jump"] = KeyCode.Space;
        input_Actions["Grow"] = KeyCode.G;
        input_Actions["Shrink"] = KeyCode.F;
    }

    public bool GetButtonDown(string buttonName)
    {
        
        if (input_Actions.ContainsKey(buttonName) == false)
            return false;

        
        return Input.GetKeyDown(input_Actions[buttonName]);
    }

    public string[] GetButtonNames()
    {
        return input_Actions.Keys.ToArray();
    }

    public string GetKeyName(string buttonName)
    {
        if (input_Actions.ContainsKey(buttonName) == false)
            return "N/A";

        return input_Actions[buttonName].ToString();
    }

    public void SetButton(string buttonName, KeyCode keyCode)
    {
        input_Actions[buttonName] = keyCode;
    }
}