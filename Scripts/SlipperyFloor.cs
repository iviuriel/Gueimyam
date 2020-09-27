using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperyFloor : MonoBehaviour
{
    //public float rotationSpeed = 35.0F; //Velocidad de rotación
    //public bool hasCollided = false;

    private float breakPercentage;
    private float actionPercentage;
    public float speed;
    public bool moveUp, moveDown, moveRight, moveLeft;
    public string state;
    public string lastMovement;

    private Vector3 direction;

    void Start()
    {
        state = "onSnow";
        lastMovement = "";
        breakPercentage = 0.15f;    //0.15f
        actionPercentage = 1.64f;   //1.64f
        speed = 15.0F; //Ice movement velocity
        moveUp = false;
        moveDown = false;
        moveRight = false;
        moveLeft = false;
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (state.Equals("onIce"))
            iceMovement();
        else if (state.Equals("onSnow"))
            snowMovement();
        else if (state.Equals("stoppedOnIce"))
        {
            moveUp = false;
            moveLeft = false;
            moveDown = false;
            moveRight = false;

            if (Input.GetKeyDown(KeyCode.W))
            {
                lastMovement = "W";
                state = "onIce";
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                lastMovement = "A";
                state = "onIce";
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                lastMovement = "S";
                state = "onIce";
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                lastMovement = "D";
                state = "onIce";
            }       
        }

        //transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime, 0);
    }

    void iceMovement()
    {
        if (lastMovement.Equals("W"))
            moveUp = true;
        else if (lastMovement.Equals("A"))
            moveLeft = true;
        else if (lastMovement.Equals("S"))
            moveDown = true;
        else if (lastMovement.Equals("D"))
            moveRight = true;

        if (moveUp)
        {
            /*
            GetComponent<Rigidbody>().velocity = Vector3.forward * 10;
            state = "onIce";
            direction = Vector3.forward; */

            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else if (moveLeft)
        {
            /*
            GetComponent<Rigidbody>().velocity = Vector3.left * 10;
            state = "onIce";
            direction = Vector3.left; */

            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else if (moveDown)
        {
            /*
            GetComponent<Rigidbody>().velocity = Vector3.back * 10;
            state = "onIce";
            direction = Vector3.back; */

            transform.Translate(0, 0, -speed * Time.deltaTime);
        }
        else if (moveRight)
        {
            /*
            GetComponent<Rigidbody>().velocity = Vector3.right * 10;
            state = "onIce";
            direction = Vector3.right; */

            transform.Translate(speed * Time.deltaTime, 0, 0);
        }


    }

    void snowMovement()
    {
        //Event e = Event.current;

        if (Input.GetKey(KeyCode.W))
        {
            lastMovement = "W";
            moveUp = true;

            moveLeft = false;
            moveDown = false;
            moveRight = false;

                transform.Translate(0, 0, 0.1f);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            lastMovement = "A";
            moveLeft = true;

            moveUp= false;
            moveDown = false;
            moveRight = false;

                transform.Translate(-0.1f, 0, 0);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            lastMovement = "S";
            moveDown = true;

            moveUp = false;
            moveLeft = false;
            moveRight = false;

            transform.Translate(0, 0, -0.1f);
        }
       
        if (Input.GetKey(KeyCode.D))
        {
            lastMovement = "D";
            moveRight = true;

            moveUp = false;
            moveLeft = false;
            moveDown = false;

            transform.Translate(0.1f, 0, 0);
        }
    }

    void OnCollisionEnter (Collision collision)
    {
        /*
        Vector3 rayorigin = transform.position - new Vector3(0f, 0.5f, 0f);
        //Raycast al suelo para comprobar que está pisando
        RaycastHit hitDown;
        Debug.DrawRay(transform.position, Vector3.down, Color.green);
        if (Physics.Raycast(transform.position, Vector3.down, out hitDown, 3))
        {
            if (hitDown.collider != null)
            {
                if (hitDown.collider.CompareTag("Snow"))
                {
                    transform.position = new Vector3(hitDown.collider.transform.position.x, transform.position.y, hitDown.collider.transform.position.z);
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    state = "onSnow";
                    direction = Vector3.zero;
                }
            }
        }

        //Raycast en la dirección por si se mueve
        RaycastHit hit;
        Debug.DrawRay(transform.position, direction, Color.red);
        if (Physics.Raycast(rayorigin, direction, out hit, 3))
        {
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Rock"))
                {
                    Debug.Log("hajsdfhjaskldfhjkasdf");
                    transform.position = new Vector3(hitDown.collider.transform.position.x, transform.position.y, hitDown.collider.transform.position.z);
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    state = "onIce";    //borrar
                    direction = Vector3.zero;
                }
            }
        } */

        if (collision.gameObject.tag == "Snow")
        {
            state = "onSnow";

            switch (lastMovement)
            {
                case "W":
                    transform.Translate(0, 0, actionPercentage);
                    break;
                case "A":
                    transform.Translate(-actionPercentage, 0, 0);
                    break;
                case "S":
                    transform.Translate(0, 0, -actionPercentage);
                    break;
                case "D":
                    transform.Translate(actionPercentage, 0, 0);
                    break;
            }
        } else if (collision.gameObject.tag == "Ice")
        {
            state = "onIce";
        }
        else if (collision.gameObject.tag == "Rock" || collision.gameObject.tag == "Wall")
        { 
            if (state.Equals("onIce"))
            {
                state = "stoppedOnIce";

                switch (lastMovement)
                {
                    case "W":
                        transform.Translate(0, 0, -breakPercentage);
                        break;
                    case "A":
                        transform.Translate(breakPercentage, 0, 0);
                        break;
                    case "S":
                        transform.Translate(0, 0, breakPercentage);
                        break;
                    case "D":
                        transform.Translate(-breakPercentage, 0, 0);
                        break;
                }

                lastMovement = "";
            } else if (state.Equals("onSnow"))
            {
                switch (lastMovement)
                {
                    case "W":
                        transform.Translate(0, 0, -breakPercentage);
                        break;
                    case "A":
                        transform.Translate(breakPercentage, 0, 0);
                        break;
                    case "S":
                        transform.Translate(0, 0, breakPercentage);
                        break;
                    case "D":
                        transform.Translate(-breakPercentage, 0, 0);
                        break;
                }
            }
        }  
    }
    /*
     * void OnCollisionExit (Collision collision)
    {
        if (collision.gameObject.tag == "Snow")
        {
            
        }  else if (collision.gameObject.tag == "Ice")
        {

        } else
        {
            //Debug.Log("Exit");
            //hasCollided = false;
        }
    }

    void OnCollisionStay (Collision collision)
    {
        if (collision.gameObject.tag != "Plane")
        {
            Debug.Log("Stay");
            transform.Translate(0, 0, 0);
        }
    } 
    
     if (Input.GetKeyDown(KeyCode.W) )
            if (!hasCollided)
                transform.Translate(0, 0, speed * Time.deltaTime);
            else
                transform.Translate(0, 0, 0);
        if (Input.GetKeyDown(KeyCode.S))
            if (!hasCollided)
                transform.Translate(0, 0, - speed * Time.deltaTime);
            else
                transform.Translate(0, 0, 0);
        if (Input.GetKeyDown(KeyCode.A))
            if (!hasCollided)
                transform.Translate(- speed * Time.deltaTime, 0, 0);
            else
                transform.Translate(0, 0, 0);
        if (Input.GetKeyDown(KeyCode.D))
            if (!hasCollided)
                transform.Translate(speed * Time.deltaTime, 0, 0);
            else
                transform.Translate(0, 0, 0);
        
     */
}
