using System;
using System.Collections;
using System.Collections.Generic;
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
        private int num;
        void Start()
        {
            going = false;
            isClosestItem = false;
            target = null;
            holding = false;
            conveyer = false;
            num = 0;
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
            if (!going && Vector3.Magnitude(transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity) > 0)
            {
            transform.parent.gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity * 1, ForceMode2D.Force);
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
               player.GetComponent<playerController>().addObj(this.gameObject);
                 
              
            }

            if(collision.tag == "deathZone"){
                player.gameObject.GetComponent<playerController>().removeObj(transform.parent.gameObject);
                Destroy(transform.parent.gameObject);
                gameController.instance.gameObject.GetComponent<gameController>().addCrate();
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
                conveyer = false;
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
        public void setShit(int number)
        {
            num = number;
        } 
       
    }

