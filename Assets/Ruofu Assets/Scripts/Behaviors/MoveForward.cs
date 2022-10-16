using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class MoveForward : Action
{

    public SharedFloat speed;


    Rigidbody rb;
  //  private EnemyController enemy;
    //   SpriteRenderer sp;

    TaskStatus status = TaskStatus.Running;

    public SharedFloat timer = 2f;
    //  public SharedTransform target;
    public override void OnStart()
    {
        status = TaskStatus.Running;
        rb = GetComponent<Rigidbody>();
        cd = 0;
        //  sp = GetComponent<SpriteRenderer>();
     //   enemy =  GetComponent<EnemyController>();
        if (rb )
        {
            rb.velocity = transform.forward * speed.Value;

              //  status = TaskStatus.Success;
        }
        else 
        {
            status = TaskStatus.Failure;
        }
    }

    private float cd = 0;
    public override TaskStatus OnUpdate()
    {
        cd += Time.deltaTime;
        if (cd > timer.Value)
        {
            rb.velocity = transform.forward * speed.Value;
            return TaskStatus.Success;
            cd = 0;
        }

        rb.velocity = transform.forward * speed.Value;
        return TaskStatus.Running;
    }
}
