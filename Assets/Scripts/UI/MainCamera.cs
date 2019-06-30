//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class mainCameraScript : MonoBehaviour
//{
//    mainCameraScript instance;
//    SamuraiScript player;
//    public Camera mainCamera;
//    bool[] directions;
//    int vectorSum = 0;
//    bool isPlayerInside = true;

//    // Start is called before the first frame update
//    void Awake()
//    {
//        instance = this;
//    }

//    private void Start()
//    {
//        player = SamuraiScript.instance;
//        directions = new bool[4] {false, false, false, false};      //up, down, left, right
//    }

//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        if(collision.gameObject.GetComponent<SamuraiScript>())
//        {
//            //Debug.Log("Test1 (Box detects when player leaves it)");
//            //gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
//            isPlayerInside = false;
//        }
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.gameObject.GetComponent<SamuraiScript>())
//        {
//            //gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
//            isPlayerInside = true;
//        }
//    }


//    private void Update()
//    {
//        //Debug.Log(directions[0]);
//        if (!isPlayerInside)
//        {
//            if (player.gameObject.GetComponent<Collider2D>().bounds.min.y > transform.GetComponent<Collider2D>().bounds.max.y)
//            {
//                //mainCamera.transform.position = new Vector2(mainCamera.transform.position.x, player.transform.position.y - transform.position.y); 
//                directions[0] = true;
//            }
//            if (player.gameObject.GetComponent<Collider2D>().bounds.min.y < transform.GetComponent<Collider2D>().bounds.min.y)
//            {
//               // Debug.Log("Is BELOW");
//                directions[1] = true;
//            }
//            if (player.transform.position.x < transform.GetComponent<Collider2D>().bounds.min.x)
//            {
//                Debug.Log("Is LEFT");
//                directions[2] = true;
//            }
//            if (player.transform.position.x > transform.GetComponent<Collider2D>().bounds.max.x)
//            {
//                //Debug.Log("Is RIGHT");
//                directions[3] = true;
//            }
//        }
//        else
//        {
//            //Debug.Log("Is INSIDE");
//            directions[0] = false;
//            directions[1] = false;
//            directions[2] = false;
//            directions[3] = false;
//        }

//        if (directions[0])
//        {
//            if (player.rig.velocity.y > 0)
//            {
//                vectorSum =1;
//            }
//            else
//            {
//                vectorSum =0;
//            }
//        }

//       if (directions[1])
//        {
//            if (player.rig.velocity.y < 0)
//            {
//                vectorSum=1;
//            }
//            else
//            {
//                vectorSum=0;
//            }

//        }

//        //Debug.Log("Player y velocity: " + player.rig.velocity.y);
//        if (!isPlayerInside)
//        {
//            Debug.Log("Player out of box");
//            //gameObject.GetComponent<Rigidbody2D>().velocity = player.rig.velocity;
//        }


//        if (vectorSum != 0)
//        {
//        }
//        else
//        {
//        }
//    }
//}

using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public float yOffset;
    
    void Update()
    {
        if (target)
        {
            /*
             * K8's notes:
             * 
             * Update() is bad place for calling GetComponent.
             * You'd better cache Camera component to a variable on Start().
             */
            Vector3 point = this.gameObject.GetComponent<Camera>().WorldToViewportPoint(target.position);
            Vector3 delta = target.position - this.gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.35f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }
}