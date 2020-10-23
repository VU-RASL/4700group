using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ENTIRE SCRIPT: ADD TO CHANGE LOG
// TODO: Create centralized SMove queue (currently replicated here and in PlayerController.cs)
public class SMDisplayManager : MonoBehaviour
{
    private Queue<Sprite> m_imageQueue;
    public Sprite[] AvailableMoves;
    public Image[] imageSlots; //todo: dynamically set based on child objects of "Next Special Move"
    public Sprite emptySlot;

    void Start()
    {
        m_imageQueue = new Queue<Sprite>();
        this.RedrawMoveQueue();
    }

    public void AddMove(SMove _SMove_)
    {
        m_imageQueue.Enqueue(this.GetSpriteWithName(_SMove_.Name));
        this.RedrawMoveQueue();
    }

    public void RemoveMove()
    {
        m_imageQueue.Dequeue();
        this.RedrawMoveQueue();
    }

    private void RedrawMoveQueue()
    {
        int i = 0;
        for(; i < m_imageQueue.Count; i++)
            imageSlots[i].sprite = m_imageQueue.ToArray()[i];
        for (; i < imageSlots.Length; i++)
            imageSlots[i].sprite = emptySlot;
    }

    private Sprite GetSpriteWithName(string name)
    {
        for (int i = 0; i < AvailableMoves.Length; i++)
            if (AvailableMoves[i].name == name)
                return AvailableMoves[i];
        return null;
    }
}
