using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class charMoveHandler : MonoBehaviour
{
    [SerializeField] GameObject[] HoleList;
    [SerializeField] GameObject Enemy1;
    [SerializeField] GameObject Enemy2;
    [SerializeField] GameObject ScoreDisplayObj;
    [SerializeField] GameObject stockPanel;
    [SerializeField] GameObject endScreenCanvas;
    [SerializeField] GameObject pauseScreenCanvas;
    [SerializeField] GameObject getReadyCanvas;

    bool paused;
    bool spinSoundPlayed;
    bool hurtSoundPlayed;

    [SerializeField] Sprite aboveGroundSpr;
    [SerializeField] Sprite belowGroundSpr;
    [SerializeField] Sprite[] attackAnim;
    [SerializeField] Sprite[] diveDownAnim;
    [SerializeField] Sprite[] diveUpAnim;

    float attackAnimIter = 0;
    float diveDownAnimIter = 0;
    float diveUpAnimIter = 0;
    float score;

    ParticleSystem partSys;
    GameObject Closest;
    BoxCollider2D boxCollider;

    Vector3 positionSet;
    Vector2 particleDirection;

    Transform stunHead;
    public string switchState;
    bool underground = false;
    public bool isAbove;
    bool diveDownPlayed;

    float stunTimer;

    [SerializeField] private AudioSource digSoundEffect;
    [SerializeField] private AudioSource emergeSoundEffect;
    [SerializeField] private AudioSource submergeSoundEffect;
    [SerializeField] private AudioSource swingSoundEffect;
    [SerializeField] private AudioSource hitSoundEffect;
    [SerializeField] AudioClip moleHurt;
    [SerializeField] AudioClip moleSwing;
    [SerializeField] AudioClip hitHammer;
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        score = 0;
        stunTimer = 0f;
        switchState = "aboveground";
        partSys = GetComponent<ParticleSystem>();
        //boardWidth = HoleList.Length / 2;
        //HoleList = new GameObject[2,2];
        positionSet = HoleList[0].transform.position;
        positionSet.z = -3.7f;
        particleDirection.x = 0;
        particleDirection.y = 0;
        stunHead = transform.GetChild(0);
        stunHead.gameObject.SetActive(false);
        boxCollider = GetComponent<BoxCollider2D>();
        spinSoundPlayed = false;
        hurtSoundPlayed = false;
        diveDownPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {

        switch (switchState)
        {
            #region
            case "underground":
                transform.localScale = new Vector3(1, 1, 1);
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    positionSet.y += 3 * Time.deltaTime;
                    particleDirection.y = 1;

                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    positionSet.y -= 3 * Time.deltaTime;
                    particleDirection.y = -1;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    positionSet.x -= 3 * Time.deltaTime;
                    particleDirection.x = -1;
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    positionSet.x += 3 * Time.deltaTime;
                    particleDirection.x = 1;
                }

                if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
                {
                    particleDirection.y = 0;
                }
                if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
                {
                    particleDirection.x = 0;
                }

                //going above ground
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    emergeSoundEffect.Play();
                    digSoundEffect.Stop();
                    //get initial
                    for (int i = 0; i < HoleList.Length; i++)
                    {
                        if(HoleList[i].GetComponent<holeStateScript>().stateIter != 2)
                        {
                            Closest = HoleList[i];
                            break;
                        }
                    }

                        //assign closest
                    for (int i = 0; i < HoleList.Length; i++)
                    {

                        if (GetDistance(HoleList[i]) < GetDistance(Closest) && HoleList[i].GetComponent<holeStateScript>().stateIter != 2)
                        {
                            Closest = HoleList[i];
                        }
                    }
                    //positionSet = Closest.transform.position;
                    //GetComponent<SpriteRenderer>().sprite = aboveGroundSpr;
                }
                if (Closest)
                {
                    Closest.GetComponent<holeStateScript>().occupied = true;
                    if (GetDistance(Closest) > .3f)
                    {
                        boxCollider.enabled = true;
                        positionSet = Vector3.Lerp(positionSet, Closest.transform.position, 55f * Time.deltaTime);
                    }
                    else
                    {
                        positionSet = Closest.transform.position;
                        //GetComponent<SpriteRenderer>().sprite = aboveGroundSpr;
                        underground = false;
                        
                        switchState = "diveUp";
                        
                        Closest.GetComponent<holeStateScript>().stateIter -= 1;

                        if (GetDistance(Enemy1) < .3f && Enemy1.GetComponent<enemyAI>().stunned)
                        {
                            Enemy1.GetComponent<enemyAI>().stunTimer = 45;
                        }
                        if (GetDistance(Enemy2) < .3f && Enemy2.GetComponent<enemyTargetAI>().stunned)
                        {
                            Enemy2.GetComponent<enemyTargetAI>().stunTimer = 45;
                        }
                    }
                }
                break;
            #endregion

            #region
            case "aboveground":
                
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //submergeSoundEffect.Play();
                    //digSoundEffect.Play();
                    underground = true;
                    switchState = "diveDown";
                    //GetComponent<SpriteRenderer>().sprite = belowGroundSpr;
                    if (Closest)
                    {
                        Closest.GetComponent<holeStateScript>().occupied = false;
                    }
                    
                    Closest = null;
                }

                //if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                if(Input.GetKeyDown(KeyCode.A))
                {
                    
                    attackAnimIter = 0;
                    switchState = "attacking";



                }

                break;
            #endregion

            #region
            case "stunned":

                if (!hurtSoundPlayed)
                {
                    hitSoundEffect.PlayOneShot(moleHurt);
                    hurtSoundPlayed = true;
                }
           
                transform.localScale = new Vector3(1f, 1f, 1f);
                stunHead.gameObject.SetActive(true);
                GetComponent<SpriteRenderer>().sprite = aboveGroundSpr;
                stunTimer += Time.deltaTime;
                if(stunTimer > 2f)
                {
                    switchState = "aboveground";
                    stunHead.gameObject.SetActive(false);
                    stunTimer = 0;
                    hurtSoundPlayed = false;
                }
                break;
            #endregion

            #region
            case "attacking":

                if (!spinSoundPlayed)
                {
                
                    hitSoundEffect.PlayOneShot(moleSwing);
                    spinSoundPlayed = true;
                }

                if (attackAnimIter < attackAnim.Length-.5f)
                {
                    attackAnimIter += Time.deltaTime * 30;
                    GetComponent<SpriteRenderer>().sprite = attackAnim[(int)(attackAnimIter-.5f)];
                    transform.localScale = new Vector3(2.5f,2.5f,2.5f);
                }
                else
                {
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    hitSoundEffect.Stop();
                    spinSoundPlayed = false;
                    if (Input.GetKey(KeyCode.Space))
                    {
                        //GetComponent<SpriteRenderer>().sprite = belowGroundSpr;
                        switchState = "diveDown";
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sprite = aboveGroundSpr;
                        switchState = "aboveground";
                    }
                }
                break;

            #endregion

            #region
            case "diveDown":
                if(!diveDownPlayed)
                {
                    submergeSoundEffect.Play();
                    digSoundEffect.Play();
                    diveDownPlayed = true;
                }
                
                if (diveDownAnimIter < diveDownAnim.Length - .5f)
                {
                    diveDownAnimIter += Time.deltaTime * 24;
                    GetComponent<SpriteRenderer>().sprite = diveDownAnim[(int)(diveDownAnimIter-.5f)];
                    transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                }
                else
                {
                    diveDownAnimIter = 0f;
                    diveDownPlayed = false;
                    if (Input.GetKey(KeyCode.Space))
                    {
                        GetComponent<SpriteRenderer>().sprite = belowGroundSpr;
                        switchState = "underground";
                        if (Closest)
                        {
                            Closest.GetComponent<holeStateScript>().occupied = false;
                        }
                        
                        Closest = null; 
                    }
                    else
                    {
                        //GetComponent<SpriteRenderer>().sprite = aboveGroundSpr;
                        switchState = "diveUp";
                        //switchState = "underground";
                    }
                }
                
                break;
            #endregion

            #region
            case "diveUp":
                //If doing rising action
                if (diveUpAnimIter < diveUpAnim.Length - .5f)
                {
                    diveUpAnimIter += Time.deltaTime * 24;
                    GetComponent<SpriteRenderer>().sprite = diveUpAnim[(int)(diveUpAnimIter-.5f)];
                    transform.localScale = new Vector3(1, 1, 1);
                    //Debug.Log(diveUpAnimIter);
                }
                else
                {
                    diveUpAnimIter = 0f;
                    if (Input.GetKey(KeyCode.Space))
                    {
                        //GetComponent<SpriteRenderer>().sprite = belowGroundSpr;
                        switchState = "diveDown";
                        if (Closest)
                        {
                            Closest.GetComponent<holeStateScript>().occupied = false;
                        }
                        Closest = null;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sprite = aboveGroundSpr;
                        switchState = "aboveground";
                    }
                }
                
                break;
                #endregion
        }

        if(GetComponent<SpriteRenderer>().sprite == aboveGroundSpr)
        {
            isAbove = true;
        }
        else
        {
            isAbove = false;
        }
        
        /*if(particleDirection.x == 1)
        {
            partSys.startRotation = Quaternion.Angle(Quaternion.Euler(particleDirection), Quaternion.Euler(-Vector2.up));
        }
        else
        {
            partSys.startRotation = Quaternion.Angle(Quaternion.Euler(particleDirection), Quaternion.Euler(Vector2.up));
        }
        */

        positionSet.z = -3.9f;
        positionSet.x = Mathf.Clamp(positionSet.x,-4.5f,3.7f);
        positionSet.y = Mathf.Clamp(positionSet.y,0.5f,4.6f);
        transform.position = positionSet;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            
        }
        if (paused)
        {
            pauseScreenCanvas.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseScreenCanvas.SetActive(false);
            Time.timeScale = 1;
        }

    }

    float GetDistance(GameObject other)
    {
        return Vector2.Distance(transform.position, other.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && (switchState == "aboveground" || switchState == "attacking") && GetDistance(collision.gameObject) < .05f)
        {
            Debug.Log("COLLISION");
            if (!collision.gameObject.GetComponent<enemyAI>().stunned)
            {
                switchState = "stunned";
                Destroy(stockPanel.transform.GetChild(0).gameObject);
                
            }
            
        }
        else if (collision.gameObject.tag == "EnemyAggro" && (switchState == "aboveground" || switchState == "attacking") && GetDistance(collision.gameObject) < .05f)
        {
            Debug.Log("COLLISION");
            if (!collision.gameObject.GetComponent<enemyTargetAI>().stunned)
            {
                switchState = "stunned";
                Destroy(stockPanel.transform.GetChild(0).gameObject);
            }
        }
        //Debug.Log(stockPanel.transform.childCount-1);
        if (stockPanel.transform.childCount-1 == 0)
        {
            //gameover
            Enemy1.SetActive(false);
            Enemy2.SetActive(false);
            endScreenCanvas.SetActive(true);
            getReadyCanvas.SetActive(false);
            //gameObject.SetActive(false);
            //Time.timeScale = 0f;
        }

        if ((collision.gameObject.tag == "EnemyAggro") && switchState == "attacking" && GetDistance(collision.gameObject) > .05f)
        {
            collision.gameObject.GetComponent<enemyTargetAI>().stunned = true;
            score += 100;
            ScoreDisplayObj.GetComponent<TMP_Text>().text = score.ToString();
            emergeSoundEffect.PlayOneShot(hitHammer);
        }

        if ((collision.gameObject.tag == "Enemy") && switchState == "attacking" && GetDistance(collision.gameObject) > .05f)
        {
            collision.gameObject.GetComponent<enemyAI>().stunned = true;
            score += 100;
            ScoreDisplayObj.GetComponent<TMP_Text>().text = score.ToString();
            emergeSoundEffect.PlayOneShot(hitHammer);
        }
    }
}
