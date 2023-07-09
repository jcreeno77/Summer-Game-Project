using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTargetAI : MonoBehaviour
{
    [SerializeField] GameObject player;
    Transform stunHead;
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
        stunTimer = 0f;
        stunHead = transform.GetChild(0);
        stunHead.gameObject.SetActive(false);
        boxCollider = GetComponent<BoxCollider2D>();
        posi = new Vector3(15, 15, 15);
    }

    // Update is called once per frame
    void Update()
    {
        //checking if player goes above ground
        if (!inSequence)
        {
            boxCollider.enabled = false;
            pauseTimer += Time.deltaTime;
            if (player.GetComponent<charMoveHandler>().isAbove && pauseTimer > 2f)
            {
                posi = player.transform.position;
                posi.z -= .3f;
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
                    shadowIter += Time.deltaTime * 24;
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
        

        transform.position = posi;
    }
}
