using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class MoveTowardsOriginWithinTimeLimit : Action
{
    public SharedVector3 target;
    private Rigidbody rigidbody;
    public float Offset = 0.25f;
    public SharedFloat Speed = 2f;
    private AnimalController animal;
    public SharedFloat timer = 4f;


    private float cd = 0;
    public override void OnStart()
    {
        rigidbody = GetComponent<Rigidbody>();
        animal = GetComponent<AnimalController>();
        cd = 0;
     

    }

    private Vector3 offset;
    public override TaskStatus OnUpdate()
    {
        cd += Time.deltaTime;
        if (cd > timer.Value)
        {
            return TaskStatus.Success;
            cd = 0;
        }


        offset =  target.Value - transform.position;
        

        if (offset.sqrMagnitude < Offset)
        {
            return TaskStatus.Success;
        }

        //   StartCoroutine(RotateAndMove());
     
        rigidbody.velocity = offset.normalized * Speed.Value;
        var _direction = ( target.Value -transform.position).normalized;
        var  _lookRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 2f);

        //  Debug.Log(offset.magnitude + animal.TargetFood.name);
        // rigidbody.velocity = offset.normalized * Speed.Value;

        return TaskStatus.Running;
    }


    IEnumerator RotateAndMove()
    {
        //find the vector pointing from our position to the target
        var _direction = (animal.TargetFood.transform.position- transform.position).normalized;
 
        //create the rotation we need to be in to look at the target
        var  _lookRotation = Quaternion.LookRotation(_direction);
 
        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 2f);
        yield return new WaitForSeconds(2f);
        
        
    }
}
