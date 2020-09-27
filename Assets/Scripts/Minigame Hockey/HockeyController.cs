using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HockeyController : MinigameController
{
    private Transform player;
    public Transform[] levelSpawns;
    private int currentLevel;
    public Animator canvasAnimator;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip levelComplete;
    public AudioClip gameComplete;

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
            PlayAudio(gameComplete);
        }else{
            canvasAnimator.Play("BlackScreen");
            PlayAudio(levelComplete);
        }
    }

    public void PlacePlayer(){
        player.GetComponent<PlayerIce>().SetStartPlayer(levelSpawns[currentLevel]);
        // player.position = new Vector3(levelSpawns[currentLevel].position.x, player.position.y, levelSpawns[currentLevel].position.z);
        // player.GetComponent<PlayerIce>().finish = false;
    }

    void PlayAudio(AudioClip clip){
        audioSource.clip = clip;
        audioSource.Play();
    }
}
