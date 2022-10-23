using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Global: MonoBehaviour
{
    public static AnimalManager animalManager;
    public static PlayerController player;



    // public static  GameObject playerGrowSpell;

    public static AnimalManager GetAnimalManager()
    {
        if(animalManager==null)
            animalManager = FindObjectOfType<AnimalManager>();


        return animalManager;
    }
    
    public static PlayerController GetPlayer()
    {
        if(player==null)
           player = FindObjectOfType<PlayerController>();


        return player;
    }
}
