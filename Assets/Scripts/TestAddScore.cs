using UnityEngine;
using UnityEngine.UI;
using System;

public class TestAddScore : MonoBehaviour
{
    public HighScoreManager highScoreManager;
    public HighScoreDisplay highScoreDisplay;

    public Button yourButton;
    private System.Random random = new System.Random();

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(AddRandomScore);
    }

    void AddRandomScore()
    {
        string randomName = "Player" + random.Next(1000, 9999);
        int randomScore = random.Next(0, 10000);

        highScoreManager.AddHighScoreEntry(randomName, randomScore);
        highScoreDisplay.RefreshHighScoreDisplay();
    }
}
