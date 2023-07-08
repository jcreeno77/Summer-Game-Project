using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] holeList;
    [SerializeField] Sprite[] shadowSequence;
    [SerializeField] Sprite[] smashSequence;
    Vector3 posi;
    float shadowIter;
    float smashIter;
    float pauseTimer;
    BoxCollider2D boxCollider;

    bool inSequence;
    // Start is called before the first frame update
    void Start()
    {
        inSequence = false;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!inSequence)
        {
            boxCollider.enabled = false;
            pauseTimer += Time.deltaTime;
            if (pauseTimer > 1f)
            {
                int randRange = Random.Range(0, holeList.Length);
                posi = holeList[randRange].transform.position;
                posi.z -= .1f;
                inSequence = true;
                shadowIter = 0;
                smashIter = 0;
                pauseTimer = 0;

            }
        }

        if (inSequence)
        {
            if (shadowIter < shadowSequence.Length - 1)
            {
                shadowIter += Time.deltaTime * 24;
                GetComponent<SpriteRenderer>().sprite = shadowSequence[(int)shadowIter];
            }
            else
            {
                if (smashIter < smashSequence.Length - 1)
                {
                    if (smashIter > 2f && smashIter < 9f)
                    {
                        boxCollider.enabled = true;
                    }
                    else
                    {
                        boxCollider.enabled = false;
                    }
                    smashIter += Time.deltaTime * 24;
                    GetComponent<SpriteRenderer>().sprite = smashSequence[(int)smashIter];
                }
                else
                {
                    inSequence = false;
                }
            }
        }

        transform.position = posi;
        //transform.position = Vector3.Lerp(transform.position, new Vector3(xVal, yVal, -3.7f), Time.deltaTime);

        //target hole

        //target player
    }
}
