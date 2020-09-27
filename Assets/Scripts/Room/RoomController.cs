using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomController : MonoBehaviour
{
    public GameObject[] keyObjects;

    public Transform corchoCerca;

    public Transform corchoObject;
    public Animator titleObject;
    public Transform doorObject;
    public BoxCollider doorBlock;

    public Animator endObject;

    private GameProgress gp;

    void Awake(){
        gp = GameObject.FindObjectOfType<GameProgress>();
        if(gp){
            ReadGameProgress();
            UpdateCorcho();
        }
    }

    void Start(){
        if(gp.isFirstTime){
            SetEnabledScripts(false);
        }else{
            titleObject.gameObject.SetActive(false);
        }
    }

    void Update(){
        if(Input.GetMouseButtonDown(0) && gp.isFirstTime){
            titleObject.SetTrigger("StartGame");
            gp.isFirstTime = false;
            StartCoroutine(ActivateScripts());
        }
    }

    void ReadGameProgress(){
        int contador = 0;
        if(gp.hockeyMinigame){
            keyObjects[0].GetComponent<KeyObject>().enable = false;
            contador++;
        }
        if(gp.shellsMinigame){
            keyObjects[1].GetComponent<KeyObject>().enable = false;
            contador++;
        }
        if(gp.mapMinigame){
            keyObjects[2].GetComponent<KeyObject>().enable = false;
            contador++;
        }
        if(gp.photosMinigame){
            keyObjects[3].GetComponent<KeyObject>().enable = false;
            contador++;
        }

        float angles = (100 / 4) * contador;
        doorObject.localRotation = Quaternion.Euler(-90f, 0f, -angles);

        if(contador == 4){
            doorBlock.isTrigger = true;
        }
    }

    void UpdateCorcho(){
        if(gp.hockeyMinigame){
            corchoCerca.GetChild(0).GetComponent<Image>().color = Color.white;
            corchoObject.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        }
        if(gp.shellsMinigame){
            corchoCerca.GetChild(1).GetComponent<Image>().color = Color.white;
            corchoObject.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        }
        if(gp.mapMinigame){
            corchoCerca.GetChild(2).GetComponent<Image>().color = Color.white;
            corchoObject.GetChild(2).GetComponent<SpriteRenderer>().color = Color.white;
        }
        if(gp.photosMinigame){
            corchoCerca.GetChild(3).GetComponent<Image>().color = Color.white;
            corchoObject.GetChild(3).GetComponent<SpriteRenderer>().color = Color.white;
        }
        if(gp.photoBaby){
            corchoCerca.GetChild(4).GetComponent<Image>().color = Color.white;
            corchoObject.GetChild(4).GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    IEnumerator ActivateScripts(){
        yield return new  WaitForSeconds(0.5f);

        SetEnabledScripts(true);

        //pMovement.transform.GetChild(2).GetComponent<Animator>().speed = 1;
        
    }

    void SetEnabledScripts(bool s){
        PlayerMovement pMovement = GameObject.FindObjectOfType<PlayerMovement>();
        PlayerSensor pSensor = GameObject.FindObjectOfType<PlayerSensor>();

        pMovement.enabled = s;
        pMovement.enabled = s;

        pMovement.transform.GetChild(2).GetComponent<Animator>().speed = 0;
    }

    public void EndGame(){
        SetEnabledScripts(false);
        endObject.Play("EndGame");

        #if UNITY_STANDALONE_WIN
        StartCoroutine(Quit());
        #endif        
    }

    IEnumerator Quit(){
        yield return new WaitForSeconds(5f);
    }
}
