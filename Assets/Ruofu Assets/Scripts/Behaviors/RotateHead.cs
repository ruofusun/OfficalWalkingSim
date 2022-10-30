using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;

public class RotateHead : Action
{
    


    Rigidbody rb;

    public float minAngle = -90f;

    public float maxAngle = 90f;
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
            float yEuler = rb.rotation.eulerAngles.y;
            rb.DORotate(new Vector3(0, Random.Range(yEuler+minAngle, yEuler+maxAngle), 0), 1f);
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
