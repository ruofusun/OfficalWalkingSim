using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class ChaseTarget :Action
{
    public SharedVector3 target;
    private Rigidbody rigidbody;
  //  public float Offset = 0.25f;
    public SharedFloat Speed = 2f;
    private CrowController crow;



    public override void OnStart()
    {
        rigidbody = GetComponent<Rigidbody>();
         crow = GetComponent<CrowController>();
        if (target == null)
        {
            target.Value = crow.target.transform.position+ new Vector3(0, 1, 0);;
        }

        if (target != null)
        {
            crow.target.ChangeFavorValue(-1);
        }

    }

    private Vector3 offset;
    public override TaskStatus OnUpdate()
    {
        if (target == null)
        {
            return TaskStatus.Failure;
        }

        target.Value = crow.target.transform.position + new Vector3(0, 2, 0);
        offset = target.Value - transform.position;
        
        
        //send afraid event to the other animals
        var behaviorTree = crow.target.GetComponent<BehaviorTree>();
        behaviorTree.SendEvent<object>("afraid",5);

        Vector3 rotation = crow.target.transform.rotation.eulerAngles;
        transform.eulerAngles = rotation;
    //    if (offset.sqrMagnitude < Offset)
     //   {
         //   return TaskStatus.Success;
     //   }

        //   StartCoroutine(RotateAndMove());
        rigidbody.velocity = offset * Speed.Value;

        //  Debug.Log(offset.magnitude + animal.TargetFood.name);
        // rigidbody.velocity = offset.normalized * Speed.Value;

        return TaskStatus.Running;
    }


 //  IEnumerator RotateAndMove()
  //  {
        //find the vector pointing from our position to the target
    //    var _direction = (animal.TargetFood.transform.position- transform.position).normalized;
 
        //create the rotation we need to be in to look at the target
       // var  _lookRotation = Quaternion.LookRotation(_direction);
 
        //rotate us over time according to speed until we are in the required rotation
      //  transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 2f);
    //    yield return new WaitForSeconds(2f);
        
        
  //  }
}
