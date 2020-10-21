using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    //implementing singleton pattern design
    public static SceneChanger instance = null;
    public Unit myPlayer;

    public static SceneChanger Instance
    {
        get { return instance; }
    }

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (instance != this)
                Destroy(gameObject);
        }
        myPlayer = gameObject.AddComponent<Unit>() as Unit;
    }

    // this script is for button pressing scene changes
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadHowTo()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void Quit()
    {
        Debug.Log("user has quit");
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    public void Died()
    {
        SceneManager.LoadScene("Credits");
    }
}
