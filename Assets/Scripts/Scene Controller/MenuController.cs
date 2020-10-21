using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject QuitButton;
    public GameObject PlayButton;
    public GameObject CreditsButton;
    public GameObject HowToButton;

    public void Awake()
    {
        PlayButton = GameObject.Find("Button - Level 1");
        QuitButton = GameObject.Find("Button - Level 1");
        CreditsButton = GameObject.Find("Button - Quit");
        HowToButton = GameObject.Find("HowToPlayButton");

    }

    public void playButtonClicked()
    {
        SceneChanger.Instance.LoadLevel1();
    }

    public void quitButtonClicked()
    {
        SceneChanger.Instance.Quit();
    }

    public void creditsButtonClicked()
    {
        SceneChanger.Instance.LoadCredits();
    }

    public void howToButtonClicked()
    {
        SceneChanger.Instance.LoadHowTo();
    }
}
