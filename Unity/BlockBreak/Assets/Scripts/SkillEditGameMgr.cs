using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillEditGameMgr : MonoBehaviour
{
    long frame;
    private GameObject propertyUI;

    // Start is called before the first frame update
    void Start()
    {
        frame = 0;
        propertyUI = GameObject.Find("PropertyUI");
        propertyUI.SetActive(false);
        Debug.Log(propertyUI);
    }

    // Update is called once per frame
    void Update()
    {
        frame += 1;
        if (frame % 60 == 0)
            Debug.Log(frame / 60);

        if (Input.anyKeyDown && !Input.GetKeyDown("q"))
        {
            //　ポーズUIのアクティブ、非アクティブを切り替え
            propertyUI.SetActive(!propertyUI.activeSelf);

            //　ポーズUIが表示されてる時は停止
            if (propertyUI.activeSelf)
            {
                Time.timeScale = 0f;
                //　ポーズUIが表示されてなければ通常通り進行
            }
            else
            {
                Time.timeScale = 1f;
            }
        }

        if (Input.GetKeyDown("q"))
        {
            SceneManager.LoadScene("Start");
        }

    }
}
