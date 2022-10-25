using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class AnimalControllerForScene1 : MonoBehaviour
{
    
    private BehaviorTree behaviorTree;
    private MoodCanvasController _moodCanvasController;
    
    // Start is called before the first frame update
    void Start()
    {
        behaviorTree = GetComponent<BehaviorTree>();
        _moodCanvasController = GetComponentInChildren<MoodCanvasController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            behaviorTree.SendEvent<object>("afraid",5);
          //  SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.chikcenFear,false);
        }
    }

    public MoodCanvasController MoodCanvasController
    {
        get => _moodCanvasController;
        set => _moodCanvasController = value;
    }
}
