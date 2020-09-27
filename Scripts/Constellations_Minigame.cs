using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constellations_Minigame : MonoBehaviour
{
    public static Constellations_Minigame controller;
    public Camera cam;
    private int level;
    private int currentStar;
    public GameObject line1, line2_1, line2_2, line3_1, line3_2, line3_3;
    private bool level1Completed, level2Completed, level3Completed;

    private int[] constellation_1; //constellation_2 = null, constellation_3 = null;


    // Start is called before the first frame update
    void Start()
    {
        controller = this;
        line1.SetActive(false);
        line2_1.SetActive(false);
        line2_2.SetActive(false);
        line3_1.SetActive(false);
        line3_2.SetActive(false);
        line3_3.SetActive(false);

        level = 1;
        currentStar = 0;
        level1Completed = false;
        level2Completed = false;
        level3Completed = false;

        constellation_1 = new int [7] { 0, 1, 2, 3, 4, 5, 6 };
        //constellation_2 = new int[7] { };
        //constellation_3 = new int[7] { };

    }

    // Update is called once per frame
    void Update()
    {
        if (level1Completed)
        {
            level1Completed = false;
            enableLines(1);
        } else if (level2Completed)
        {
            level2Completed = false;
            enableLines(2);
        } else if (level3Completed)
        {
            level3Completed = false;
            enableLines(3);
        }
    }

    private void enableLines(int id)
    {
        switch (id)
        {
            case 1:
                line1.SetActive(true); 
                break;
            case 2:
                line2_2.SetActive(true);
                break;
            case 3:
                line3_3.SetActive(true);
                break;
        }
    }

    public void checkClickedStar(int id)
    {
        //Debug.Log("Current star: " + currentStar);
        //Debug.Log("Clicked star: " + id);

        if (id == currentStar+1)
        {
            Debug.Log("Well choosed!");
            currentStar++;

            if (currentStar == 6) //|| currentStar == 34
            {
                Debug.Log("Level Completed!");
                level1Completed = true;
                levelCompleted();
            }

            // LEVEL 2
            if (currentStar == 41) //|| currentStar == 34
            {
                line2_1.SetActive(true);
                currentStar++;
            }
            if (currentStar == 49) //|| currentStar == 34
            {
                Debug.Log("Level Completed!");
                //Mostrar la constelación completa
                level2Completed = true;
                levelCompleted();
            }

            // LEVEL 3
            if (currentStar == 101) //|| currentStar == 34
            {
                line3_1.SetActive(true);
                currentStar++;
            }
            if (currentStar == 103) //|| currentStar == 34
            {
                line3_2.SetActive(true);
                currentStar++;
            }
            if (currentStar == 108) //|| currentStar == 34
            {
                Debug.Log("Level Completed!");
                level3Completed = true;
                levelCompleted();
            }
        }
    }

    public void levelCompleted()
    {
        level++;  //Change to the next constellation

        if (level == 2)
        {
            StartCoroutine(WaitBeforeNextLevel());
            currentStar = 34;
        }
        else if (level == 3)
        {
            StartCoroutine(WaitBeforeNextLevel());
            currentStar = 90;
        } else if (level3Completed)
        {
            //GAMEOVER
            Debug.Log("GAME OVER !!!");
        }

    }

    IEnumerator WaitBeforeNextLevel()
    {
        yield return new WaitForSeconds(2);
        if (level == 2)
            cam.gameObject.transform.position = new Vector3(6, 2, -371);
        else if (level == 3)
            cam.gameObject.transform.position = new Vector3(6f, 2f, -728);
    }
}
