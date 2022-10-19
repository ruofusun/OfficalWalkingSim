using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour
{
    public GameObject pickupGameObject;

    public Vector3 holdingPosition;

    public void PickUpGameObject(GameObject holdingTarget)
    {
        pickupGameObject = holdingTarget;
        pickupGameObject.transform.SetParent(this.transform);
        pickupGameObject.transform.localPosition = holdingPosition;
        Rigidbody rb = pickupGameObject.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    public void DropGameObject()
    {
        if (pickupGameObject)
        {
          //  yield return new WaitForSeconds(0.1f);
            Rigidbody rb = pickupGameObject.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }
            pickupGameObject.transform.SetParent(null);
            pickupGameObject = null;
        }
    }
}
