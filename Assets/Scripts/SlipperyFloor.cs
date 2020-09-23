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

    void Start()
    {
        state = "onSnow";
        lastMovement = "";
        breakPercentage = 0.15f;
        actionPercentage = 1.65f;
        speed = 15.0F; //Ice movement velocity
        moveUp = false;
        moveDown = false;
        moveRight = false;
        moveLeft = false;
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
            transform.Translate(0, 0, speed * Time.deltaTime);    
        else if (moveLeft)
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        else if (moveDown)
            transform.Translate(0, 0, -speed * Time.deltaTime);
        else if (moveRight)
            transform.Translate(speed * Time.deltaTime, 0, 0);

    }

    void snowMovement()
    {
        //Event e = Event.current;

        if (Input.GetKeyDown(KeyCode.W))
        {
            lastMovement = "W";
            moveUp = true;

            moveLeft = false;
            moveDown = false;
            moveRight = false;

                transform.Translate(0, 0, 0.1f);
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            lastMovement = "A";
            moveLeft = true;

            moveUp= false;
            moveDown = false;
            moveRight = false;

                transform.Translate(-0.1f, 0, 0);
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            lastMovement = "S";
            moveDown = true;

            moveUp = false;
            moveLeft = false;
            moveRight = false;

                transform.Translate(0, 0, -0.1f);
        }
       
        if (Input.GetKeyDown(KeyCode.D))
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
