using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInteraction : MonoBehaviour
{
    public bool enable = true;
    public Material outline;
    public virtual void Interact(GameObject sender){
        Debug.Log("hola soy un objeto");
    }
}
