using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject SoundPrefab;


    public static SoundManager Instance;

    [Header("Loops and Background")] public AudioClip backgroundMusic;
    [Range(0f, 1f)] public float musicVolume = 1.0f;

    #region Scene1
    public AudioClip scene1Bgm;//白噪音 风声
    public AudioClip patatoHarvest;
    public AudioClip patatoCrash;
    public AudioClip patatoDeliver;
    public AudioClip walkingstepOnRoad;
    public AudioClip walkingstepOnFarm;
    public AudioClip clockAlarm;

    #endregion

    #region Scene2
    public AudioClip scene2Bgm;//轻松惬意的游乐风
    public AudioClip chicken1;
    public AudioClip chicken2;
    public AudioClip pickUpItem;
    public AudioClip eat;
    public AudioClip whistle;
    #endregion

    #region General
    #endregion

    public AudioSource musicSource;
    public AudioSource dangerSource;

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
        musicSource.clip = backgroundMusic;
        musicSource.volume = musicVolume;
        musicSource.Play();

    }

    public void PlaySoundEffect(AudioClip clipToPlay)
    {
        if (clipToPlay == null)
        {
            Debug.Log("AUDIO CLIP NOT ASSIGNED ON AUDIO DIRECTOR!");
            return;
        }

        GameObject newSound = Instantiate(SoundPrefab, Vector3.zero, Quaternion.identity);
        AudioSource newSoundSource = newSound.GetComponent<AudioSource>();
    }
}
