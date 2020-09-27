using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMinigameObject : BasicInteraction
{
    public string displayText; 

    public Text uiText;
    public Animator textAnimator; 

    private Coroutine textCoroutine;
    private bool typing;

    private bool activated;

    public override void Interact(GameObject sender){
        if(!activated){
            DisplayObject();
            activated = true;
        }
    }

    void Start(){
        activated = false;
        typing = false;
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            if(typing){
                StopCoroutine(textCoroutine);
                textCoroutine = null;
                uiText.text = displayText;
                typing = false;
            }else{
                GameObject.FindObjectOfType<MinigameController>().CompleteMinigame();
                activated = false;
            }
        }
    }

    void DisplayObject(){
        if(displayText.Length > 0){
            textAnimator.Play("ShowText");
            textCoroutine = StartCoroutine(AddText());
        }
    }

    IEnumerator AddText(){
        //yield return new WaitForSeconds(0.5f);
        int contador = 0;
        int numCharacters = displayText.Length;
        string text = "";
        typing = true;

        while(typing){
            text += displayText[contador];
            uiText.text = text;
            contador++;
            if(contador == numCharacters){
                typing = false;
            }
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.02f);
        }
    }
}
