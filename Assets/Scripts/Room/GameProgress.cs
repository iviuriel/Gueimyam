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
        hockeyMinigame = true;
        mapMinigame = false;
        shellsMinigame = false;
        photosMinigame = false;
    }
}
