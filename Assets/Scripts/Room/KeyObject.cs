using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyObject : BasicInteraction
{
    public string sceneName;

    public override void Interact(GameObject sender){
        if(sceneName != null && !sceneName.Equals("")){
            PlayerMovement pMovement = sender.GetComponent<PlayerMovement>();
            if(pMovement){
                pMovement.SetPlayerInfo();
            }
            SceneManager.LoadScene(sceneName);
        }
    }
}
