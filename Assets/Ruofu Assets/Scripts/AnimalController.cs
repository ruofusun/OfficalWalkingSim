using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    public bool Inthefarm = true;

    private List<FoodController> detectedFood;
    private FoodController targetFood;
    
    // Start is called before the first frame update
    void Start()
    {
        detectedFood = new List<FoodController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FarmArea")
        {
            Inthefarm = true;
        }
        FoodController food = other.GetComponent<FoodController>();
        if (food)
        {
            if (!detectedFood.Contains(food))
            {
                detectedFood.Add(food);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FarmArea")
        {
            Inthefarm = false;
        }
        FoodController food = other.GetComponent<FoodController>();
        if (food)
        {
            if (detectedFood.Contains(food))
            {
                detectedFood.Remove(food);
            }
        }
    }

    public FoodController GetNearestFood()
    {
        if (detectedFood.Count > 0)
        {
            detectedFood = detectedFood.OrderBy(
                x => Vector3.Distance(this.transform.position,x.transform.position)
            ).ToList();
            targetFood = detectedFood[0];
            return detectedFood[0];
        }
        else
        {
            targetFood = null;
            return null;
        }
        
    }

}

