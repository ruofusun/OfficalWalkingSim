using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicTrigger : MonoBehaviour
{
    private bool musicTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() && !musicTriggered)
        {
            SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.scene2Bgm,true,true);
            musicTriggered = true;
        }
    }
}
