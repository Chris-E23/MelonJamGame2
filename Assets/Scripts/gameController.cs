using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Net.Sockets;

public class gameController : MonoBehaviour
{
    private gamestates state = gamestates.betweenround;
    [SerializeField] private float timeRemaining, betweenTimer, currBetweenTimer, spawnTimer, currSpawnTimer, currTimeRemaining, speedToAdd;
    [SerializeField] private Text timerText, roundText, crateText, targetText, crateMoneyText, addTimeText, addSpeedText, scoreText, currScoreText;
    [SerializeField] GameObject crate;
    [SerializeField] Transform cratePosition;
    List<GameObject> crateList;
    private int currCrates, round; 
    public static gameController instance;
   [SerializeField] private GameObject A, B, C, D, bad;
    [SerializeField] private int target;
    [SerializeField] private GameObject gameOverScreen, player, waitButton, pauseScreen;
    [SerializeField] private scoreSystem sys;
    private int leftOver;
    private int cratesMoney;
    private int score;
    [SerializeField] private int moneyToRemoveForSpeed, moneyToRemoveForTime, timeToAdd;
    [SerializeField] private Text speedText, timeText;
    [SerializeField] private AudioSource crateCollector;
    private bool paused;
    public enum gamestates
    {
        betweenround, 
        currRound,
        gameOver

    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        state = gamestates.betweenround;
        timerText.text = "Time: " + (int)(betweenTimer+1);
        betweenTimer = currBetweenTimer;
        crateList = new List<GameObject>();
        leftOver = 0;
        cratesMoney = 0;
        waitButton.SetActive(false);
        paused = false;
        pauseScreen.SetActive(false);
        speedToAdd += 1;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(spawnTimer > 0)
            spawnTimer = 5 - .1f * round - PlayerPrefs.GetFloat("Speed")*.9f;

        currScoreText.text = "Score: " + score;

        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            Time.timeScale = 0;
            paused = true;
            pauseScreen.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && paused)
        {   Time.timeScale = 1;
            paused = false;
            pauseScreen.SetActive(false);
        }



        timeText.text = "+ " + timeToAdd + " secs\n" + "Cost: " + moneyToRemoveForTime;
        speedText.text = "+ " + speedToAdd + " speed\n" + "Cost: " + moneyToRemoveForSpeed;

        crateText.text = "Crates: " + currCrates;
        roundText.text = "Round: " + (round+1);
        targetText.text = "Target: " + target;
        crateMoneyText.text = "Money: " + cratesMoney;
        //Could all just make this a giant switch statement, but fuck it.
        if (state == gamestates.betweenround && currBetweenTimer > 0)
        {
            currBetweenTimer -= Time.deltaTime * 1;
            timerText.text = "Changing Rounds: " + (int)(currBetweenTimer+1);
            currCrates = 0;
            waitButton.SetActive(true);
            foreach(GameObject crate in crateList){
                if (crate)
                {
                    leftOver++;
                }
                Destroy(crate);
            }
            crateList.Clear();
            while(leftOver > 0)
            {
                leftOver--;
                target++;
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
            if (currCrates >= target)
            {
                waitButton.SetActive(true);
            }
        }
        else if (state == gamestates.currRound && currTimeRemaining <= 0)
        {
            
            if(currCrates >= target)
            {
                target++;
                state = gamestates.betweenround;
                round++;
                speedToAdd += round * .1f;
               
            }
            else
            {
                state = gamestates.gameOver;
                Destroy(player.GetComponent<playerController>());
            }
            
            currTimeRemaining = timeRemaining;


        }
        if(state == gamestates.gameOver)
        {
            gameOverScreen.SetActive(true);
            if (sys.CurrentScore > score)
            {
                scoreText.text = "Score: " + score;
            }
            else
            {
                scoreText.text = "New High Score! : " + score;
                sys.setScore(score);
            }
                

        }

    }
    void spawnCrate()
    {
        GameObject newCrate;
        int rand = (int)Random.Range(1, 6);
        switch (rand)
        {
            case 1:
                newCrate = Instantiate(A, cratePosition.position, Quaternion.identity);
                break;
            case 2:
                newCrate = Instantiate(B, cratePosition.position, Quaternion.identity);
                break;
            case 3:
                newCrate = Instantiate(C, cratePosition.position, Quaternion.identity);
                break;
            case 4:
                newCrate = Instantiate(D, cratePosition.position, Quaternion.identity);
                break;
            case 5:
                newCrate = Instantiate(bad, cratePosition.position, Quaternion.identity);
                break;
             default:
                newCrate = Instantiate(bad, cratePosition.position, Quaternion.identity);
                break;

        }
       
        crateList.Add(newCrate);
        
    }
    public void addCrate(){  currCrates++;}
    public void addCrateMoney(){ cratesMoney++;}
    public void removeCrate(){ currCrates--;}
    public void removeMoney(int money){cratesMoney -= money;}
    public void restart(){SceneManager.LoadScene(1);}
    public void mainMenu(){SceneManager.LoadScene(0);}
    public void addTime(int time){ currTimeRemaining += time; timeRemaining += time; }
    
    public void skip()
    {
        if(state == gamestates.betweenround)
        {
            state = gamestates.currRound;
            currTimeRemaining = timeRemaining;
            currBetweenTimer = betweenTimer;
            waitButton.SetActive(false);
        }
        else if(state == gamestates.currRound)
        {
            state = gamestates.betweenround;
            currTimeRemaining = timeRemaining;
            round++;
           
        }
        
    }

    public int getSpeedMoney(){return moneyToRemoveForSpeed;}
    public int getTimeMoney(){return moneyToRemoveForTime;}
    public int getTime(){ return timeToAdd; }
    public float getSpeed(){return speedToAdd;}
    public int getMoney() { return cratesMoney; }
    public void cont()
    {
        Time.timeScale = 1;
        paused = false;
        pauseScreen.SetActive(false);
    }
    public void setTimeCost(int cost)
    {
        moneyToRemoveForTime += cost;

    }
    public void setSpeedCost(int cost)
    {
        moneyToRemoveForSpeed += cost;

    }
    public void addScore(int addition)
    {
        score += addition;
    }
    public void playCrateCollector()
    {
        crateCollector.Play();
    }
}
