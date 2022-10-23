using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    private Rigidbody rb;
    public bool isInTree;

    public enum FoodType
    {
        potato,
        apple
        
    }

    public bool NeedPickUp = false;

    public FoodType type = FoodType.potato;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    public void CancelFreeze()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.GetComponent<Terrain>())
        {
            rb.isKinematic = true;
        }
        

    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.GetComponent<Terrain>())
        {
            rb.isKinematic = true;
        }
        
        //todo: pick up should set it back to another setting?
    }
}
