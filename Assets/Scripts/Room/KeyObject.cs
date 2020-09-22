using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyObject : BasicInteraction
{
    public string sceneName;

    public override void Interact(){
        if(sceneName != null && !sceneName.Equals("")){
            SceneManager.LoadScene(sceneName);
        }
    }
}
