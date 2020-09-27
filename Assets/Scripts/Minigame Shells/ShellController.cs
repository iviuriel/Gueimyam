using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MinigameController
{
    public GameObject[] shells;
    public int currentLevel;

    public int[] elementsPerLevel;
    public int[] rowsPerLevel;

    private List<GameObject> selectedShells;

    private Animator animator;

    public bool endGame;

    [Header("SFX")]
    public AudioSource audioSource;
    public AudioClip clickShell;
    public AudioClip correctShell;
    public AudioClip wrongShell;
    public AudioClip levelCompleted;
    public AudioClip gameCompleted;

    // Start is called before the first frame update

    void Awake(){
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        selectedShells = new List<GameObject>();
        currentLevel = -1;
        endGame = false;
        NextLevel();
    }

    void Update(){
        if(!endGame){
            if (Input.GetMouseButtonDown(0))
            {
                CheckClick();
            }

            CheckShells();
            if(IsEndOfLevel()){
                if(currentLevel != 2){
                    PlayAudio(levelCompleted);
                }else{
                    PlayAudio(gameCompleted);
                }
                NextLevel();
            }
        }
    }

    void NextLevel(){
        currentLevel++;
        if(currentLevel == 3){
            endGame = true;
            animator.SetTrigger("NextLevel");            
            return;
        }

        //We add the shells
        List<GameObject> shellsInTheLevel = new List<GameObject>();
        for(int i = 0; i < elementsPerLevel[currentLevel] / 4; i++){
            //Añadimos 4 de cada copia
            shellsInTheLevel.Add(shells[i]);
            shellsInTheLevel.Add(shells[i]);
            shellsInTheLevel.Add(shells[i]);
            shellsInTheLevel.Add(shells[i]);
        }
        //Shuffle the elements
        shellsInTheLevel = ShuffleShells(shellsInTheLevel);

        //We set them in the game
        Transform table = transform.GetChild(currentLevel);

        for(int i = 0; i< shellsInTheLevel.Count; i++){
            GameObject s = Instantiate(shellsInTheLevel[i], table.GetChild(0).GetChild(i).position - new Vector3(0,0,2), Quaternion.identity, table.GetChild(1));
            s.transform.localScale = new Vector3 (1/table.localScale.x, 1/table.localScale.z, 1/table.localScale.z );
            //s.transform.localPosition = new Vector3 (s.transform.localPosition.x, s.transform.localPosition.y, 0f );
        }

        animator.SetTrigger("NextLevel");

    }

    List<GameObject> ShuffleShells(List<GameObject> elements){
        List<GameObject> tmp = new List<GameObject>();
        int max = elements.Count;
        while (max > 0)
        {
            int offset = UnityEngine.Random.Range(0, max);
            tmp.Add(elements[offset]);
            elements.RemoveAt(offset);
            max -= 1;
        }

        return tmp;
    }

    void CheckClick(){
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {            
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Shell"))
                {
                   GameObject s = hit.collider.gameObject;
                   if(selectedShells.Contains(s)){
                       selectedShells.Remove(s);                                          
                   }else{
                       selectedShells.Add(s);                       
                   }  
                   PlayAudio(clickShell);
                   s.GetComponent<Animator>().SetTrigger("Toggle");                     
                }
            }
        }
    }

    void CheckShells(){
        //StartCoroutine("ClickDelay");
        if(selectedShells.Count == 2 && selectedShells[0].GetComponent<Shell>().selected && selectedShells[1].GetComponent<Shell>().selected){
            CompareShells();            
        }
    }

    void CompareShells(){
        Shell s1 = selectedShells[0].GetComponent<Shell>();
        Shell s2 = selectedShells[1].GetComponent<Shell>();
        selectedShells = new List<GameObject>();

        if(s1.id == s2.id){
            s1.GetComponent<Animator>().SetTrigger("Correct");
            s2.GetComponent<Animator>().SetTrigger("Correct");
            PlayAudio(correctShell);
        }else{
            s1.GetComponent<Animator>().SetTrigger("Toggle");
            s2.GetComponent<Animator>().SetTrigger("Toggle");
            PlayAudio(wrongShell);
        }
    }

    bool IsEndOfLevel(){
        Transform table = transform.GetChild(currentLevel);
        return table.GetChild(1).childCount == 0;
    }

    void PlayAudio(AudioClip clip){
        audioSource.clip = clip;
        audioSource.Play();
    }
}
