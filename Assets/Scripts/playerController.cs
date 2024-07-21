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
        
        Vector2 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Collider2D[] col = Physics2D.OverlapPointAll(v);

        if (col.Length > 0)
        {
            foreach (Collider2D c in col)
            {
               
                if(c.gameObject.transform.position.x - transform.position.x <= 1 && c.gameObject.transform.position.y - transform.position.y <= 1 && c.tag == "crate")
                {
                    currObject = c.gameObject;
                    //Debug.Log("Collided with: " + c.gameObject.name);
                }
                else
                {
                    currObject = null;
                }
               
            }

        }


            // controller.Move(-Vector3.up * Time.deltaTime * 9.8f);
            //Reading Input





            var input = Input.inputString;

        if (!string.IsNullOrEmpty(input))
        {
            switch (input)
            {
                case "e":
                    if(currObject)
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

      

        //Finding Closest Object

    }
    void pickup()
    {
        
                currObject.transform.position = hand.position;
                //currObject = nearestObjs[lowestDistIndex].transform.parent.gameObject;
                currObject.transform.SetParent(transform);
                currObject.transform.GetChild(0).gameObject.GetComponent<Interactable>().setHolding(true); //Im killing myself
                                                                                                           //Destroy(currObject.GetComponent<Rigidbody2D>());
            currObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            

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
            currObject.GetComponent<Rigidbody2D>().AddForce(transform.forward * 5, ForceMode2D.Impulse);
            currObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            currObject = null;
            
        }
       
        

    }
    public void addSpeed(int speed)
    {
        playerSpeed += speed;
    }
}
