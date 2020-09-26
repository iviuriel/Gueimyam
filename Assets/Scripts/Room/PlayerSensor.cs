using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    private List<GameObject> gameObjectsFound;
    private GameObject nearestGameObject;

    private LineRenderer lineRenderer;

    public Vector3 lineOrigin;

    void Awake(){
        lineRenderer = GetComponent<LineRenderer>();
    }
    void Start(){
        gameObjectsFound = new List<GameObject>();
    }

    void Update(){
        UpdateActiveItem();

        if(Input.GetKeyDown(KeyCode.E) && nearestGameObject){
            nearestGameObject.GetComponent<BasicInteraction>().Interact(transform.parent.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        BasicInteraction bi = other.gameObject.GetComponent<BasicInteraction>();
        if(bi && bi.enable){
            AddObject(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
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
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);
        }
    }
    
    void UpdateActiveItem(){
        Vector3 currentPosition = transform.position;
        float closestDistanceSqr = Mathf.Infinity;

        //Deletes the current outline
        if(nearestGameObject){
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);
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
            lineRenderer.SetPosition(0, transform.position + lineOrigin);           
            lineRenderer.SetPosition(1, nearestGameObject.transform.position);           
        }
    }
}
