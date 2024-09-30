using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ADPlay : MonoBehaviour
{

    void Start()
    {
        Ad();
    }


    void Update()
    {
        
    }

    public void DelayAD()
    {
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Ad()
    {
        YandexApi.Init(() =>
        {
            YandexApi.GetPlayer(() =>
            {
                Mute();
                YandexApi.ShowFullscreenAdv(() =>
                { Unmute(); }, (string error) =>
                { Unmute(); });
                var a = YandexApi.DeviceInfo();
                Debug.Log(a);
                var playerInfo = YandexApi.GetPlayerInfo();
                Debug.Log(JsonUtility.ToJson(playerInfo));
            }, (error) => Debug.Log(error));
        }, (error) => Debug.Log(error));
    }

    public void AdPlay()
    {
        YandexApi.ShowFullscreenAdv();
    }

    public void Mute()
    {
        AudioListener.volume = 0f;
    }

    public void Unmute()
    {
        AudioListener.volume = 1f;
    }
}
