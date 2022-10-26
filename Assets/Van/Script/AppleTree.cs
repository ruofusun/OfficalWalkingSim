using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    public List<FoodController> apples;
    public Picker picker;

    private void Start()
    {
        picker = FindObjectOfType<Picker>().GetComponent<Picker>();
    }

    public void PickUpAppleInTheTree()
    {
        picker.PickUpGameObject(apples[0].gameObject);
        apples.RemoveAt(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player is on the tree");
            other.GetComponent<FirstPersonDrifter>().isInTree = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
