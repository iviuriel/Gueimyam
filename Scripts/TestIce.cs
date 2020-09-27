using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIce : MonoBehaviour
{
    private bool slicing;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        slicing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!slicing){
            if (Input.GetKeyDown(KeyCode.W))
            {
                GetComponent<Rigidbody>().velocity = Vector3.forward * 10;
                slicing = true;
                direction = Vector3.forward;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                GetComponent<Rigidbody>().velocity = Vector3.left * 10;
                slicing = true;
                direction = Vector3.left;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                GetComponent<Rigidbody>().velocity = Vector3.back * 10;
                slicing = true;
                direction = Vector3.back;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                GetComponent<Rigidbody>().velocity = Vector3.right * 10;
                slicing = true;
                direction = Vector3.right;
            } 
        }else{
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
                        slicing = false;  
                        direction = Vector3.zero;             
                    }                 
                }
            }

            //Raycast en la dirección por si se mueve
            RaycastHit hit;
            Debug.DrawRay(transform.position, direction, Color.red);
            if (Physics.Raycast(rayorigin ,direction, out hit, 3))
            {            
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Rock"))
                    {
                        Debug.Log("hajsdfhjaskldfhjkasdf");
                        transform.position = new Vector3(hitDown.collider.transform.position.x, transform.position.y, hitDown.collider.transform.position.z);
                        GetComponent<Rigidbody>().velocity = Vector3.zero;
                        slicing = false;     
                        direction = Vector3.zero;            
                    }                 
                }
            }
        }  
    }

    /*void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Wall")){
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            slicing = false;
        }
    }*/
}
