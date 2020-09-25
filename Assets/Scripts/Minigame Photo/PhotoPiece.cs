﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoPiece : MonoBehaviour
{
    public int id;
    public bool isClicked { get; private set; }
    private bool mouseOver;
    private Transform sheet;
    private Animator animator;

    void Awake(){
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        int rotMultiplier = UnityEngine.Random.Range(0, 4);
        int initialRotation = 90*rotMultiplier;
        Debug.Log(initialRotation);
        transform.rotation = Quaternion.Euler(0, 0, initialRotation);

        animator.Play("Start"+ initialRotation);

        isClicked = false;
        mouseOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isClicked)
        {
            if (Input.GetMouseButton(0))
            {
                //calculate the position based on percentage of mouse/screen
                Vector3 mousePosition = Input.mousePosition;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                transform.localPosition = new Vector3(worldPosition.x, worldPosition.y, 0f);
            }
            if (Input.GetMouseButtonUp(0))
            {              
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity, 1 << 8);
 
                if(hit.collider != null)
                {
                    Transform t = hit.collider.transform;
                    transform.position = t.position;
                    sheet = t;
                }else{
                    sheet = null;
                }
                isClicked = false;
            }
        }

        if (Input.GetMouseButtonDown(1) && mouseOver)
        {
            animator.SetTrigger("Rotate");
        }
    }

    private void OnMouseDrag()
    {
        isClicked = true;        
    }

    private void OnMouseEnter(){
        this.GetComponent<SpriteRenderer>().material.SetFloat("_OutlineEnabled", 1);
        mouseOver = true;
    }

    private void OnMouseExit(){
        this.GetComponent<SpriteRenderer>().material.SetFloat("_OutlineEnabled", 0);
        mouseOver = false;
    }

    public bool IsPlacedCorrectly(){
        if(!sheet){
            return false;
        }

        int sheetId = sheet.GetComponent<PhotoSheet>().id;
        if(this.id != sheetId || transform.rotation.eulerAngles.z != 0){
            return false;
        }

        return true;
    }
}
