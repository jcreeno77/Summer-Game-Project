using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScoreEntry
{
    public int rank;
    public string name;
    public int score;
}


public class HighScoreManager : MonoBehaviour
{
    public List<HighScoreEntry> highScores = new List<HighScoreEntry>();

    // Call this function to add a new score entry
    public void AddHighScoreEntry(string name, int score)
    {
        // create new entry
        HighScoreEntry highScoreEntry = new HighScoreEntry();
        highScoreEntry.name = name;
        highScoreEntry.score = score;

        // load saved high scores
        LoadHighScores();

        // add new entry
        highScores.Add(highScoreEntry);

        // sort entries by score in descending order
        highScores.Sort((entry1, entry2) => entry2.score - entry1.score);

        // update ranks and remove extra entries to keep only top 10
        for (int i = 0; i < highScores.Count && i < 10; i++)
        {
            highScores[i].rank = i + 1;
        }

        if (highScores.Count > 10)
        {
            highScores.RemoveRange(10, highScores.Count - 10);
        }

        // save updated high scores
        SaveHighScores();
    }


    // Save high scores to PlayerPrefs
    private void SaveHighScores()
    {
        // convert the high score list to a JSON string
        string json = JsonUtility.ToJson(new HighScores { highScoreEntryList = highScores });

        // save the JSON string to PlayerPrefs
        PlayerPrefs.SetString("HighScores", json);
        PlayerPrefs.Save();
    }

    // Load high scores from PlayerPrefs
    public void LoadHighScores()
    {
        // load the JSON string from PlayerPrefs
        string json = PlayerPrefs.GetString("HighScores", "");

        // if there is a saved high score list
        if (!string.IsNullOrEmpty(json))
        {
            // convert the JSON string back to a high score list
            highScores = JsonUtility.FromJson<HighScores>(json).highScoreEntryList;
        }
        else
        {
            highScores = new List<HighScoreEntry>();
        }
    }

    public void ClearHighScores()
    {
        PlayerPrefs.DeleteKey("HighScores");
        PlayerPrefs.Save();
    }

    public bool IsTopScore(int score)
    {
        // load saved high scores
        LoadHighScores();

        // if there are less than 10 scores, this score is automatically in the top 10
        if (highScores.Count < 10) return true;

        // otherwise, compare the score to the last entry in the sorted list
        return score > highScores[9].score;
    }

}

[System.Serializable]
public class HighScores
{
    public List<HighScoreEntry> highScoreEntryList;
}
