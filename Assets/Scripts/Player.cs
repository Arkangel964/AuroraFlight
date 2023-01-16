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
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerColl = GetComponent<CapsuleCollider>();
        shieldObj = transform.Find("Shield").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = forwardSpeed;
        

        if (controller.isGrounded)
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
                shieldTime = 115;
            }
        }
        if (shieldTime > 0)
        {
            shieldTime--;
            if(shieldTime <= 0)
            {
                shield = false;
                shieldObj.SetActive(false);
                shieldCooldown = 115;
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
        controller.Move(direction * Time.fixedDeltaTime);
        transform.position = Vector3.Lerp(transform.position, nextPosition, laneSwitchSmoothing * Time.deltaTime);
        if(transform.position.y < -15)
        {
            Destroy(gameObject);
        }
    }
    
    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Fire")
        {
            if (!shield)
            {
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.tag == "BlackHole")
        {
            Destroy(gameObject);
        }
    }
}
