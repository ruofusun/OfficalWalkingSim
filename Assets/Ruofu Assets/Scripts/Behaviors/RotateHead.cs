using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;

public class RotateHead : Action
{
    


    Rigidbody rb;
    //  private EnemyController enemy;
    //   SpriteRenderer sp;

    TaskStatus status = TaskStatus.Running;
    //  public SharedTransform target;
    public override void OnStart()
    {
        status = TaskStatus.Running;
        rb = GetComponent<Rigidbody>();
        //  sp = GetComponent<SpriteRenderer>();
        //   enemy =  GetComponent<EnemyController>();
        if (rb )
        {
            rb.DORotate(new Vector3(0, Random.Range(-90f, 90f), 0), 1f);
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
