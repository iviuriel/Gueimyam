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

    //PLAYER INFO
    private PlayerInfo playerInfo;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(gameObject);
            created = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        hockeyMinigame = false;
        mapMinigame = false;
        shellsMinigame = false;
        photosMinigame = false;
        photoBaby = false;

        isFirstTime = true;
    }

    public void SetPlayerInfo(Vector3 p, Vector3 r, Vector3 cp, Vector3 cr){
        playerInfo = new PlayerInfo(p, r, cp, cr);
    }

    public PlayerInfo GetPlayerInfo() {return playerInfo;}
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
