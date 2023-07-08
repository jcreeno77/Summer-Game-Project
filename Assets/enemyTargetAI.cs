using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTargetAI : MonoBehaviour
{
    [SerializeField] GameObject player;
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

        if (inSequence)
        {
            if(shadowIter < shadowSequence.Length-1)
            {
                shadowIter += Time.deltaTime*24;
                GetComponent<SpriteRenderer>().sprite = shadowSequence[(int)shadowIter];
            }
            else
            {
                if(smashIter < smashSequence.Length-1)
                {
                    if(smashIter > 2f && smashIter < 9f)
                    {
                        boxCollider.enabled = true;
                    }
                    else
                    {
                        boxCollider.enabled = false;
                    }
                    smashIter += Time.deltaTime*24;
                    GetComponent<SpriteRenderer>().sprite = smashSequence[(int)smashIter];
                }
                else
                {
                    inSequence = false;
                }
            }
        }

        transform.position = posi;
    }
}
