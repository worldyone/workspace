using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // シングルトン
    public static SoundManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public AudioSource audioSourceBGM; // BGMのスピーカー
    public AudioClip[] audioClipsBGM; // BGMの音源(0: Title, 1:Town, 2:Quest, 3:Battle)
    public AudioSource audioSourceSE; // SEのスピーカー
    public AudioClip audioClip; // 音源

    public void PlayBGM(string sceneName)
    {
        audioSourceBGM.Stop();

        switch (sceneName)
        {
            case "Title":
                audioSourceBGM.clip = audioClipsBGM[0];
                break;
            case "Town":
                audioSourceBGM.clip = audioClipsBGM[1];
                break;
            case "Quest":
                audioSourceBGM.clip = audioClipsBGM[2];
                break;
            case "Battle":
                audioSourceBGM.clip = audioClipsBGM[3];
                break;
        }
        audioSourceBGM.Play();
    }
    public void PlaySE()
    {
        audioSourceSE.PlayOneShot(audioClip);
    }

}
