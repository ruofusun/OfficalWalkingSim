using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public Animator levelAni;
    public bool isScene1;
    public bool isPause = false;

    public GameObject pauseCanvas;

    public static ScenesManager Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        //Set the appropriate clips and volume on music and danger loop, then play

    }
    private void Start()
    {
        if (isScene1)
        {
            StartScene1();
        }
    }

    public void LoadScene()
    {
        levelAni.SetTrigger("isFadeOut");
    }

    public void StartScene1()
    {
        //²¥·ÅÄÖÁåÉù
        SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.clockAlarm,true);
    }

    public void OnFadeComplete()
    {
        LoadScene1();
    }

    public void LoadScene0()
    {
        SceneManager.LoadScene(0);

    }

    public void LoadScene1()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadScene0();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadScene1();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
            pauseCanvas.SetActive(isPause);
        }
    }
}
