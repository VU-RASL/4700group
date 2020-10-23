using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
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
    private SplBtnMangr m_jumBtn;
    private SplBtnMangr m_growBtn;
    private SplBtnMangr m_shrinkBtn;
    private Queue<SMove> m_specialMoves; //ADD TO CHANGE LOG
    private SMDisplayManager m_SMDisplayManager; //ADD TO CHANGE LOG
    private SMJump _sMJump;
    private InputField m_jumpHeight;
    private InputField m_jumpVelocity;
    private SMGrow _sMGrow;
    private SMShrink _sMShrink;
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
        m_jumBtn = GameObject.Find("Canvas/JumpButton").GetComponent<SplBtnMangr>();
        m_shrinkBtn = GameObject.Find("Canvas/ShrinkButton").GetComponent<SplBtnMangr>();
        m_jumpHeight = GameObject.Find("Canvas/JumpButton/Height").GetComponent<InputField>();
        m_jumpVelocity = GameObject.Find("Canvas/JumpButton/Velocity").GetComponent<InputField>();
        m_growBtn= GameObject.Find("Canvas/GrowButton").GetComponent<SplBtnMangr>();
        m_specialMoves = new Queue<SMove>(); //ADD TO CHANGE LOG
        m_SMDisplayManager = GameObject.Find("Canvas/Image/Next Special Move").GetComponent<SMDisplayManager>(); //ADD TO CHANGE LOG
    }

    void Update()
    {
        float vertical = 0, horizontal = 0;
        this.GetPlayerInput(ref vertical, ref horizontal);
        
        //this.GetSpecialMovInput();
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
        if (m_inputMode == INPUT_MODE.KEYBOARD)
        {
            if (Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.J))
            {
               _sMJump = new SMJump(gameObject.transform);
                try
                {
                    _sMJump.height = float.Parse(m_jumpHeight.text);
                }
                catch
                { }
                try
                {
                    _sMJump.velocity = float.Parse(m_jumpVelocity.text);
                }
                catch
                { }

                AddSpecialMove(_sMJump);
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                _sMGrow = new SMGrow(gameObject.transform);
              
                AddSpecialMove(_sMGrow);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                _sMShrink = new SMShrink(gameObject.transform);

                AddSpecialMove(_sMShrink);
            }
        }

        ExecuteSpecialMoves();
    }
    private void ExecuteSpecialMoves()
    {
       while(m_specialMoves.Count > 0)
        {
            Debug.Log("the count is above 0!");
            m_specialMoves.Dequeue().Activate();
        }
    }
    private void GetPlayerInput(ref float _vertical_, ref float _horizontal_)
    {
        GetSpecialMovInput();
        if (m_inputMode == INPUT_MODE.KEYBOARD) {
            _vertical_ = Input.GetAxis("Vertical");
            _horizontal_ = Input.GetAxis("Horizontal");
        }
        else if(m_inputMode == INPUT_MODE.BROWSER) {
            _vertical_ = m_navBtns[0].ButtonPressed ? 1 : (m_navBtns[2].ButtonPressed ? -1 : 0);
            _horizontal_ = m_navBtns[1].ButtonPressed ? 1 : (m_navBtns[3].ButtonPressed ? -1 : 0);
            
        }
    }
    private void GetSpecialMovInput()
    {
        if (m_jumBtn.ButtonPressed)
       {
            if (m_inputMode == INPUT_MODE.BROWSER)
            {
                _sMJump = new SMJump(gameObject.transform);
                AddSpecialMove(_sMJump);
            }
            m_jumBtn.ButtonPressed = false;
            Debug.Log("player control jump seted!");
        }
        if (m_growBtn.ButtonPressed)
        {
            if (m_inputMode == INPUT_MODE.BROWSER)
            {
                _sMGrow = new SMGrow(gameObject.transform);
                AddSpecialMove(_sMGrow);
            }
            m_growBtn.ButtonPressed = false;
            Debug.Log("player control jump seted!");
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
        //m_SMDisplayManager.AddMove(_move_); //add move to UI
    }
}
