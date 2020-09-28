using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasStarsController : MonoBehaviour
{
    public void PlacePlayer(){
        //GameObject.FindObjectOfType<HockeyController>().PlacePlayer();
    }

    public void CompleteGame(){
        GameObject.FindObjectOfType<Constellations_Minigame>().ShowFinalText();
    }
}
