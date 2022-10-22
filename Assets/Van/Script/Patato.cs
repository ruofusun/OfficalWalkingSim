using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patato : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Audio:≤»ªµµƒ“Ù–ß
            Debug.Log("You step on a patato, what a waste!!!");
            Destroy(this.gameObject);
        }
    }
}
