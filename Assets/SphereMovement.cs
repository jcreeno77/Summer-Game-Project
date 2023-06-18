using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    float moveSpeed;
    float vSpeed;
    float hSpeed;
    [SerializeField] GameObject OpenAIHandler;
    Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        //up key
        moveSpeed = OpenAIHandler.GetComponent<OpenAITesting>().movementSpeed;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            vSpeed += 1f;

        }else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            vSpeed -= 1f;
        }
        //down key
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            vSpeed += -1f;

        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            vSpeed -= -1f;
        }
        //right key
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            hSpeed += 1f;

        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            hSpeed -= 1f;

        }
        //left key
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            hSpeed += -1f;

        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            hSpeed -= -1f;
        }
        //assign movement
        movement *= 0;
        movement += transform.forward * vSpeed;
        movement += transform.right * hSpeed;
        movement = movement.normalized;
        movement *= moveSpeed;
        GetComponent<Rigidbody>().velocity = movement;
    }
}
