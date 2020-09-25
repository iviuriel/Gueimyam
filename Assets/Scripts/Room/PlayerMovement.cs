using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //--------------ROTATION-----------------
    //---------------------------------------
    private Vector3 localRotation;

    [Header("Rotation values")]
    [Tooltip("Increases the sensibility of the mouse in X times.")]
    [Range(0, 10)]
    public float mouseSensitivity = 4f;
    [Tooltip("Increases the sensibility of the scrollwheel in X times.")]
    [Range(0, 10)]
    public float scrollSensitivity = 2f;
    [Tooltip("Rotation speed of the camera")]
    [Range(0, 20)]
    public float orbitSpeed = 10f;
    [Tooltip("Movement speed of the camera to zoom")]
    [Range(0, 10)]
    public float scrollSpeed = 6f;

    [Tooltip("The times the rotation will be divided on to set the camera away")]
    public int distanceDeltaMultiplier = 200;
    
    [Tooltip("The distance between the player position and the pivot origin in the Z-Axis")]
    [Range(0, 0.05f)]
    public float distanceOriginPivot = 0.005f;

    [Header("Max Distance values")]
    [Tooltip("Minimum distance of the pivot to the player.")]
    [Range(0f, 0.1f)]
    public float minCameraDistance = 0.001f;

    [Tooltip("Maximum distance of the pivot to the player.")]
    [Range(0f, 0.1f)]
    public float maxCameraDistance = 0.03f;

    private float percentageRotation;

    [Header("Start Rotation")]
    [Tooltip("Start rotation of the pivot on X-axis")]
    public float startRotationVertical = 20;
    [Tooltip("Start rotation of the player on Y-axis")]
    public float startRotationHorizontal;

    [Header("Max Rotation values")]
    [Tooltip("Minimum rotation of the pivot on X-axis in degrees.")]
    [Range(0, 180)]
    public float minVerticalRotation = 10f;

     [Tooltip("Maximum rotation of the pivot on X-axis in degrees.")]
    [Range(0, 180)]
    public float maxVerticalRotation = 50f;
    public bool ableToRotate { get; set; }

    private Transform cameraPivot;

    //--------------MOVEMENT-----------------
    //---------------------------------------
    //Movement values
    private float movVertValue;
    private float movHorValue;
    private bool movingRight; //Si se mueve hacia la derecha
    private bool wasMovingRight; //Si se estaba moviendo hacia la derecha

    [Header("Movement")]
    [Tooltip("Speed of the main character")]
    public float speed = 15f;

    //---------------GLOBAL------------------
    //---------------------------------------
    private Rigidbody rigidBody;

    private GameProgress gameProgress;

    //------------ANIMATION------------------
    //---------------------------------------

    private Animator rotationAnimator;
    private Animator moveAnimator;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        cameraPivot = transform.GetChild(0);
        gameProgress = GameObject.FindObjectOfType<GameProgress>();

        rotationAnimator = GetComponent<Animator>();
        moveAnimator = transform.GetChild(2).GetComponent<Animator>();

        if(gameProgress){
            PlayerInfo pi = gameProgress.GetPlayerInfo();
            if(pi != null){
                transform.position = pi.position;

                transform.rotation = Quaternion.Euler(0, pi.rotation.y, 0);
                cameraPivot.localRotation = Quaternion.Euler(pi.cameraRotation.x, 0, 0);
                
                startRotationVertical = pi.cameraRotation.x;
                startRotationHorizontal = pi.rotation.y;

            }

        }
    }
    void Start()
    {      
        localRotation.y = startRotationVertical;
        localRotation.x = startRotationHorizontal;
        ableToRotate = true;     
        percentageRotation = 0;   

        movingRight = true;
        wasMovingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        movVertValue = Input.GetAxis("Vertical");
        movHorValue = Input.GetAxis("Horizontal");
    }

    void LateUpdate(){
        Rotate();
    }

    void FixedUpdate(){
        Move();
    }

    void Rotate(){
        if (Input.GetMouseButton(1) && ableToRotate)
        {
            localRotation.x += Input.GetAxis("Mouse X") * mouseSensitivity;
            localRotation.y += Input.GetAxis("Mouse Y") * mouseSensitivity * -1f ;

            localRotation.y = Mathf.Clamp(localRotation.y, minVerticalRotation, maxVerticalRotation);
        }

        Quaternion QT = Quaternion.Euler(0, localRotation.x, 0);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, QT, Time.deltaTime * orbitSpeed);

        Quaternion QTPivot = Quaternion.Euler(localRotation.y, 0, 0);
        cameraPivot.localRotation = Quaternion.Lerp(cameraPivot.localRotation, QTPivot, Time.deltaTime * orbitSpeed);
        percentageRotation = (cameraPivot.localRotation.eulerAngles.x - minVerticalRotation)/(maxVerticalRotation - minVerticalRotation);
        float zPos = ((maxCameraDistance - minCameraDistance) * percentageRotation) + minCameraDistance;
        cameraPivot.localPosition = new Vector3(0f, cameraPivot.localPosition.y, zPos);
    }

    void Move(){
        Vector3 moveOnLocal = transform.TransformDirection(new Vector3(movHorValue, 0f, movVertValue));
        Vector3 movement = moveOnLocal * speed * Time.deltaTime;
        rigidBody.MovePosition(rigidBody.position + movement);

        if(movHorValue == 0f && movVertValue == 0f){
            moveAnimator.speed = 0;
        }else{
            moveAnimator.speed = 1;
            //Si se mueve a la derecha
            if(movHorValue > 0)
            {   
                //Si no se movía hacia la derecha lo giramos
                if(!movingRight){
                    rotationAnimator.Play("RotateToRight");
                    movingRight = true;
                }
            }else if(movHorValue < 0){
                //Si se movia hacia la derecha lo giramos
                if(movingRight){
                    rotationAnimator.Play("RotateToLeft");
                    movingRight = false;
                }
            }
        }
    }

    public void SetPlayerInfo(){
        if(gameProgress){
            gameProgress.SetPlayerInfo(transform.position, transform.rotation.eulerAngles, cameraPivot.localPosition, cameraPivot.localRotation.eulerAngles);
        }
    }
}
