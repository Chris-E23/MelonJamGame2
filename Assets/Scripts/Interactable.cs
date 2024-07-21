using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

    public class Interactable : MonoBehaviour
    {
        bool isInRange;
        private bool isClosestItem;
        private GameObject player = null;
        private bool holding;
        private bool going;
         private Transform target;
         private bool conveyer;
        [SerializeField] private int num;
         private Dictionary<int, string> dict;    
          
        void Start()
        {
            dict = new Dictionary<int, string>();
            going = false;
            isClosestItem = false;
            target = null;
            holding = false;
            conveyer = false;
         
            dict[1] = "Akill";
            dict[2] = "Bkill";
            dict[3] = "Ckill";
            dict[4] = "Dkill";
            dict[5] = "crateCollector";
    }


        void Update()
        {

        
            if (going && target && conveyer)
            {
                transform.parent.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(target.position.x - transform.parent.position.x,0, 0) * 1f, ForceMode2D.Force);
               
            }
            else if(going && target)
            {
                transform.parent.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, target.position.y - transform.parent.position.y, 0) * 1f, ForceMode2D.Force);
            }
            else if (!going && Vector3.Magnitude(transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity) > 0 && !holding && conveyer)
            {
                transform.parent.gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity * 1, ForceMode2D.Force);
            }
            else if (Vector3.Magnitude(transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity) <= 0 && !going && conveyer)
            {
                conveyer = false;
            }
        if (Vector3.Magnitude(transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity) > 0 && holding)
            {
                //playsound
            }
           
            if (holding)
            {
                conveyer = false;
                transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            }


        }


        private void OnTriggerStay(Collider collision) {
            
        // Debug.Log(collision.tag);
            if (collision.tag == "water" && holding == false)
            {
               
                going = true;
                target = collision.gameObject.GetComponent<waterMovement>().getTarget();
                

            }
            else if (collision.tag == "conveyer" && holding == false)
             {

                    going = true;
                    target = collision.gameObject.GetComponent<waterMovement>().getTarget();
                    conveyer = true;

             }
            else
            {
                going = false;
            }
            if (collision.gameObject.CompareTag("Player"))
            {
                player = collision.gameObject;
                isInRange = true; 
                if(player&&player.GetComponent<playerController>())
                    player.GetComponent<playerController>().addObj(this.gameObject);
                 
              
            }

            if(collision.tag == dict[num]){
                player.gameObject.GetComponent<playerController>().removeObj(transform.parent.gameObject);
                Destroy(transform.parent.gameObject);
                gameController.instance.gameObject.GetComponent<gameController>().addCrate();
                if (collision.tag == "crateCollector")
                {
                    gameController.instance.addCrateMoney();
                    Destroy(transform.parent.gameObject); //im a terrible programmer LMAO!!!!!
                }
            }
            for(int i = 1; i <= 5; i++)
            {
                if (dict[i] == collision.tag && i != num)
                {
                    Destroy(transform.parent.gameObject);
                   
                    if (collision.tag == "crateCollector")
                    {
                        gameController.instance.addCrateMoney();
                        
                    }
                    else
                    {
                    gameController.instance.removeCrate();
                    }
                }
            }
            
            
            

    }
        private void OnTriggerExit(Collider collision)
        {
            if (!going)
            {
                isInRange = false;
                player.GetComponent<playerController>().removeObj(this.gameObject);
                isClosestItem = false;
            }
            else
            {
                going = false;
               
            }
           
        }

        public void isLowestDistObj(bool isNearest)
        {
            isClosestItem = isNearest;
        }
        public void setHolding(bool hold)
        {
            holding = hold;
        }
         
       
    }

