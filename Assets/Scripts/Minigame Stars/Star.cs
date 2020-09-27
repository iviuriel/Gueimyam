using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public int id;
    Constellations_Minigame c;
    Star thisStar;

    void Start()
    {
        //thisStar = gameObject.GetComponent<Star>();
        c = Constellations_Minigame.controller;
    }

    public int getId()
    {
        return this.id;
    }

    public void checkStar(Star s)
    {
        c.checkClickedStar(s.getId());
    }

    public void setGreen()
    {

    }
}
