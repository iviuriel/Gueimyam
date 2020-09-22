using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public GameObject[] keyObjects;

    private GameProgress gp;

    void Awake(){
        gp = GameObject.FindObjectOfType<GameProgress>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ReadGameProgress();
    }

    void ReadGameProgress(){
        if(gp.hockeyMinigame){
            Destroy(keyObjects[0].GetComponent<KeyObject>());
        }
        if(gp.shellsMinigame){
            Destroy(keyObjects[1].GetComponent<KeyObject>());
        }
        if(gp.mapMinigame){
            Destroy(keyObjects[2].GetComponent<KeyObject>());
        }
        if(gp.photosMinigame){
            Destroy(keyObjects[3].GetComponent<KeyObject>());
        }
    }
}
