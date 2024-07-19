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

        void Start()
        {
            isClosestItem = false;
        }


        void Update()
        {
            transform.parent.gameObject.GetComponent<Rigidbody>().AddForce(-Vector3.forward * 9.8f, ForceMode.Force);

        }


        private void OnTriggerEnter(Collider collision) { 
           
            if (collision.gameObject.CompareTag("Player"))
            {
                player = collision.gameObject;
                isInRange = true;
               player.GetComponent<playerController>().addObj(this.gameObject);
            
              
            }
        }
        private void OnTriggerExit(Collider collision)
        {
            isInRange = false;
            player.GetComponent<playerController>().removeObj(this.gameObject);
            isClosestItem = false;
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

