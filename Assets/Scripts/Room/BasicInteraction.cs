using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInteraction : MonoBehaviour
{
    public bool enable = true;
    public virtual void Interact(){
        Debug.Log("hola soy un objeto");
    }
}
