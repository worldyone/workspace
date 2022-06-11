using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneDirector : MonoBehaviour
{
    static public int PlayerNum = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void VsCPU()
    {
        PlayerNum = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void VsPlayer()
    {
        PlayerNum = 2;
        SceneManager.LoadScene("SampleScene");
    }
}
