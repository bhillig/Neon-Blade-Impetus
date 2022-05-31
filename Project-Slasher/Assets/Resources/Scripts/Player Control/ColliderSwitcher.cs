using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSwitcher : MonoBehaviour
{
    public List<Collider> states;
    public int startCollider;
    private int currentIndex;

    private void Awake()
    {
        SwitchToCollider(startCollider);
        currentIndex = startCollider;
    }

    public void SwitchToCollider(int index)
    {
        if (index >= 0 && index < states.Count)
        {
            foreach (var v in states)
                v.enabled = false;
            states[index].enabled = true;
        }
        currentIndex = index;
    }

    public Collider GetCollider(int index)
    {
        return states[index];
    }

    public Collider GetCurrentCollider()
    {
        return states[currentIndex];
    }
}
