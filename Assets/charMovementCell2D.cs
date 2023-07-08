using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charMovementCell2D : MonoBehaviour
{
    int boardWidth = 3;
    int boardHeight = 3;
    Vector2 startPos;
    Vector3 positionSet;
    // Start is called before the first frame update
    void Start()
    {
        positionSet = new Vector3(0,0,0);

        if(boardWidth % 2 == 0)
        {
            startPos.x = boardWidth / 2;
        }
        else
        {
            startPos.x = (boardWidth+1) / 2;
        }

        if(boardHeight % 2 == 0)
        {
            startPos.y = (boardHeight) / 2;
        }
        else
        {
            startPos.y = (boardHeight + 1) / 2;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(startPos.y + 1 <= boardHeight)
            {
                positionSet.y += 1;
                startPos.y += 1;
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (startPos.y - 1 >= 1)
            {
                positionSet.y -= 1;
                startPos.y -= 1;
            }
                
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (startPos.x - 1 >= 1)
            {
                positionSet.x -= 1;
                startPos.x -= 1;
            }
                
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(startPos.x +1 <= boardWidth)
            {
                positionSet.x += 1;
                startPos.x += 1;
            }
            
        }


        transform.position = positionSet;
    }
}
