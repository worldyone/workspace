using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuButton : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene("Start");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
