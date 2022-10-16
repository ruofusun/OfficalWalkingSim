using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using UnityEngine;

public class GetNearbyAnimalTarget : Conditional
{
    private AnimalController targetanimal;
    private TaskStatus status = TaskStatus.Running;
    private AnimalManager animalManager;

    public override void OnStart()
    {
        animalManager = Global.GetAnimalManager();
        if (animalManager == null)
            status = TaskStatus.Failure;

 

    }

    public override TaskStatus OnUpdate()
    {
        targetanimal = animalManager.GetOneRandomAnimal();
        if (targetanimal)
        {
            Debug.Log("find a target animal for the crow");
            GetComponent<CrowController>().SetAnimalTarget(targetanimal);
            status = TaskStatus.Success;
        }
        else
        {
            status = TaskStatus.Failure;
        }
        return status;
    }
}
