using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public void PlacePlayer(){
        GameObject.FindObjectOfType<HockeyController>().PlacePlayer();
    }

    public void CompleteGame(){
        GameObject.FindObjectOfType<HockeyController>().ShowFinalText();
    }
}
