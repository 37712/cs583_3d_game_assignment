using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    public GameObject MenuButton;

    public void Awake()
    {
        MenuButton = GameObject.Find("MenuButton");
    }

    public void menuButtonClicked()
    {
        SceneChanger.Instance.LoadMainMenu();
    }
}
