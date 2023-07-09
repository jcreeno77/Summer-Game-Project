using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MenuButtonColorChanger : MonoBehaviour
{

    public Button button;
    public TextMeshProUGUI buttonText;
    public Color selectedColor;
    public Color unselectedColor;

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == button)
        {
            buttonText.color = selectedColor;
            Debug.Log("A Button is selected");
        }
        else
        {
            buttonText.color = unselectedColor;
        }
    }

}
