using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    private List<GameObject> gameObjectsFound;
    private GameObject nearestGameObject;

    void Start(){
        gameObjectsFound = new List<GameObject>();
    }

    void Update(){
        UpdateActiveItem();
    }
    private void OnTriggerEnter(Collider other)
    {
        BasicInteraction bi = other.gameObject.GetComponent<BasicInteraction>();
        if(bi){
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
            Material mat = g.GetComponent<MeshRenderer>().material;
            mat.SetFloat("_enable", 0);
        }
    }
    
    void UpdateActiveItem(){
        Vector3 currentPosition = transform.position;
        float closestDistanceSqr = Mathf.Infinity;

        //Deletes the current outline
        if(nearestGameObject){
            Material mat = nearestGameObject.GetComponent<MeshRenderer>().material;
            mat.SetFloat("_enable", 0);
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
            Material mat = nearestGameObject.GetComponent<MeshRenderer>().material;
            mat.SetFloat("_enable", 1);           
        }
    }
}
