using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    
    public Transform getTarget()
    {
        return target; 
    }
}
