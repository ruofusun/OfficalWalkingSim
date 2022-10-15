using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class RotateHeadToTarget : Action
{
    public SharedVector3 target;
   // private Rigidbody rigidbody;
    public float Offset = 0.25f;

    private AnimalController animal;



    public override void OnStart()
    {
       // rigidbody = GetComponent<Rigidbody>();
        animal = GetComponent<AnimalController>();
        if (target == null)
        {
            target.Value = animal.TargetFood.transform.position;
        }

    }

    private float t;
    public override TaskStatus OnUpdate()
    {
        if (target == null)
        {
            return TaskStatus.Failure;
        }
        
        //find the vector pointing from our position to the target
        var _direction = (animal.TargetFood.transform.position- transform.position).normalized;
 
        //create the rotation we need to be in to look at the target
        var  _lookRotation = Quaternion.LookRotation(_direction);
        _lookRotation.z = 0;
        _lookRotation.x = 0;

      
        t+= Time.deltaTime*0.5f;
        t = Mathf.Min(1, t);
        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, t);
//        Debug.Log(t);
        
     //   Debug.Log(offset);
    //    var offset = animal.transform.rotation.Diff(_lookRotation);
       // Debug.Log(offset);
        
        if ((Mathf.Abs(t-1)) < 0.001)
        {
          //  transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation, 1);
            Debug.Log("ROTATION ENDS");
            return TaskStatus.Success;
       
        }

     //   Debug.Log(offset);
      //  if (offset.ma)
     //   {
        //   return TaskStatus.Success;
     //   }


        return TaskStatus.Running;
    }

}
