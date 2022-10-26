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

    public AudioClip scene1Bgm; //°×ÔëÒô ·çÉù
    public AudioClip patatoHarvest;
    public AudioClip patatoCrash;
    public AudioClip patatoDeliver;
    public AudioClip walkingstepOnRoad;
    public AudioClip walkingstepOnFarm;
    public AudioClip clockAlarm;

    #endregion

    #region Scene2

    public AudioClip scene2Bgm; //ÇáËÉã«ÒâµÄÓÎÀÖ·ç
    public AudioClip chicken1;
    public AudioClip chicken2;
    public AudioClip pickUpItem;
    public AudioClip eat;
    public AudioClip whistle;
    public AudioClip chickenThrow;
    public AudioClip chikcenFear;
    public AudioClip sheepFear;
    public AudioClip cowFear;
    public AudioClip drop;
    public AudioClip generalthrow;
    public AudioClip danger;
    public AudioClip crowSound;

    #endregion 

    #region General

    #endregion

    public AudioSource musicSource;
    public AudioSource dangerSource;
    public AudioSource loopSESource;

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

    private AudioSource fadeInAudioSource;
    public void PlaySoundEffect(AudioClip clipToPlay, bool isLoop = false, bool fadeIn = false, float volume  =1)
    {
        if (clipToPlay == null)
        {
            Debug.Log("AUDIO CLIP NOT ASSIGNED ON AUDIO DIRECTOR!");
            return;
        }

        GameObject newSound = Instantiate(SoundPrefab, Vector3.zero, Quaternion.identity);
        AudioSource newSoundSource = newSound.GetComponent<AudioSource>();
        newSoundSource.loop = isLoop;
        newSoundSource.clip = clipToPlay;
        newSoundSource.volume = volume;

        if (!fadeIn)
        {
            newSoundSource.Play();
        }
        

        if (fadeIn)
        {
            if (fadeInAudioSource !=null && fadeInAudioSource.clip != clipToPlay)
            {
                Debug.Log("fade in clip" + fadeInAudioSource.clip + "clip to play" + clipToPlay);
                FadeOutAudio(fadeInAudioSource, 3);
                FadeInAudio(newSoundSource, 0.4f, 7);
                newSoundSource.Play();
            }else if (fadeInAudioSource == null)
            {
                FadeInAudio(newSoundSource, 0.4f, 7);
                newSoundSource.Play();
            }
            else
            {
               newSoundSource.gameObject.SetActive(false);
               newSoundSource = null;
            }

            // FadeInAudio(newSoundSource, 0.4f, 7);
            if(newSoundSource)
            fadeInAudioSource = newSoundSource;
        }


        if (!isLoop)
        {
            Destroy(newSound, clipToPlay.length);
        }
        else
        {
            loopSESource = newSoundSource;
        }
    }


    public void FadeInAudio(AudioSource source, float destVolume, float timeToFade)
    {
        Debug.Log("fading in audio");
        StartCoroutine(LinearFadeIn(source, destVolume, timeToFade));
    }

    public void FadeOutAudio(AudioSource source, float timeToFade)
    {
        Debug.Log("fade out" + source.clip);
        StartCoroutine(LinearFadeOut(source, timeToFade));
    }

    public IEnumerator LinearFadeIn(AudioSource audioSource, float destVol, float time)
    {
        while (audioSource.volume < destVol)
        {
            audioSource.volume += Time.deltaTime / time;
            yield return null;
        }
    }

    public IEnumerator LinearFadeOut(AudioSource audioSource, float time)
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= Time.deltaTime / time;
            yield return null;
        }
    }
}
