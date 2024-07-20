using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    private gamestates state = gamestates.betweenround;
    [SerializeField] private float timeRemaining, betweenTimer, currBetweenTimer, spawnTimer, currSpawnTimer, currTimeRemaining;
    [SerializeField] private Text timerText, roundText, crateText;
    [SerializeField] GameObject crate;
    [SerializeField] Transform cratePosition;
    List<GameObject> crateList;
    private int currCrates; 
    private int round;
    public static gameController instance;
    public enum gamestates
    {
        betweenround, 
        currRound

    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        state = gamestates.betweenround;
        timerText.text = "Time: " + (int)(betweenTimer+1);
        betweenTimer = currBetweenTimer;
        crateList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        crateText.text = "Crates: " + currCrates;
        roundText.text = "Round: " + (int)(round+1);

        //Could all just make this a giant switch statement, but fuck it.
        if (state == gamestates.betweenround && currBetweenTimer > 0)
        {
            currBetweenTimer -= Time.deltaTime * 1;
            timerText.text = "Changing Rounds: " + (int)(currBetweenTimer+1);
            currCrates = 0;
            foreach(GameObject crate in crateList){
                Destroy(crate);
            }
        }
        else if (currBetweenTimer <= 0)
        {
            state = gamestates.currRound;
            currBetweenTimer = betweenTimer;
            currTimeRemaining = timeRemaining;
        }
        if (state == gamestates.currRound && currTimeRemaining > 0)
        {
            currTimeRemaining -= 1 * Time.deltaTime;
            currSpawnTimer -= 1 * Time.deltaTime;
            timerText.text = "Current Round: " + (int)(currTimeRemaining+1);
            if(currSpawnTimer <= 0)
            {
                spawnCrate();
                currSpawnTimer = spawnTimer;
            }
            
        }
        else if (state == gamestates.currRound && currTimeRemaining <= 0)
        {
            currTimeRemaining = timeRemaining;
            state = gamestates.betweenround;
            round++;
            
        }

    }
    void spawnCrate()
    {
        GameObject newCrate = Instantiate(crate, cratePosition.position, Quaternion.identity);
        crateList.Add(newCrate);
    }
    public void addCrate()
    {
        currCrates++;
    }
}
