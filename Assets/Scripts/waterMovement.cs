using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] GameObject crate;
    private int step;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "crate")
        {
            crate = collision.gameObject;
        }
    }
}
