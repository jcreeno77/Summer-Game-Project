using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charMoveHandler : MonoBehaviour
{
    [SerializeField] GameObject[] HoleList;
    [SerializeField] Sprite aboveGroundSpr;
    [SerializeField] Sprite belowGroundSpr;
    ParticleSystem partSys;
    GameObject Closest;

    Vector3 positionSet;
    Vector2 particleDirection;

    public string switchState;
    bool underground = false;
    public bool isAbove;

    float stunTimer;
    // Start is called before the first frame update
    void Start()
    {
        stunTimer = 0f;
        switchState = "aboveground";
        partSys = GetComponent<ParticleSystem>();
        //boardWidth = HoleList.Length / 2;
        //HoleList = new GameObject[2,2];
        positionSet.z = -3.7f;
        particleDirection.x = 0;
        particleDirection.y = 0;
    }

    // Update is called once per frame
    void Update()
    {

        switch (switchState)
        {
            case "underground":
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

                Color tmp = Color.white;
                tmp.a = 1f;
                GetComponent<SpriteRenderer>().color = tmp;

                if (Input.GetKeyUp(KeyCode.Space))
                {
                    
                    
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
                        positionSet = Vector3.Lerp(positionSet, Closest.transform.position, 55f * Time.deltaTime);
                    }
                    else
                    {
                        positionSet = Closest.transform.position;
                        GetComponent<SpriteRenderer>().sprite = aboveGroundSpr;
                        underground = false;
                        switchState = "aboveground";
                        Closest.GetComponent<holeStateScript>().stateIter -= 1;
                    }
                }
                break;

            case "aboveground":

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    underground = true;
                    switchState = "underground";
                    GetComponent<SpriteRenderer>().sprite = belowGroundSpr;
                    Closest.GetComponent<holeStateScript>().occupied = false;
                    Closest = null;
                }
                break;

            case "stunned":
                GetComponent<SpriteRenderer>().color = Color.red;
                stunTimer += Time.deltaTime;
                if(stunTimer > 2f)
                {
                    switchState = "aboveground";
                    GetComponent<SpriteRenderer>().color = Color.white;
                    stunTimer = 0;
                }
                break;
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

        positionSet.z = -3.7f;
        transform.position = positionSet;
    }

    float GetDistance(GameObject other)
    {
        return Vector2.Distance(transform.position, other.transform.position);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && switchState == "aboveground")
        {
            switchState = "stunned";
        }
    }
}
