using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    private Vector3 offset;
    public float smoothSpeed = 60f;
    public float followDistance = 10f;
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 moveTo = new Vector3(transform.position.x, transform.position.y, offset.z + player.position.z - followDistance);
        transform.position = Vector3.Lerp(transform.position, moveTo, smoothSpeed * Time.deltaTime);
    }
}
