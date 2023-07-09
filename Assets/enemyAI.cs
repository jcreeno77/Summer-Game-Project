using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    [SerializeField] GameObject player;
    Transform stunHead;
    [SerializeField] GameObject[] holeList;
    [SerializeField] Sprite[] shadowSequence;
    [SerializeField] Sprite[] smashSequence;
    Vector3 posi;
    float shadowIter;
    float smashIter;
    float pauseTimer;
    BoxCollider2D boxCollider;

    bool inSequence;
    public bool stunned;
    public float stunTimer;
    // Start is called before the first frame update
    void Start()
    {
        inSequence = false;
        stunned = false;
        stunHead = transform.GetChild(0);
        stunHead.gameObject.SetActive(false);
        boxCollider = GetComponent<BoxCollider2D>();
        posi = new Vector3(15, 15, 15);
    }

    // Update is called once per frame
    void Update()
    {

        if (!inSequence)
        {
            boxCollider.enabled = false;
            pauseTimer += Time.deltaTime;
            if (pauseTimer > 2.5f)
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

        if (stunned)
        {
            stunTimer += Time.deltaTime;
            stunHead.gameObject.SetActive(true);
            //temp
            smashIter = 9f;
            stunned = false;
            stunHead.gameObject.SetActive(false);
            if (stunTimer > 45f)
            {
                //boxCollider.enabled = false;
                stunHead.gameObject.SetActive(false);
                stunned = false;
                stunTimer = 0f;
            }
        }
        else
        {
            if (inSequence)
            {
                if (shadowIter < shadowSequence.Length - 1)
                {
                    transform.localScale = new Vector3(3, 3, 3);
                    shadowIter += Time.deltaTime * 15;
                    GetComponent<SpriteRenderer>().sprite = shadowSequence[(int)shadowIter];
                }
                else
                {
                    if (smashIter < smashSequence.Length - 1)
                    {
                        if (smashIter > 2f && smashIter < 9f)
                        {
                            transform.localScale = new Vector3(2.11f, 2.11f, 2.11f);
                            boxCollider.enabled = true;
                        }
                        else
                        {
                            boxCollider.enabled = false;
                        }
                        smashIter += Time.deltaTime * 15;
                        GetComponent<SpriteRenderer>().sprite = smashSequence[(int)smashIter];
                    }
                    else
                    {
                        inSequence = false;
                    }
                }
            }
        }

        posi.z = -4.2f;
        transform.position = posi;
        //transform.position = Vector3.Lerp(transform.position, new Vector3(xVal, yVal, -3.7f), Time.deltaTime);

        //target hole

        //target player
    }
}
