using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreSystem : MonoBehaviour
{
    // Start is called before the first frame update
    string scoreKey = "score";
    public int CurrentScore { get; set; }
    public int topScore; 
    void Awake()
    {
        CurrentScore = PlayerPrefs.GetInt(scoreKey);
    }

    // Update is called once per frame
    public void setScore(int score)
    {
        
        PlayerPrefs.SetInt(scoreKey, score);
    }
   
}
