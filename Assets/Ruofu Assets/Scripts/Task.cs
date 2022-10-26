using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using DG.Tweening;
using UnityEngine;

public class Task : MonoBehaviour
{
    public int amountRequired = 1;

    public string animalTag = "Chicken";

    private bool taskFinished = false;
    private int currentAmount = 0;
    public List<Transform> positions;

    private TaskManager _taskManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _taskManager = FindObjectOfType<TaskManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == animalTag)
        {
            AnimalController animal = other.GetComponent<AnimalController>();
            if (animal)
            {
                if (animal.transform.parent && animal.transform.parent.tag == "Player")
                {
                    animal.transform.parent = null;
                }

             
               // animal.Inthefarm = true;  
                animal.GetComponent<BehaviorTree>().enabled = false;
                Rigidbody rb = animal.GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;
                float distance = Vector3.Distance(animal.transform.position, positions[currentAmount].position);
                
                rb.DOMove(positions[currentAmount].position, distance);
                //animal.transform.DOMove(positions[currentAmount].position, 5);
                rb.DORotate(positions[currentAmount].eulerAngles, distance);
                rb.isKinematic = true;
                currentAmount++;
                animal.tag = "Untagged";
                if (currentAmount >= amountRequired)
                {
                    taskFinished = true;
                    //todo: add ui here
                    _taskManager.CheckTaskStatus();

                    Debug.Log("finish one task");
                    currentAmount = 0;
                }

            }
        }
    }


    public bool TaskFinished
    {
        get => taskFinished;
        set => taskFinished = value;
    }
}
