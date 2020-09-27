using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorchoObject : BasicInteraction
{
    public string displayText; 

    public Text uiText;
    public Animator textAnimator; 
    public Animator objectAnimator;

    private Coroutine textCoroutine;
    private bool typing;

    private bool activated;
    private PlayerMovement pMovementRef;
    public override void Interact(GameObject sender){
        if(!activated){
            DisplayObject();
            pMovementRef = sender.GetComponent<PlayerMovement>();
            if(pMovementRef){
                pMovementRef.enabled = false;
            }
            GetComponent<Outline>().enabled = false;
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
                uiText.text = "";
                if(displayText.Length > 0){
                    textAnimator.Play("HideText");
                }
                objectAnimator.Play("HideObject");
                if(pMovementRef){
                    pMovementRef.enabled = true;
                }
                pMovementRef = null;
                GetComponent<Outline>().enabled = true;
                activated = false;
            }
        }
    }

    void DisplayObject(){
        if(displayText.Length > 0){
            textAnimator.Play("ShowText");
            textCoroutine = StartCoroutine(AddText());
        }
        objectAnimator.Play("ShowObject");
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
            yield return new WaitForSeconds(0.01f);
        }
    }
}
