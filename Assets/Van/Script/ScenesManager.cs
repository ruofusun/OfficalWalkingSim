using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public Animator levelAni;
    public bool isScene1;

    private void Start()
    {
        StartScene1();
    }

    public void LoadScene()
    {
        levelAni.SetTrigger("isFadeOut");
    }

    public void StartScene1()
    {
        //≤•∑≈ƒ÷¡Â…˘
        SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.clockAlarm,true);
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update() 
    {
        
    }
}
