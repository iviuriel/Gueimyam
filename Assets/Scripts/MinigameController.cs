using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameController : MonoBehaviour
{
    public virtual void CompleteMinigame(string minigame){
        switch(minigame){
            case "shell":
                GameObject.FindObjectOfType<GameProgress>().shellsMinigame = true;
                break;
            case "photo":
                GameObject.FindObjectOfType<GameProgress>().photosMinigame = true;
                break;
            case "hockey":
                GameObject.FindObjectOfType<GameProgress>().hockeyMinigame = true;
                break;
            case "map":
                GameObject.FindObjectOfType<GameProgress>().mapMinigame = true;
                break;
        }

        //Temporal
        SceneManager.LoadScene("Test-Ivan");
        return;
    }
}
