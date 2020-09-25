using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public int id; 
    public bool selected = false;

    public void DestroySelf(){
        Destroy(this.gameObject);
    }

    public void SetSelectedTrue(){selected = true;}
    public void SetSelectedFalse(){selected = false;}
}
