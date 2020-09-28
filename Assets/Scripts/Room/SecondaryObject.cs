﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondaryObject : BasicInteraction
{
    public string displayText; 
    public Sprite objectToShow;

    public Text uiText;
    public Animator textAnimator; 
    public Animator objectAnimator;

    private Coroutine textCoroutine;
    public bool typing;

    private bool activated;
    private PlayerMovement pMovementRef;

    public Tag tagName;

    public enum Tag{
        SWITCH,
        MOVIL,
        PANFLETO,
        ESPEJO
    }
    public override void Interact(GameObject sender){
        if(!activated){
            DisplayObject();
            pMovementRef = sender.GetComponent<PlayerMovement>();
            if(pMovementRef){
                pMovementRef.enabled = false;
            }
            GetComponent<Outline>().enabled = false;
        }
    }

    void Start(){
        activated = false;
        typing = false;
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && activated){
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
                if(objectToShow){
                    objectAnimator.Play("HideObject");
                }
                if(pMovementRef){
                    pMovementRef.enabled = true;
                }
                pMovementRef = null;
                GetComponent<Outline>().enabled = true;
                activated = false;
                StartCoroutine("UsedDelay");
            }
        }
    }

    void DisplayObject(){
        if(displayText.Length > 0){
            textAnimator.Play("ShowText");
            textCoroutine = StartCoroutine("AddText");
        }
        if(objectToShow){
            objectAnimator.transform.GetChild(1).GetComponent<Image>().sprite = objectToShow;
            objectAnimator.Play("ShowObject");
        }
        activated = true;
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
            yield return new WaitForSeconds(0.04f);
        }
    }

    IEnumerator UsedDelay(){
        yield return new WaitForSeconds(0.2f);
        FindObjectOfType<GameProgress>().SetUsedObject(tagName);
        //usedOnce = true;
    }

    public void SetUIText(Text t){uiText = t;}
    public void SetAnimatorText(Animator t){textAnimator = t;}
    public void SetAnimatorObject(Animator t){objectAnimator = t;}
}
