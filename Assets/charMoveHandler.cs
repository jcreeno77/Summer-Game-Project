using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charMoveHandler : MonoBehaviour
{
    [SerializeField] GameObject[] HoleList;
    [SerializeField] Sprite aboveGroundSpr;
    [SerializeField] Sprite belowGroundSpr;
    GameObject Closest;
    Vector3 positionSet;
    int[,] moveGrid;
    Dictionary<GameObject, int>[] dictArr;
    bool underground = false;
    // Start is called before the first frame update
    void Start()
    {
        //boardWidth = HoleList.Length / 2;
        //HoleList = new GameObject[2,2];
        positionSet.z = -3.7f;
        moveGrid = new int[3, 3];
        for (int i = 0; i < moveGrid.Length; i++)
        {
            Debug.Log(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (underground)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                positionSet.y += 3 * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                positionSet.y -= 3 * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                positionSet.x -= 3 * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                positionSet.x += 3 * Time.deltaTime;
            }
            Color tmp = Color.white;
            tmp.a = 1f;
            GetComponent<SpriteRenderer>().color = tmp;
        }
        else
        {
            if (Closest)
            {
                if (GetDistance(Closest) > .3f)
                {
                    positionSet = Vector3.Lerp(positionSet, Closest.transform.position, 55f * Time.deltaTime);
                }
                else
                {
                    positionSet = Closest.transform.position;
                    GetComponent<SpriteRenderer>().sprite = aboveGroundSpr;
                }
            }
            
        }
        
        //go below ground and start moving by pressing space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            underground = true;
            GetComponent<SpriteRenderer>().sprite = belowGroundSpr;
        }
        
        //Go above ground by releasing space
        if(Input.GetKeyUp(KeyCode.Space))
        {
            underground = false;
            Closest = HoleList[0];
            for(int i = 0; i < HoleList.Length; i++)
            {

                if(GetDistance(HoleList[i]) < GetDistance(Closest))
                {
                    Closest = HoleList[i];
                }
            }
            //positionSet = Closest.transform.position;
            //GetComponent<SpriteRenderer>().sprite = aboveGroundSpr;
        }
        positionSet.z = -3.7f;
        transform.position = positionSet;
    }

    float GetDistance(GameObject other)
    {
        return Vector2.Distance(transform.position, other.transform.position);
    }
}
