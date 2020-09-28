using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotoController : MinigameController
{
    private PhotoPiece[] photoPieces;

    public AudioSource completeAudioSource;
    private bool endGame;
    // Start is called before the first frame update
    void Start()
    {
        photoPieces = GameObject.FindObjectsOfType<PhotoPiece>();
        endGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!endGame){
            foreach(PhotoPiece p in photoPieces){
                bool isCorrect = p.IsPlacedCorrectly();
                if(!isCorrect){
                    return;
                }
            }

            completeAudioSource.Play();
            GetComponent<Animator>().Play("ShowCompletePhoto");
            endGame = true;
            foreach(PhotoPiece p in photoPieces){
                p.enabled = false;
            }
        }
    }
}
