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
    //private Animator rotationAnimator;
    //private Animator moveAnimator;
    private HockeyController hockeyController;
    public bool finish;

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip snowFloor;
    public AudioClip iceRoll;
    public AudioClip hitSound;
    void Awake(){
        hockeyController = GameObject.FindObjectOfType<HockeyController>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        slicing = false;
        movingRight = true;
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
                    PlayAudio(iceRoll);
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    GetComponent<Rigidbody>().velocity = Vector3.left * 10;
                    slicing = true;
                    direction = Vector3.left;
                    if(movingRight){
                        movingRight = false;
                    }
                    PlayAudio(iceRoll);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    GetComponent<Rigidbody>().velocity = Vector3.back * 10;
                    slicing = true;
                    direction = Vector3.back;
                    PlayAudio(iceRoll);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    GetComponent<Rigidbody>().velocity = Vector3.right * 10;
                    slicing = true;
                    direction = Vector3.right;
                    if(!movingRight){
                        movingRight = true;
                    }
                    PlayAudio(iceRoll);
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
                            if(lastFloor.CompareTag("Snow")){
                                PlayAudio(snowFloor);   
                            }else{
                                PlayAudio(hitSound);   
                            }
                                     
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
                            PlayAudio(snowFloor);    
                        } 
                        else if (hit.collider.CompareTag("Ice"))
                        {
                            lastFloor = hit.collider.transform;
                            if(snowStepped){
                                snowStepped.GetComponent<BoxCollider>().enabled = true;
                            }
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

    void PlayAudio(AudioClip clip){
        audioSource.clip = clip;
        audioSource.Play();
    }
}
