using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public GameObject[] keyObjects;

    private GameProgress gp;

    void Awake(){
        gp = GameObject.FindObjectOfType<GameProgress>();
        if(gp){
            ReadGameProgress();
        }
    }

    void ReadGameProgress(){
        if(gp.hockeyMinigame){
            keyObjects[0].GetComponent<KeyObject>().enable = false;
        }
        if(gp.shellsMinigame){
            keyObjects[1].GetComponent<KeyObject>().enable = false;
        }
        if(gp.mapMinigame){
            keyObjects[2].GetComponent<KeyObject>().enable = false;
        }
        if(gp.photosMinigame){
            keyObjects[3].GetComponent<KeyObject>().enable = false;
        }
    }
}
