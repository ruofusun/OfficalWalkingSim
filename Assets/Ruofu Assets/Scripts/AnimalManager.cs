using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    private List<AnimalController> animals;
    // Start is called before the first frame update
    void Start()
    {
        animals = new List<AnimalController>();
        GetCurrentAnimals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<AnimalController> GetCurrentAnimals()
    {
        animals = FindObjectsOfType<AnimalController>().ToList();
        List<AnimalController> temp = new List<AnimalController>();
        
        foreach (var animal in animals)
        {
            if (animal.Inthefarm && !animal.IsNPC)
            {
               temp.Add(animal);
            }
        }

        animals = temp;
        return animals;
    }

    public AnimalController GetOneRandomAnimal()
    {
        GetCurrentAnimals();
        int rand = Random.Range(0, animals.Count);
        return animals[rand];

    }
}
