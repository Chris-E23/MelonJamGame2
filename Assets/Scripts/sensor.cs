using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class sensor : MonoBehaviour
{
    
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
                    transform.parent.gameObject.GetComponent<playerController>().addSpeed(gameController.instance.getSpeed());
                    gameController.instance.removeMoney(gameController.instance.getSpeedMoney());
                }
              
                break;
            case "timeAdder":
                if (gameController.instance.getMoney() >= gameController.instance.getTimeMoney())
                {
                    gameController.instance.addTime(gameController.instance.getTime());
                    gameController.instance.removeMoney(gameController.instance.getTimeMoney());
                }
                    
                break;
        }
       

    }
}
