using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody m_rigidbody;
    private int m_forceScalar;
    private bool m_atDestination;
    private bool m_freezePlayer;
    private enum INPUT_MODE {KEYBOARD,BROWSER};
    private INPUT_MODE m_inputMode;
    private NavBtnManager[] m_navBtns; //clockwise from top: up, right, down, left
    private Queue<SMove> m_specialMoves; //ADD TO CHANGE LOG
    private SMDisplayManager m_SMDisplayManager; //ADD TO CHANGE LOG

    public static string PlayerName = "Player-Default";
    public static string PlayerNameDefault = "Player-Default";

    void Start()
    {
        Vector3 startPos = GameObject.Find("LabyrinthObjects/Triggers/Start").GetComponent<Transform>().position;
        gameObject.transform.position = startPos;
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
        m_forceScalar = 123;
        m_atDestination = false;
        m_freezePlayer = false;
        m_inputMode = INPUT_MODE.KEYBOARD;
        m_navBtns = new NavBtnManager[4];
        m_navBtns[0] = GameObject.Find("Canvas/BtnUp").GetComponent<NavBtnManager>();
        m_navBtns[1] = GameObject.Find("Canvas/BtnRight").GetComponent<NavBtnManager>();
        m_navBtns[2] = GameObject.Find("Canvas/BtnDown").GetComponent<NavBtnManager>();
        m_navBtns[3] = GameObject.Find("Canvas/BtnLeft").GetComponent<NavBtnManager>();
        m_specialMoves = new Queue<SMove>(); //ADD TO CHANGE LOG
        m_SMDisplayManager = GameObject.Find("Canvas/Image/Next Special Move").GetComponent<SMDisplayManager>(); //ADD TO CHANGE LOG
    }

    void Update()
    {
        float vertical = 0, horizontal = 0;
        this.GetPlayerInput(ref vertical, ref horizontal);

        m_atDestination = LabyrinthComplete.isComplete;

        if (!m_atDestination) {
            Vector3 force = new Vector3(horizontal * Time.deltaTime, 0, vertical * Time.deltaTime);
            m_rigidbody.AddForce((float)m_forceScalar * force);
        }
        else {
            if(!m_freezePlayer) {
                m_freezePlayer = true;
                m_rigidbody.velocity = .01F * m_rigidbody.velocity;
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (m_specialMoves.Count > 0)
            {
                m_specialMoves.Dequeue().Activate(); //remove move from local queue
                m_SMDisplayManager.RemoveMove(); //remove move from UI
            }
        }
    }

    private void GetPlayerInput(ref float _vertical_, ref float _horizontal_)
    {
        if (m_inputMode == INPUT_MODE.KEYBOARD) {
            _vertical_ = Input.GetAxis("Vertical");
            _horizontal_ = Input.GetAxis("Horizontal");
        }
        else if(m_inputMode == INPUT_MODE.BROWSER) {
            _vertical_ = m_navBtns[0].ButtonPressed ? 1 : (m_navBtns[2].ButtonPressed ? -1 : 0);
            _horizontal_ = m_navBtns[1].ButtonPressed ? 1 : (m_navBtns[3].ButtonPressed ? -1 : 0);
        }
    }

    public void Teleport(Vector3 newPos)
    {
        transform.position = newPos;
        m_rigidbody.velocity = Vector3.zero;
    }

    public void AtDestination()
    {
        m_atDestination = true;
    }

    public bool toggleState = true;
    public void ToggleInputMode()
    {
        toggleState = !toggleState;

        if (toggleState)
            m_inputMode = INPUT_MODE.KEYBOARD;
        else
            m_inputMode = INPUT_MODE.BROWSER;
    }

    //BELOW: ADD TO CHANGE LOG
    public void AddSpecialMove(SMove _move_)
    {
        m_specialMoves.Enqueue(_move_); //add move to local queue
        m_SMDisplayManager.AddMove(_move_); //add move to UI
    }
}
