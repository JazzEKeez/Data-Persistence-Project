using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public DataManager dataManager;
    public TMP_InputField nameInput;
    public TextMeshProUGUI highScoreListText;
    public string playerName;

    public void Awake()
    {
        dataManager = GameObject.Find("Data Manager").GetComponent<DataManager>();
    }
    public void SetName()
    {
        dataManager.playerName = nameInput.text;
    }
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void UpdateHighScoreListText()
    {
        string formattedText = "High Scores: \n";

        for(int i = 0; i < dataManager.highScores.Count; i++)
        {
            formattedText += $"{i + 1}. {dataManager.highScores[i].playerName}: {dataManager.highScores[i].highScore}\n";
        }

        highScoreListText.text = formattedText;
    }
    public void ExitApplication()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
