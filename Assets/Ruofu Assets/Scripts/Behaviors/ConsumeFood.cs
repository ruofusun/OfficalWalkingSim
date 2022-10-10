using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using UnityEngine;

public class ConsumeFood : Action
{
    private AnimalController animal;
    private TaskStatus status = TaskStatus.Running;
  
    public override void OnStart()
    {
        animal = GetComponent<AnimalController>();
        if (animal == null)
            status = TaskStatus.Failure;

    }

    public override TaskStatus OnUpdate()
    {

        if (animal.TargetFood)
        {
           animal.ConsumeFood();
             
            status = TaskStatus.Success;
        }
        else
        {
            status = TaskStatus.Failure;
        }

        return status;

    }
}
