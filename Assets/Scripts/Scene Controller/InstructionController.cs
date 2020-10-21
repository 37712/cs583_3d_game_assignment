using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionController : MonoBehaviour
{
    public GameObject backButton;

    public void Awake()
    {
        backButton = GameObject.Find("BackButton");
    }

    public void backButtonClicked()
    {
        SceneChanger.Instance.LoadMainMenu();
    }
}
