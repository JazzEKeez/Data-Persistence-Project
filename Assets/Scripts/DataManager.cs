using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    
   
    public string playerName;
    public int highScore;
    public List<HighScore> highScores;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    public void SaveHighScores()
    {
        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("HighScores", json);
        PlayerPrefs.Save();
    }

    public void LoadHighScores()
    {
        if (PlayerPrefs.HasKey("HighScores"))
        {
            string json = PlayerPrefs.GetString("HighScores");
            highScores = JsonUtility.FromJson<List<HighScore>>(json);
        }
        else
        {
            highScores = new List<HighScore>();
        }
    }

    public void SortHighScores()
    {
        highScores.Sort((a, b) => b.highScore.CompareTo(a.highScore));
    }

    public void AddHighScore(string playerName, int score)
    {
        HighScore newEntry = new HighScore
        {
            playerName = playerName,
            highScore = score
        };

        //Check if highScores list is empty or score is higher than lowest score on highScores list
        if(highScores.Count == 0 || score > highScores[highScores.Count - 1].highScore)
        {
            // Add new entry to the end of the list
            highScores.Add(newEntry);
        }
        else
        {
            // Find the appropriate index for new entry
            for(int i = 0; i < highScores.Count; i++)
            {
                if (score > highScores[i].highScore)
                {
                    // insert new score to list above lower scores
                    highScores.Insert(i, newEntry);
                    break;
                }

            }
        }
        
        // when highScores cout exceeds 5 entries
        while (highScores.Count > 5)
        {
            // Remove the lowest entry from the list
            highScores.RemoveAt(highScores.Count - 1);
        }
    }
}
[System.Serializable]
public class HighScore
{
    public string playerName;
    public int highScore;
}
