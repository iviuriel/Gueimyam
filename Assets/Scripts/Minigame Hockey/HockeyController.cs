using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HockeyController : MinigameController
{
    private Transform player;
    public Transform[] levelSpawns;
    private int currentLevel;
    public Animator canvasAnimator;

    void Awake(){
        player = GameObject.FindObjectOfType<PlayerIce>().transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 0;
        PlacePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel(){
        currentLevel++;
        if(currentLevel == levelSpawns.Length){
            canvasAnimator.Play("PhotoHockeyShow");
        }else{
            canvasAnimator.Play("BlackScreen");
        }
    }

    public void PlacePlayer(){
        player.position = new Vector3(levelSpawns[currentLevel].position.x, player.position.y, levelSpawns[currentLevel].position.z);
        player.GetComponent<PlayerIce>().finish = false;
    }
}
