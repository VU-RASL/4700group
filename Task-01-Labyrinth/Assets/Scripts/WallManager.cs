using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    private List<Rigidbody> m_walls;

    void Start()
    {
        m_walls = new List<Rigidbody>();
        foreach (Transform child in transform)
        {
            if (child.name.Contains("Wall"))
            {
                child.gameObject.AddComponent<WallBehavior>();
                m_walls.Add(child.GetComponent<Rigidbody>());
            }
        }

        this.SetWallConstraintsActive(true);
        this.SetWallMass(true);
    }
    
    //TODO: move behavior to WallBehavior.cs
    private void SetWallConstraintsActive(bool active)
    {
        foreach(Rigidbody rb in m_walls)
            rb.constraints = active ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None;
    }

    //TODO: move behavior to WallBehavior.cs
    private void SetWallMass(bool rigid)
    {
        foreach (Rigidbody rb in m_walls)
            rb.mass = rigid ? 1 : .25F;
    }

    public void EnableToppleWalls()
    {
        this.SetWallConstraintsActive(false);
        this.SetWallMass(false);
    }
}
