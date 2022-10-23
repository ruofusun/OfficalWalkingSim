using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using UnityEngine;

public class StorePlayerPosition : Action
{

    public SharedVector3 storePlayerPos;

    TaskStatus status = TaskStatus.Running;

    private PlayerController player;
    //  public SharedTransform target;
    public override void OnStart()
    {
        player = Global.GetPlayer();
 
        //  sp = GetComponent<SpriteRenderer>();
        //   enemy =  GetComponent<EnemyController>();
        if (player )
        {
            storePlayerPos.Value = player.transform.position;
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
