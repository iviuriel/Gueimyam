using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIce : MonoBehaviour
{
    private bool slicing;
    private Vector3 direction;

    private Transform lastFloor;
    private Transform snowStepped;

    private bool movingRight;
    private Animator rotationAnimator;
    private Animator moveAnimator;
    private HockeyController hockeyController;
    public bool finish;
    void Awake(){
        rotationAnimator = GetComponent<Animator>();
        moveAnimator = transform.GetChild(1).GetComponent<Animator>();
        hockeyController = GameObject.FindObjectOfType<HockeyController>();
    }
    void Start()
    {
        slicing = false;
        movingRight = true;
        moveAnimator.speed = 0;
        finish = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Recuperamos la camara
        transform.GetChild(0).localPosition = Vector3.zero;

        if(!finish){
            if(!slicing){
                if (Input.GetKeyDown(KeyCode.W))
                {
                    GetComponent<Rigidbody>().velocity = Vector3.forward * 10;
                    slicing = true;
                    direction = Vector3.forward;

                    //Sprites
                    /*if(state.Equals(StateStep.ICE)){
                    moveAnimator.speed = 0;
                    }else{
                        moveAnimator.speed = 1;
                    }*/
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    GetComponent<Rigidbody>().velocity = Vector3.left * 10;
                    slicing = true;
                    direction = Vector3.left;
                    if(movingRight){
                        rotationAnimator.Play("RotateToLeft");
                        movingRight = false;
                    }
                    
                    //Sprites
                    /*if(state.Equals(StateStep.ICE)){
                    moveAnimator.speed = 0;
                    }else{
                        moveAnimator.speed = 1;
                    }*/
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    GetComponent<Rigidbody>().velocity = Vector3.back * 10;
                    slicing = true;
                    direction = Vector3.back;
                    
                    //Sprites
                    /*if(state.Equals(StateStep.ICE)){
                    moveAnimator.speed = 0;
                    }else{
                        moveAnimator.speed = 1;
                    }*/
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    GetComponent<Rigidbody>().velocity = Vector3.right * 10;
                    slicing = true;
                    direction = Vector3.right;
                    if(!movingRight){
                        rotationAnimator.Play("RotateToRight");
                        movingRight = true;
                    }
                    
                    //Sprites
                    /*if(state.Equals(StateStep.ICE)){
                    moveAnimator.speed = 0;
                    }else{
                        moveAnimator.speed = 1;
                    }*/
                } 

                
            }else{
                Vector3 rayorigin = transform.position + new Vector3(0f, 1f, 0f);
                //Raycast en la dirección por si se mueve
                RaycastHit hit;
                Debug.DrawRay(transform.position, direction, Color.red);
                if (Physics.Raycast(rayorigin ,direction, out hit, 1))
                {            
                    if (hit.collider != null)
                    {
                        Debug.Log(hit.collider.gameObject.tag);
                        if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Rock"))
                        {
                            transform.position = new Vector3(lastFloor.position.x, transform.position.y, lastFloor.position.z);
                            GetComponent<Rigidbody>().velocity = Vector3.zero;
                            slicing = false;     
                            direction = Vector3.zero;            
                        } else if (hit.collider.CompareTag("Snow"))
                        {
                            lastFloor = hit.collider.transform;
                            transform.position = new Vector3(hit.collider.transform.position.x, transform.position.y, hit.collider.transform.position.z);
                            GetComponent<Rigidbody>().velocity = Vector3.zero;
                            slicing = false;  
                            direction = Vector3.zero;     
                            if(snowStepped){
                                snowStepped.GetComponent<BoxCollider>().enabled = true;
                            }
                            snowStepped = hit.collider.transform;
                            snowStepped.GetComponent<BoxCollider>().enabled = false;    
                            //moveAnimator.speed = 0;    
                        } 
                        else if (hit.collider.CompareTag("Ice"))
                        {
                            lastFloor = hit.collider.transform;
                            if(snowStepped){
                                snowStepped.GetComponent<BoxCollider>().enabled = true;
                            }
                            //moveAnimator.speed = 0;
                        }                  
                        else if (hit.collider.CompareTag("Exit"))
                        {
                            GetComponent<Rigidbody>().velocity = Vector3.zero;
                            finish = true;
                            slicing = false;
                            hockeyController.NextLevel();
                        }                  
                    }
                }
            } 
        } 
    }

    public void SetStartPlayer(Transform spawn){
        lastFloor = spawn;
        transform.position = new Vector3(spawn.position.x, transform.position.y, spawn.position.z);
        GetComponent<PlayerIce>().finish = false;

    }
}
