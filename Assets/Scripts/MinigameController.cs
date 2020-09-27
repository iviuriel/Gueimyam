using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameController : MonoBehaviour
{
    public string nameMinigame;

    public EndMinigameObject endObject;
    public virtual void CompleteMinigame(){
        switch(nameMinigame){
            case "shell":
                GameObject.FindObjectOfType<GameProgress>().shellsMinigame = true;
                break;
            case "photo":
                GameObject.FindObjectOfType<GameProgress>().photosMinigame = true;
                break;
            case "hockey":
                GameObject.FindObjectOfType<GameProgress>().hockeyMinigame = true;
                break;
            case "stars":
                GameObject.FindObjectOfType<GameProgress>().mapMinigame = true;
                break;
        }

        //Temporal
        SceneManager.LoadScene("Room");
        return;
    }

    public virtual void ShowFinalText(){
        endObject.Interact(this.gameObject);
    }
}
