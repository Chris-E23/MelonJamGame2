using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class sensor : MonoBehaviour
{
    [SerializeField] private AudioSource soda;
    [SerializeField] private AudioSource clock;
    private float timeToPlaySoda;

    void Start()
    {
      
        
    }

    
    void Update()
    {
       
    }
 
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "speedAdder":
                if(gameController.instance.getMoney() >= gameController.instance.getSpeedMoney())
                {
                   
                    transform.parent.gameObject.GetComponent<playerController>().addSpeed((int)gameController.instance.getSpeed());
                    gameController.instance.removeMoney(gameController.instance.getSpeedMoney());
                    soda.Play();
                    gameController.instance.setSpeedCost((int)(gameController.instance.getSpeedMoney() * 2));
                }
                
              
                break;
            case "timeAdder":
                if (gameController.instance.getMoney() >= gameController.instance.getTimeMoney())
                {
                    
                    gameController.instance.addTime(gameController.instance.getTime());
                    gameController.instance.removeMoney(gameController.instance.getTimeMoney());
                    gameController.instance.setTimeCost((int)(gameController.instance.getTimeMoney() * 2));
                    clock.Play();
                }
                    
                break;
        }
       

    }
}
