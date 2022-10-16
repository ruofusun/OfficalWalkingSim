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
    }
}
