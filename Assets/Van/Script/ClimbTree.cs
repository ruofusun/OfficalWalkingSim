using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbTree : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Tree")
        {
            Debug.Log("test");
        }
    }


}
