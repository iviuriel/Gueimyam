using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    private static bool created = false;

    public bool hockeyMinigame {get; set;}
    public bool mapMinigame {get; set;}
    public bool shellsMinigame {get; set;}
    public bool photosMinigame {get; set;}

    public bool photoBaby {get; set;}

    public bool isFirstTime {get; set;}



    public bool objectSwitch {get; set;}
    public bool objectMirror {get; set;}
    public bool objectPanflet {get; set;}
    public bool objectMobile {get; set;}

    //PLAYER INFO
    private PlayerInfo playerInfo;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(gameObject);
            created = true;

            hockeyMinigame = false;
            mapMinigame = false;
            shellsMinigame = false;
            photosMinigame = false;
            photoBaby = false;

            objectSwitch = false;
            objectMirror = false;
            objectPanflet = false;
            objectMobile = false;

            isFirstTime = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetPlayerInfo(Vector3 p, Vector3 r, Vector3 cp, Vector3 cr){
        playerInfo = new PlayerInfo(p, r, cp, cr);
    }

    public PlayerInfo GetPlayerInfo() {return playerInfo;}

    public void SetUsedObject(SecondaryObject.Tag t){
        switch(t){
            case SecondaryObject.Tag.ESPEJO:
                objectMirror = true;
                break;
            case SecondaryObject.Tag.SWITCH:
                objectSwitch = true;
                break;
            case SecondaryObject.Tag.MOVIL:
                objectMobile = true;
                break;
            case SecondaryObject.Tag.PANFLETO:
                objectPanflet = true;
                break;
        }
    } 

    public bool AreAllObjectsUsed(){ return objectMirror && objectSwitch && objectMobile && objectPanflet;}
}

public class PlayerInfo{
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 cameraRotation;
    public Vector3 cameraPosition;
    public PlayerInfo(Vector3 p, Vector3 r, Vector3 cp, Vector3 cr){
        this.position = p;
        this.rotation = r;
        this.cameraPosition = cp;
        this.cameraRotation = cr;
    }
}
