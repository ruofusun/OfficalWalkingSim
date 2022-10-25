using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class MoveAwayFromPlayer : Action
{

    private Rigidbody rigidbody;

    public SharedFloat Speed = 3f;

    public SharedFloat timer =5f;
    private float cd;
    private PlayerController player;



    public override void OnStart()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = Global.GetPlayer();
        cd = 0;
    }


    public override TaskStatus OnUpdate()
    {
        Vector3 direction = transform.position - player.transform.position;
        direction.y = 0;
        
     
        cd += Time.deltaTime;
        if (cd > timer.Value)
        {
            cd = 0;
            return TaskStatus.Success;
        }

        //   StartCoroutine(RotateAndMove());
     
        rigidbody.velocity = direction.normalized * Speed.Value;
       

        
        var  _lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 2f);

        //  Debug.Log(offset.magnitude + animal.TargetFood.name);
        // rigidbody.velocity = offset.normalized * Speed.Value;

        return TaskStatus.Running;
    }

}
