using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class LoadAfraidContent : Action
{
    private MoodCanvasController moodCanvasController;
    //  private EnemyController enemy;
    //   SpriteRenderer sp;

    TaskStatus status = TaskStatus.Running;
    //  public SharedTransform target;
    public override void OnStart()
    {

        AnimalController animal = GetComponent<AnimalController>();
        if (animal)
        {
            moodCanvasController = animal.MoodCanvasController;
        }
        else
        {
            moodCanvasController = GetComponent<CrowController>().MoodCanvasController;
        }
        

        //  sp = GetComponent<SpriteRenderer>();
        //   enemy =  GetComponent<EnemyController>();
        if (moodCanvasController )
        {
            moodCanvasController.LoadAfraidContent();
            status = TaskStatus.Success;
        }
        else 
        {
            status = TaskStatus.Failure;
        }
    }


    public override TaskStatus OnUpdate()
    {
        return status;
    }
}
