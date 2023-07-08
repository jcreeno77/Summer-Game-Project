using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    [SerializeField] GameObject[] HoleList;
    [SerializeField] Sprite shadow;
    [SerializeField] Sprite hammer;
    GameObject target;
    float smashTimer;
    float animTimer;
    float moveTimer;

    float xVal;
    float yVal;

    bool hitting;
    // Start is called before the first frame update
    void Start()
    {
        smashTimer = 0;
        hitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        smashTimer += Time.deltaTime;
        //move randomly

        if(smashTimer > 1f && !hitting)
        {
            //smash begin
            hitting = true;
            target = HoleList[Random.Range(0, 8)];
            GetComponent<SpriteRenderer>().sprite = shadow;
        }

        if (hitting)
        {
            transform.position = target.transform.position;
            animTimer += Time.deltaTime;

            if (animTimer > .5f)
            {
                GetComponent<SpriteRenderer>().sprite = hammer;
                hitting = false;
                smashTimer = 0f;
                animTimer = 0f;
            }
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -3f);
        
        //transform.position = Vector3.Lerp(transform.position, new Vector3(xVal, yVal, -3.7f), Time.deltaTime);

        //target hole

        //target player
    }
}
