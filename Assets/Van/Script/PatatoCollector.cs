using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatatoCollector : MonoBehaviour
{
    public int amountOfPatatoInBag;
    public int amountOfStorePatato;

    public float speedFactor;

    public FirstPersonDrifter drifter;

    //check the patato amount, return false if the amount is above 10
    public bool PatatoCheck()
    {
        if(amountOfPatatoInBag < 10)
        {
            amountOfPatatoInBag += 1;
            ChangeSpeed();
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ChangeSpeed()
    {
        drifter.speed = drifter.walkSpeed - amountOfPatatoInBag * speedFactor;
    }

    //Transfer the patato data
    public void StorePatato()
    {
        Debug.Log("Storing   " + amountOfPatatoInBag);
        if(amountOfPatatoInBag > 0)
        {
            amountOfStorePatato += 1;
            amountOfPatatoInBag -= 1;
            ChangeSpeed();
        }

    }
    //return true if the store amount is greater or equal to 30.
    public bool CheckPatatoCondition()
    {
        if(amountOfStorePatato >= 10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
