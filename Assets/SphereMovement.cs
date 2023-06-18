using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    float moveSpeed;
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
        moveSpeed = OpenAIHandler.GetComponent<OpenAITesting>().movementSpeed;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movement = transform.forward * moveSpeed;
        }
        GetComponent<Rigidbody>().velocity = movement;
    }
}
