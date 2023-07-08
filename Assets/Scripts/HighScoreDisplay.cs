using UnityEngine;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    public HighScoreManager highScoreManager;
    public TextMeshProUGUI textMeshPro;

    private void Start()
    {
        highScoreManager.LoadHighScores();

        //hold the high score text
        string highScoreText = "";

        //append each high score to the high score text
        foreach (HighScoreEntry entry in highScoreManager.highScores)
        {
            highScoreText += entry.rank + ". " + entry.name + " " + entry.score + "\n";
        }

        // update the text mesh pro component with the high score text
        textMeshPro.text = highScoreText;
    }

    public void UpdateHighScoreDisplay()
    {
        highScoreManager.LoadHighScores();

        // create a string to hold the high score text
        string highScoreText = "HIGH SCORES\n\n";

        // append each high score to the high score text
        foreach (HighScoreEntry entry in highScoreManager.highScores)
        {
            highScoreText += entry.rank + ". " + entry.name + " " + entry.score + "\n";
        }

        // update the text mesh pro component with the high score text
        textMeshPro.text = highScoreText;
    }

}
