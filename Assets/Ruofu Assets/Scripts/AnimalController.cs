using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BehaviorDesigner.Runtime;
using Sirenix.Utilities;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    public bool Inthefarm = true;
    public bool IsNPC = false;
    public bool IsTutorialChicken = false;

    private List<FoodController> detectedFood;
    private FoodController targetFood;
    public FoodController.FoodType desiredFoodType;
    private MoodCanvasController moodCanvasController;
    private BehaviorTree behaviorTree;

    
    
    //favoribility system
    private int currentFavor = 0;
    public int favorThreshold = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        behaviorTree = GetComponent<BehaviorTree>();
        if (!IsNPC && !IsTutorialChicken)
        {

            behaviorTree.SetVariableValue("Origin", transform.position);
        }

        detectedFood = new List<FoodController>();
        moodCanvasController = GetComponentInChildren<MoodCanvasController>();
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
        if (food && food.type == desiredFoodType&& !food.NeedPickUp)
        {
            if (!detectedFood.Contains(food))
            {
                detectedFood.Add(food);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "FarmArea")
        {
            Inthefarm = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FarmArea")
        {
            Inthefarm = false;
        }
        FoodController food = other.GetComponent<FoodController>();
        if (food && food.type == desiredFoodType && !food.NeedPickUp)
        {
            if (detectedFood.Contains(food))
            {
                detectedFood.Remove(food);
            }
        }
    }

    public FoodController GetNearestFood()
    {
     /*   List<FoodController> temp= new List<FoodController>();

        foreach (var food in detectedFood)
        {
            if (food)
            {
                temp.Add(food);
            }

        }

        detectedFood = temp;*/
     

     if (detectedFood.Count > 0)
        {
            detectedFood = detectedFood.OrderBy(
                x => Vector3.Distance(this.transform.position,x==null? new Vector3(10000,0,0):x.transform.position)
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

    public FoodController TargetFood
    {
        get => targetFood;
        set => targetFood = value;
    }

    public MoodCanvasController MoodCanvasController => moodCanvasController;


    public void ConsumeFood()
    {
        detectedFood.Remove(targetFood);
        if(targetFood)
        Destroy(targetFood.gameObject);
        currentFavor++;
        currentFavor = Mathf.Min(currentFavor, 2);
    }

    public bool IsFavored()
    {
        return currentFavor >= favorThreshold;
    }

    public void ChangeFavorValue(int value)
    {
        currentFavor += value;
        currentFavor = Mathf.Min(currentFavor, 2);
        currentFavor = Mathf.Max(currentFavor, -1);
    }

}

