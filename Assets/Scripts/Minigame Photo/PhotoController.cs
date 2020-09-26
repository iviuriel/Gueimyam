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

        GetComponent<Animator>().Play("ShowCompletePhoto");
    }
}
