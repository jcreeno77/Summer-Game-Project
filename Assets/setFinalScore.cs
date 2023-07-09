using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class setFinalScore : MonoBehaviour
{
    [SerializeField] GameObject scoreDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMP_Text>().text = "Score: " + scoreDisplay.GetComponent<TMP_Text>().text;
    }
}
