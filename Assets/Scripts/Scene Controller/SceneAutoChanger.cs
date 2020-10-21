using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAutoChanger : MonoBehaviour
{
    public float timeToLoad;
    private float timeElapsed;
    public string sceneToLoad;

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > timeToLoad)
            SceneManager.LoadScene(sceneToLoad);
    }
}
