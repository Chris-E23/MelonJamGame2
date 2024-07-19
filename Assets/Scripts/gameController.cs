using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameController : MonoBehaviour
{
    private gamestates state = gamestates.betweenround;
    [SerializeField] private float timeRemaining, betweenTimer, spawnTimer, currSpawnTimer;
    [SerializeField] private TMP_Text timerText, roundText;
    [SerializeField] GameObject crate;
    [SerializeField] Transform cratePosition;
   
    private int round;
    public enum gamestates
    {
        betweenround, 
        currRound

    }
    // Start is called before the first frame update
    void Start()
    {
        state = gamestates.betweenround;
        timerText.text = "Time: " + (int)betweenTimer;
        currSpawnTimer = spawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        roundText.text = "Round " + round;
        //Could all just make this a giant switch statement, but fuck it.
        if (state == gamestates.betweenround && betweenTimer > 0)
        {
            betweenTimer -= Time.deltaTime * 1;
            timerText.text = "Time: " + (int)betweenTimer;
        }
        else if (betweenTimer <= 0)
        {
            state = gamestates.currRound;
        }
        if (state == gamestates.currRound && timeRemaining > 0)
        {
            timeRemaining -= 1 * Time.deltaTime;
            currSpawnTimer -= 1 * Time.deltaTime;
            if(spawnTimer <= 0)
            {
                spawnCrate();
                currSpawnTimer = spawnTimer;
            }
            
        }
        else if (state == gamestates.currRound && timeRemaining <= 0)
        {
            betweenTimer = 5f;
            state = gamestates.betweenround;
            round++;
        }

    }
    void spawnCrate()
    {
        Instantiate(crate, cratePosition.position, Quaternion.identity);

    }
}
