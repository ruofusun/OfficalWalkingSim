using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class IsInTheFarm : Conditional
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

        if (animal.Inthefarm)
        {
            status = TaskStatus.Success;
        }
        else
        {
            status = TaskStatus.Failure;
        }

        return status;

    }
}
