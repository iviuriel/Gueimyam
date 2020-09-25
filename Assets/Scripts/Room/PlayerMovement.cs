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

    [Header("Start Rotation")]
    [Tooltip("Start rotation of the pivot on X-axis")]
    public float startRotationVertical;
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

    [Header("Movement")]
    [Tooltip("Speed of the main character")]
    public float speed = 15f;

    //---------------GLOBAL------------------
    //---------------------------------------
    private Rigidbody rigidBody;

    private GameProgress gameProgress;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        cameraPivot = transform.GetChild(0);
        gameProgress = GameObject.FindObjectOfType<GameProgress>();

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
        cameraPivot.localPosition = new Vector3(0f, 0f, cameraPivot.localEulerAngles.x/-2);
    }

    void Move(){
        Vector3 moveOnLocal = transform.TransformDirection(new Vector3(movHorValue, 0f, movVertValue));
        Vector3 movement = moveOnLocal * speed * Time.deltaTime;
        rigidBody.MovePosition(rigidBody.position + movement);
    }

    public void SetPlayerInfo(){
        if(gameProgress){
            gameProgress.SetPlayerInfo(transform.position, transform.rotation.eulerAngles, cameraPivot.localPosition, cameraPivot.localRotation.eulerAngles);
        }
    }
}
