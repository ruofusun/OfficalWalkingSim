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
}
