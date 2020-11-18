using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private SpecialMoves sm;
    private InputManager input;
    private Rigidbody m_rigidbody;
    private int m_forceScalar;
    private bool m_atDestination;
    private bool m_freezePlayer;
    private string move_name;
    public static string PlayerName = "Player-Default";
    public static string PlayerNameDefault = "Player-Default";
    private Text countText;
    private int count;
    public GameObject gateWall;
    private bool StartCrossed;
    void Start()
    {
        input = GameObject.FindObjectOfType<InputManager>();
        Vector3 startPos = GameObject.Find("LabyrinthObjects/Triggers/Start").GetComponent<Transform>().position;
        gameObject.transform.position = startPos;
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
        sm = gameObject.AddComponent<SpecialMoves>();
        m_forceScalar = 123;
        m_atDestination = false;
        m_freezePlayer = false;
        StartCrossed = false;
        try
        {
            countText = GameObject.Find("LabyrinthObjects/DisplayGameObject/TextObject/Canvas/Text").GetComponent<Text>();
        }
        catch { }

        count = 0;
        if (countText != null)
        {
            setCountText();
        }
    }

    void Update()
    {
        float vertical = 0, horizontal = 0;

        this.GetPlayerInput(ref vertical, ref horizontal);

        m_atDestination = LabyrinthComplete.isComplete;

        if (!m_atDestination)
        {
            Vector3 force = new Vector3(horizontal * Time.deltaTime, 0, vertical * Time.deltaTime);
            m_rigidbody.AddForce((float)m_forceScalar * force);
        }
        else
        {
            if (!m_freezePlayer)
            {
                m_freezePlayer = true;
                m_rigidbody.velocity = .01F * m_rigidbody.velocity;
            }
        }
    }
    private void GetPlayerInput(ref float _vertical_, ref float _horizontal_)
    {
        _vertical_ = Input.GetAxis("Vertical");
        _horizontal_ = Input.GetAxis("Horizontal");
                
        if (input.GetButtonDown("Jump"))
        {
            Debug.Log("Commencing jump");
            sm.Jump();
        }

        else if (input.GetButtonDown("Grow"))
            sm.Grow();

        else if (input.GetButtonDown("Shrink"))
            sm.Shrink();
    }
    public void Teleport(Vector3 newPos)
    {
        transform.position = newPos;
        m_rigidbody.velocity = Vector3.zero;
        count = 0;
        setCountText();

    }
    void setCountText()
    {
        countText.text = "Count: " + count.ToString();

    }

    public void AtDestination()
    {
        m_atDestination = true;
    }
    void OnTriggerEnter(Collider other)
    {

        Debug.Log("colided1 "+ other.gameObject.tag);
        if(other.gameObject.tag == "Start")
        {
            StartCrossed = true;
        }
        
        if (other.gameObject.tag == "Finish")
        {
            Debug.Log("colided2" + other.gameObject.tag);
            Debug.Log("colided1");
            other.gameObject.SetActive(true);
            if (StartCrossed)
            {
                count = count + 1;
                setCountText();
                StartCrossed = false;
                Debug.Log("loop completed");
            }
        }
        
        if(count==3)
        {
            try
            {
                GameObject.Find("LabyrinthObjects/Walls/Gate").SetActive(false);
                Debug.Log("Gate colided " + other.gameObject.tag);
            }
            catch { }

          if (other.gameObject.tag=="Gate")
            {
                other.gameObject.SetActive(false);
                Debug.Log("wall colided " + other.gameObject.tag);
            }
        }
    }
    
}
