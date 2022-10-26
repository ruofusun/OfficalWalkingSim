using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PatatoCollector : MonoBehaviour
{
    public int amountOfPatatoInBag;
    public int amountOfStorePatato;

    public float speedFactor;

    public FirstPersonDrifter drifter;

    public GameObject potatoBox;
    public GameObject freePotatoPrefab;

    public TextMeshProUGUI text;

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
            DropPatatoInBox();
            amountOfStorePatato += 1;
            amountOfPatatoInBag -= 1;
            ChangeSpeed();
        }

    }

    public void DropPatatoInBox()
    {
        GameObject patato = Instantiate(freePotatoPrefab);
        patato.transform.SetParent(potatoBox.transform);
        patato.transform.position = potatoBox.transform.position + Vector3.up * 1.5f + Vector3.right * Random.Range(-0.1f,0.1f) + Vector3.forward *Random.Range(-0.1f,0.1f); 
    }

    //return true if the store amount is greater or equal to 30.
    public bool CheckPatatoCondition()
    {
        if(amountOfStorePatato >= 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        text.text = amountOfStorePatato.ToString() + "/30";
    }
}
