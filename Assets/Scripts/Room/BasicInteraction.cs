using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInteraction : MonoBehaviour
{
    public bool enable = true;

    protected bool usedOnce = false;
    public virtual void Interact(GameObject sender){}

    public virtual bool IsUsedOnce(){return usedOnce;}
}
