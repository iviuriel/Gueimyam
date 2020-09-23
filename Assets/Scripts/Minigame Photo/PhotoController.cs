using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotoController : MinigameController
{
    private PhotoPiece[] photoPieces;
    // Start is called before the first frame update
    void Start()
    {
        photoPieces = GameObject.FindObjectsOfType<PhotoPiece>();
        Debug.Log("Pieces: "+ photoPieces.Length);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(PhotoPiece p in photoPieces){
            bool isCorrect = p.IsPlacedCorrectly();
            if(!isCorrect){
                return;
            }
        }

        CompleteMinigame();
    }
}
