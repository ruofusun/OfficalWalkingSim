using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class CrowFlyAway : Action
{
    public SharedVector3 target;
    private Rigidbody rigidbody;
    public float Offset = 0.25f;
    public SharedFloat Speed = 2f;
    private CrowController crow;



    public override void OnStart()
    {
        rigidbody = GetComponent<Rigidbody>();
        crow = GetComponent<CrowController>();


    }

    private Vector3 offset;

    public override TaskStatus OnUpdate()
    {
        if (target == null)
        {
            return TaskStatus.Failure;
        }
        
        if(crow.target)
        crow.target.GetComponent<BehaviorTree>().SendEvent<object>("rescue",5);

        offset = target.Value - transform.position;
        crow.target = null;

        if (offset.sqrMagnitude < Offset)
        {
            gameObject.SetActive(true);
            return TaskStatus.Success;
        }

        //   StartCoroutine(RotateAndMove());
        rigidbody.velocity = offset.normalized * Speed.Value;
        var _direction = (target.Value - transform.position).normalized;
        var _lookRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 2f);
        //  Debug.Log(offset.magnitude + animal.TargetFood.name);
        // rigidbody.velocity = offset.normalized * Speed.Value;
        return TaskStatus.Running;
    }
}
