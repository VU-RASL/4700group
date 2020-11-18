using UnityEngine;

public class SpecialMoves : MonoBehaviour
{
    public LayerMask ground;
    private Transform tf;
    private Rigidbody rb;
    public SphereCollider sc;
    private float height = 2F;
    private float gScale = 2F;
    private float sScale =  0.5F;
    private bool isShrunk = false;
    private bool isGrown = false;
    private bool normal = true;
    private bool midAir;

    private void Start()
    {
        tf = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody>();
        sc = gameObject.GetComponent<SphereCollider>();
    }
    public void Jump() //TODO: Remove infinite jumps
    {
        if (!midAir)
        {
            rb.AddForce(Vector3.up * height, ForceMode.Impulse);
            midAir = true;
        }
    }
    public void Grow()
    {
        if (!isGrown && normal)
        {
            tf.localScale *= gScale;
            isGrown = true;
            normal = false;
        }
        else if (!isGrown && isShrunk)
        {
            tf.localScale *= gScale;
            isShrunk = false;
            normal = true;
        }
    }
    public void Shrink()
    {
        if (!isShrunk && normal)
        {
            tf.localScale *= sScale;
            isShrunk = true;
            normal = false;
        }
        else if (!isShrunk && isGrown)
        {
            tf.localScale *= sScale;
            isGrown = false;
            normal = true;
        }
    } 
    private void OnCollisionEnter() {
        midAir = false;
    }
    private void OnCollisionExit() {
        midAir = true;
    }
}
