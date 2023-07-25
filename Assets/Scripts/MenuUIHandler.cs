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

    public void ExitApplication()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
