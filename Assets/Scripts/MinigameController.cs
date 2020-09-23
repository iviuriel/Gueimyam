using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameController : MonoBehaviour
{
    protected virtual void CompleteMinigame(){
        GameObject.FindObjectOfType<GameProgress>().shellsMinigame = true;
        //Temporal
        SceneManager.LoadScene("Test-Ivan");
        return;
    }
}
