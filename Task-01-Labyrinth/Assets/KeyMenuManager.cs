using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class KeyMenuManager : MonoBehaviour
{
    InputManager input;
    public GameObject keyItemPrefab, keyList;

    string buttonToRebind = null;
    Dictionary<string, Text> buttonLabels;

    void Start()
    {
        input = GameObject.FindObjectOfType<InputManager>();
        string[] buttonNames = input.GetButtonNames();
        buttonLabels = new Dictionary<string, Text>();

        for (int i = 0; i < buttonNames.Length; i++)
        {
            string bn;
            bn = buttonNames[i];

            GameObject go = Instantiate(keyItemPrefab);
            go.transform.SetParent(keyList.transform);
            go.transform.localScale = Vector3.one;

            Text buttonName = go.transform.Find("Button Name").GetComponent<Text>();
            buttonName.text = bn;

            Text keyNameText = go.transform.Find("Button/Key Name").GetComponent<Text>();
            keyNameText.text = input.GetKeyName(bn);
            buttonLabels[bn] = keyNameText;

            Button keyBind = go.transform.Find("Button").GetComponent<Button>();
            keyBind.onClick.AddListener(() => { Rebind(bn); });
        }

    }
    void Update()
    {
        if (buttonToRebind != null)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode kc in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(kc))
                    {
                        input.SetButton(buttonToRebind, kc);
                        buttonLabels[buttonToRebind].text = kc.ToString();
                        buttonToRebind = null;
                        break;
                    }
                }
            }
        }
    }

    void Rebind(string buttonName)
    {
        buttonToRebind = buttonName;
    }
}
