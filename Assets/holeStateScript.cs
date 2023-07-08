using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holeStateScript : MonoBehaviour
{
    [SerializeField] Sprite[] holeStates;
    public int stateIter;
    public bool occupied;
    // Start is called before the first frame update
    void Start()
    {
        occupied = false;
    }

    // Update is called once per frame
    void Update()
    {
        stateIter = Mathf.Clamp(stateIter, 0, 2);
        GetComponent<SpriteRenderer>().sprite = holeStates[stateIter];
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && !occupied)
        {
            stateIter += 1;
            Debug.Log(collision.gameObject.name);
        }
    }
}
