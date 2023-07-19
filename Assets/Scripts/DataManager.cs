using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        LoadHighScores();
    }

    public void SaveHighScores()
    {
        string json = JsonUtility.ToJson(highScores);

        Debug.Log("JSON Data to be Saved: " + json);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        Debug.Log("Saved High Scores: ");
        foreach (var score in highScores)
        {
            Debug.Log($"{score.playerName}: {score.highScore}");
        }
    }

    public void LoadHighScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            
            // Debug log the JSON data retrieved from persistentDataPath
            Debug.Log("Json Data:" + json);

            highScores = JsonUtility.FromJson<List<HighScore>>(json);
            
            //Check whether there are high score entries in the Json file
            if (highScores == null || highScores.Count == 0)
            {
                Debug.Log("No high scores found or JSON deserialization issue!");
            }
        }
        else
        {
            highScores = new List<HighScore>();
        }

        Debug.Log("High Scores Loaded: ");
        foreach (var score in highScores)
        {
            Debug.Log($"{score.playerName}: {score.highScore}");
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

        highScores.Add(newEntry);

        SortHighScores();
        
        // when highScores cout exceeds 5 entries
        while (highScores.Count > 5)
        {
            // Remove the lowest entry from the list
            highScores.RemoveAt(highScores.Count - 1);
        }

        //Save list of high scores
        SaveHighScores();
    }
}
[System.Serializable]
public class HighScore
{
    public string playerName;
    public int highScore;

    // Parameterless constructor for deserialization
    public HighScore()
    {
    }
}
