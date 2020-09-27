using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constellations_Minigame : MinigameController
{
    public static Constellations_Minigame controller;
    public Camera cam;
    private int level;
    private int currentStar;
    //public GameObject line1, line2_1, line2_2, line3_1, line3_2, line3_3;
    public GameObject line1__1, line1__2, line1__3, line1__4, line1__5, line1__6, line1__7;
    public GameObject line2__1, line2__2, line2__3, line2__4, line2__5, line2__6, line2__7, line2__8, line2__9, line2__10, line2__11, line2__12, line2__13, line2__14, line2__15, line2__16;
    public GameObject line3__1, line3__2, line3__3, line3__4, line3__5, line3__6, line3__7, line3__8, line3__9, line3__10, line3__11, line3__12, line3__13, line3__14, line3__15, line3__16, line3__17, line3__18, line3__19, line3__20;

    private bool level1Completed, level2Completed, level3Completed;

    private int[] constellation_1; //constellation_2 = null, constellation_3 = null;

    public Animator canvasAnimator;
    [Header("Audios")]
    public AudioSource audioSource;
    public AudioClip starClicked;
    public AudioClip groupCompleted;
    public AudioClip levelCompletedSound;
    public AudioClip gameCompletedSound;


    // Start is called before the first frame update
    void Start()
    {
        controller = this;
        //line1.SetActive(false); line2_1.SetActive(false); line2_2.SetActive(false); line3_1.SetActive(false); line3_2.SetActive(false); line3_3.SetActive(false);

        line1__1.SetActive(false); line1__2.SetActive(false); line1__3.SetActive(false); line1__4.SetActive(false); line1__5.SetActive(false); line1__6.SetActive(false); line1__7.SetActive(false);

        line2__1.SetActive(false); line2__2.SetActive(false); line2__3.SetActive(false); line2__4.SetActive(false); line2__5.SetActive(false); line2__6.SetActive(false); line2__7.SetActive(false); line2__8.SetActive(false);
        line2__9.SetActive(false); line2__10.SetActive(false); line2__11.SetActive(false); line2__12.SetActive(false); line2__13.SetActive(false); line2__14.SetActive(false); line2__15.SetActive(false); line2__16.SetActive(false);

        line3__1.SetActive(false); line3__2.SetActive(false); line3__3.SetActive(false); line3__4.SetActive(false); line3__5.SetActive(false); line3__6.SetActive(false); line3__7.SetActive(false); line3__8.SetActive(false);
        line3__9.SetActive(false); line3__10.SetActive(false); line3__11.SetActive(false); line3__12.SetActive(false); line3__13.SetActive(false); line3__14.SetActive(false); line3__15.SetActive(false); line3__16.SetActive(false);
        line3__17.SetActive(false); line3__18.SetActive(false); line3__19.SetActive(false); line3__20.SetActive(false);

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
        } else if (level2Completed)
        {
            level2Completed = false;
        } else if (level3Completed)
        {
            level3Completed = false;
        }
    }

    private void enableLines()
    {
        //Debug.Log("level " + level);
        //Debug.Log("current star " + currentStar);

        if (level == 1)
        {
            switch (currentStar)
            {
                case 1:
                    line1__1.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 2:
                    line1__2.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 3:
                    line1__3.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 4:
                    line1__4.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 5:
                    line1__5.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 6:
                    line1__6.SetActive(true);
                    PlayAudio(groupCompleted);
                    break;
                case 7:
                    line1__7.SetActive(true);
                    break;
            }
        } else if (level == 2)
        {
            switch (currentStar)
            {
                case 35:
                    line2__1.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 36:
                    line2__2.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 37:
                    line2__3.SetActive(true);
                     PlayAudio(starClicked);
                    break;
                case 38:
                    line2__4.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 39:
                    line2__5.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 40:
                    line2__6.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 41:
                    line2__7.SetActive(true);
                    PlayAudio(groupCompleted);
                    break;
                case 43:
                    line2__8.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 44:
                    line2__9.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 45:
                    line2__10.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 46:
                    line2__11.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 47:
                    line2__12.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 48:
                    line2__13.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 49:
                    line2__14.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 50:
                    line2__15.SetActive(true);
                    line2__16.SetActive(true);
                    PlayAudio(groupCompleted);
                    break;
            }
        } else if (level == 3) {
            switch (currentStar)
            {
                case 91:
                    line3__1.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 92:
                    line3__2.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 93:
                    line3__3.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 94:
                    line3__4.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 95:
                    line3__5.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 96:
                    line3__6.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 97:
                    line3__7.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 98:
                    line3__8.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 99:
                    line3__9.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 100:
                    line3__10.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 101:
                    line3__11.SetActive(true);
                    PlayAudio(groupCompleted);
                    break;

                case 103:
                    line3__12.SetActive(true);
                    line3__13.SetActive(true);
                    PlayAudio(groupCompleted);
                    break;

                case 105:
                    line3__14.SetActive(true);
                    PlayAudio(starClicked);
                    break;

                case 106:
                    line3__15.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 107:
                    line3__16.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 108:
                    line3__17.SetActive(true);
                    PlayAudio(starClicked);
                    break;
                case 109:
                    line3__18.SetActive(true);
                    line3__19.SetActive(true);
                    line3__20.SetActive(true);
                    PlayAudio(groupCompleted);
                    break;
            }
        }
       
    }

    public void checkClickedStar(int id)
    {
        //Debug.Log("Current star: " + currentStar);
        //Debug.Log("Clicked star: " + id);

        if (id == currentStar+1)
        {
            //Debug.Log("Well choosed!");
            currentStar++;
            enableLines();

            if (currentStar == 6) //|| currentStar == 34
            {
                Debug.Log("Level Completed!");
                level1Completed = true;
                //PlayAudio(levelCompletedSound);
                levelCompleted();
            }

            /*
            // LEVEL 2
            if (currentStar == 41) //|| currentStar == 34
            {
                //line2_1.SetActive(true);
                currentStar++;
                enableLines();
            } */

            if (currentStar == 49) //|| currentStar == 34
            {
                Debug.Log("Level Completed!");
                //Mostrar la constelación completa
                level2Completed = true;
                //PlayAudio(levelCompletedSound);
                levelCompleted();
            }

            // LEVEL 3
            /*
            if (currentStar == 101) //|| currentStar == 34
            {
                //line3_1.SetActive(true);
                currentStar++;
                enableLines();
            }
            if (currentStar == 103) //|| currentStar == 34
            {
                //line3_2.SetActive(true);
                currentStar++;
                enableLines();
            } */

            if (currentStar == 108) //|| currentStar == 34
            {
                Debug.Log("Level Completed!");
                level3Completed = true;
                //PlayAudio(gameCompletedSound);
                levelCompleted();
            }
        }
    }

    public void levelCompleted()
    {
        currentStar++;
        enableLines();
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
            canvasAnimator.Play("PhotoHockeyShow");
            Debug.Log("MINIGAME COMPLETED !!!");
        }

    }

    IEnumerator WaitBeforeNextLevel()
    {
        yield return new WaitForSeconds(1);
        canvasAnimator.Play("BlackScreen");
        yield return new WaitForSeconds(0.5f);
        
        if (level == 2)
            cam.gameObject.transform.position = new Vector3(6, 2, -371);
        else if (level == 3)
            cam.gameObject.transform.position = new Vector3(6f, 2f, -728);
    }

    void PlayAudio(AudioClip clip){
        audioSource.clip = clip;
        audioSource.Play();
    }
}
