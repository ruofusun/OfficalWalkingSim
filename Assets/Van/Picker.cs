using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class Picker : MonoBehaviour
{
    public GameObject pickupGameObject;

    public Vector3 holdingPosition;

    public Vector3 chickenPosition;

    public Vector3 chickenRotation;

    private bool canPick = true;

    public void PickUpGameObject(GameObject holdingTarget)
    {

        if (!canPick)
            return;

        if (transform.childCount > 1 && transform.GetChild(1).gameObject != holdingTarget.gameObject)
        {
            DropCertainGameObject(transform.GetChild(0).gameObject);
        }

        pickupGameObject = holdingTarget;
        
        BehaviorTree bt = pickupGameObject.GetComponent<BehaviorTree>();
        if(bt)
        {
            var behaviorTree = pickupGameObject.GetComponent<BehaviorTree>();
            behaviorTree.SendEvent<object>("pickup",5);
            AnimalController animal = pickupGameObject.GetComponent<AnimalController>();
            if (animal&&!animal.IsFavored())
            {
                return;
            }
            bt.enabled = false;
        }
        
        pickupGameObject.transform.SetParent(this.transform);
        pickupGameObject.transform.localPosition = holdingPosition;
        Rigidbody rb = pickupGameObject.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

     

        Animator anim = pickupGameObject.GetComponent<Animator>();
        {
            if (anim)
            {
                anim.enabled = false;
            }
        }

        if (holdingTarget.tag == "Chicken")
        {
            pickupGameObject.transform.localPosition = chickenPosition;
            pickupGameObject.transform.localEulerAngles  = chickenRotation;
        }


    }

    public void DropGameObject()
    {
        if (pickupGameObject)
        {

            AnimalController animal = pickupGameObject.GetComponent<AnimalController>();
            if (animal && !animal.IsFavored())
            {
                return;
            }

            //  yield return new WaitForSeconds(0.1f);
            Rigidbody rb = pickupGameObject.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.AddForce(transform.forward * rb.mass * 2f, ForceMode.Impulse);
            }

            BehaviorTree bt = pickupGameObject.GetComponent<BehaviorTree>();
            if (bt)
            {
                bt.enabled = true;
                //  bt.SendEvent<object>("dropoff",5);
                StartCoroutine(SendEventRoutine(bt, "dropoff"));
            }

            Animator anim = pickupGameObject.GetComponent<Animator>();
            {
                if (anim)
                {
                    anim.enabled = true;
                }
            }
            pickupGameObject.transform.eulerAngles = new Vector3(0, pickupGameObject.transform.eulerAngles.y, 0);

            FoodController food = pickupGameObject.GetComponent<FoodController>();
            if (food && food.NeedPickUp)
            {
                food.NeedPickUp = false;
            }

            pickupGameObject.transform.SetParent(null);
            pickupGameObject = null;

            StartCoroutine(ResetCanpickRoutine());


        }

    }

    public void DropCertainGameObject(GameObject obj)
        {
            AnimalController animal = obj.GetComponent<AnimalController>();
                if (animal&&!animal.IsFavored())
                {
                    return;
                }
                //  yield return new WaitForSeconds(0.1f);
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.isKinematic = false;
                    rb.useGravity = true;
                    rb.AddForce(transform.forward*rb.mass*2f,ForceMode.Impulse);
                }
            
                BehaviorTree bt = obj.GetComponent<BehaviorTree>();
                if(bt)
                {
                    bt.enabled = true;
                    //  bt.SendEvent<object>("dropoff",5);
                    StartCoroutine(SendEventRoutine(bt, "dropoff"));
                }
            
                Animator anim =obj.GetComponent<Animator>();
                {
                    if (anim)
                    {
                        anim.enabled = true;
                    }
                }
                obj.transform.eulerAngles = new Vector3(0, obj.transform.eulerAngles.y, 0);
            
            
                FoodController food = obj.GetComponent<FoodController>();
                if (food && food.NeedPickUp)    
                {
                    food.NeedPickUp = false;
                }
                obj.transform.SetParent(null);
                pickupGameObject = null;

                StartCoroutine(ResetCanpickRoutine());


            }

    

    IEnumerator SendEventRoutine(BehaviorTree bt, string name)
    {
        yield return new WaitForSeconds(1f);
        bt.SendEvent<object>(name,5);
        

    }

    IEnumerator ResetCanpickRoutine()
    {
        canPick = false;
        yield return new WaitForSeconds(0.2f);
        canPick = true;
    }
}
