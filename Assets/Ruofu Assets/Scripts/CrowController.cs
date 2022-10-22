using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class CrowController : MonoBehaviour
{

    public AnimalController target;
    private BehaviorTree behaviorTree;

    private MoodCanvasController moodCanvasController;
    
    // Start is called before the first frame update
    void Start()
    {
        behaviorTree = GetComponent<BehaviorTree>();
        behaviorTree.SetVariableValue("Origin", transform.position);
        moodCanvasController = GetComponentInChildren<MoodCanvasController>();
        
    }

    public MoodCanvasController MoodCanvasController
    {
        get => moodCanvasController;
        set => moodCanvasController = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAnimalTarget(AnimalController animal)
    {
        target = animal;
    }

    private void OnTriggerEnter(Collider other)
    {
        FoodController food = other.gameObject.GetComponent<FoodController>();
        if (food&& food.GetComponent<Rigidbody>().isKinematic==false)
        {
            Debug.Log(" hit crow");

            if ( !food.NeedPickUp)
            {
                behaviorTree.SendEvent<object>("hit",5);
            }
        }
    }
}
