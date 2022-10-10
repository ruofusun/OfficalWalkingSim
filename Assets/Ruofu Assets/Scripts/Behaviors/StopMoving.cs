using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class StopMoving : Action
{
    Rigidbody rigidbody;
    TaskStatus status = TaskStatus.Running;

    public override void OnStart()
    {
        
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector2.zero;
        status = TaskStatus.Success;
        
    }

    public override TaskStatus OnUpdate()
    {
        return status;
    }
}
