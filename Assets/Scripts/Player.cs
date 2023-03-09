using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float laneSwitchSmoothing = 60f;
    private int lane = 1;
    public float laneOffset = 4;
    public float jumpForce;
    public float gravity = -9.81f;
    private Collider playerColl;
    private bool shield;
    public int shieldTime = 0;
    public int shieldCooldown = 0;
    private GameObject shieldObj;
    private Vector3 touchStart;   //First touch position
    private Vector3 touchEnd;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerColl = GetComponent<CapsuleCollider>();
        shieldObj = transform.Find("Shield").gameObject;
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                touchStart = touch.position;
                touchEnd = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                touchEnd = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                touchEnd = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(touchEnd.x - touchStart.x) > dragDistance || Mathf.Abs(touchEnd.y - touchStart.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(touchEnd.x - touchStart.x) > Mathf.Abs(touchEnd.y - touchStart.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((touchEnd.x > touchStart.x))  //If the movement was to the right)
                        {   //Right swipe
                            lane++;
                            if (lane > 2)
                            {
                                lane = 2;
                            }
                        }
                        else
                        {   //Left swipe
                            lane--;
                            if (lane < 0)
                            {
                                lane = 0;
                            }
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (controller.isGrounded)
                        {
                            if (touchEnd.y > touchStart.y)
                            {
                                direction.y = jumpForce;

                            }
                        }
                        if(touchEnd.y < touchStart.y)
                        {   //Down swipe
                            if (!shield && shieldCooldown == 0 && shieldTime == 0)
                            {
                                shield = true;
                                shieldObj.SetActive(true);
                                shieldTime = 120;
                            }
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
        
        direction.z = forwardSpeed;

        if(!controller.isGrounded)
        {
            direction.y += gravity * Time.deltaTime;
        }
        /*if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                direction.y = jumpForce;

            }
        } else
        {
            direction.y += gravity * Time.deltaTime;
        }
            
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            lane++;
            if (lane > 2)
            {
                lane = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lane--;
            if (lane < 0)
            {
                lane = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!shield && shieldCooldown == 0 && shieldTime == 0)
            {
                shield = true;
                shieldObj.SetActive(true);
                shieldTime = 120;
            }
        }*/
        if (shieldTime > 0)
        {
            shieldTime--;
            if(shieldTime <= 0)
            {
                shield = false;
                shieldObj.SetActive(false);
                shieldCooldown = 30;
            }
        }
        if (shieldCooldown > 0)
        {
            shieldCooldown--;
        }

        Vector3 nextPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lane == 0)
        {
            nextPosition += Vector3.left * laneOffset;
        }
        else if (lane == 2)
        {
            nextPosition += Vector3.right * laneOffset;
        }
        controller.Move(direction * Time.fixedDeltaTime * Time.timeScale);
        transform.position = Vector3.Lerp(transform.position, nextPosition, laneSwitchSmoothing * Time.deltaTime * Time.timeScale);
        if(transform.position.y < -15)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Fire")
        {
            if (!shield)
            {
                Destroy(gameObject);
            } else
            {
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.tag == "BlackHole")
        {
            Destroy(gameObject);
        }
    }
}
