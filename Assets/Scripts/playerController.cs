using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CharacterController controller;
    [SerializeField] private float playerSpeed, lowestDist, distance;
    [SerializeField] private Transform hand;
    private List<GameObject> nearestObjs;
    private int lowestDistIndex;
    private GameObject currObject;
    public LayerMask layersToHit;
    [SerializeField] private GameObject camera;
    
    void Start()
    {
        currObject = null;
                nearestObjs = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;

        camera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z-10);

        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        controller.Move(move.normalized * Time.deltaTime * playerSpeed);
       // controller.Move(-Vector3.up * Time.deltaTime * 9.8f);
       //Reading Input
        var input = Input.inputString;

        if (!string.IsNullOrEmpty(input))
        {
            switch (input)
            {
                case "e":
                    if(!currObject)
                    pickup();
                    break;
                case "f":
                    throwing();
                    break;
                default:
                    break;
            }
        }
        
      //Reading Input
        
        
        lowestDist = float.MaxValue;
        lowestDistIndex = 0;
        if (nearestObjs.Count > 0)
        {

            for (int i = 0; i < nearestObjs.Count; i++)
            {
                if(nearestObjs[i])
                     distance = Vector3.Distance(this.transform.position, nearestObjs[i].transform.position);

                if (distance < lowestDist)
                {
                    lowestDist = distance;
                    lowestDistIndex = i;

                }

            }

            for (int i = 0; i < nearestObjs.Count; i++)
            {
                if (i == lowestDistIndex && nearestObjs[i]) nearestObjs[i].gameObject.GetComponent<Interactable>().isLowestDistObj(true);
                
                else if(nearestObjs[i])nearestObjs[i].gameObject.GetComponent<Interactable>().isLowestDistObj(false);
                
            }

           


        }
        //Finding Closest Object

    }
    void pickup()
    {
        if(nearestObjs.Count > 0)
        {
            if (nearestObjs[lowestDistIndex])
            {
                nearestObjs[lowestDistIndex].transform.parent.position = hand.position;
                currObject = nearestObjs[lowestDistIndex].transform.parent.gameObject;
                currObject.transform.SetParent(transform);
                currObject.transform.GetChild(0).gameObject.GetComponent<Interactable>().setHolding(true); //Im killing myself
                                                                                                           //Destroy(currObject.GetComponent<Rigidbody2D>());
            }
        }
    }
    public void addObj(GameObject obj)
    {
        nearestObjs.Add(obj);
    }
    public void removeObj(GameObject obj)
    {
        nearestObjs.Remove(obj);
    }
    void throwing()
    {
        if (currObject)
        {
            currObject.transform.SetParent(null);
            currObject.transform.GetChild(0).gameObject.GetComponent<Interactable>().setHolding(false);

            currObject = null;
        }
       
        

    }
    public void addSpeed(int speed)
    {
        playerSpeed += speed;
    }
}
