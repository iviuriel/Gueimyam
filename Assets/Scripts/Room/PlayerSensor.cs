using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    private List<GameObject> gameObjectsFound;
    private GameObject nearestGameObject;

    private LineRenderer lineRenderer;

    public Vector3 lineOrigin;

    private Animator cartelAnimator;

    private bool isDetecting;

    void Awake(){
        lineRenderer = GetComponent<LineRenderer>();
        cartelAnimator = transform.parent.GetChild(2).GetChild(2).GetComponent<Animator>();
    }
    void Start(){
        gameObjectsFound = new List<GameObject>();
        isDetecting = false;
        
    }

    void Update(){
        if(isDetecting){
            UpdateActiveItem();

            if(Input.GetKeyDown(KeyCode.E) && nearestGameObject){
                nearestGameObject.GetComponent<BasicInteraction>().Interact(transform.parent.gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!isDetecting){
            return;
        }
        BasicInteraction bi = other.gameObject.GetComponent<BasicInteraction>();
        if(bi && bi.enable){
            AddObject(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(!isDetecting){
            return;
        }
        BasicInteraction bi = other.gameObject.GetComponent<BasicInteraction>();
        if(bi){
            RemoveObject(other.gameObject);
        }
    }

    void AddObject(GameObject g)
    {
        gameObjectsFound.Add(g);
    }

    void RemoveObject(GameObject g)
    {
        gameObjectsFound.Remove(g);
        if(g == nearestGameObject){
            nearestGameObject.GetComponent<Outline>().OutlineMode = Outline.Mode.NoOutline;
            nearestGameObject = null;
        }
    }
    
    void UpdateActiveItem(){
        Vector3 currentPosition = transform.position;
        float closestDistanceSqr = Mathf.Infinity;

        //Deletes the current outline
        if(nearestGameObject){
            nearestGameObject.GetComponent<Outline>().OutlineMode = Outline.Mode.NoOutline;
        } 
        if(gameObjectsFound.Count > 0){
            foreach(GameObject go in gameObjectsFound)
            {
                Vector3 directionToTarget = go.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if(dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    nearestGameObject = go;
                }
            } 

            //Add the outline to the current object
            nearestGameObject.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineVisible; 

            cartelAnimator.SetBool("IsShown", true);          
        }else{
            cartelAnimator.SetBool("IsShown", false);  
        }
    }

    public void StartDetecting(){
        cartelAnimator.SetTrigger("Start");
        isDetecting = true;
    }
}
