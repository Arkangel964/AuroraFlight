using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    private Vector3 offset;
    public float smoothSpeed = 60f;
    public float followDistance = 10f;
    void Start()
    {
        try
        {
            offset = transform.position - player.position;
        }
        catch (System.Exception)
        {
            
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        try
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            } else
            {
                Vector3 moveTo = new Vector3(transform.position.x, transform.position.y, offset.z + player.position.z - followDistance);
                transform.position = Vector3.Lerp(transform.position, moveTo, smoothSpeed * Time.deltaTime);
            }
        }
        catch (System.Exception)
        {

        }
        
    }
}
