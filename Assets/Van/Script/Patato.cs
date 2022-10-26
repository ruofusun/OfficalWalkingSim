using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patato : MonoBehaviour
{
    public GameObject mashedPotato;
    public GameObject parentRow;

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
        Debug.Log("MashTest");
        if(other.tag == "Player")
        {
            GameObject mashPotato = Instantiate(mashedPotato);
            mashPotato.transform.SetParent(parentRow.transform);
            Debug.Log(parentRow.transform);
            mashedPotato.transform.localPosition = this.transform.localPosition;
            SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.patatoCrash);
            //Audio:≤»ªµµƒ“Ù–ß
            Debug.Log("You step on a patato, what a waste!!!");
            Destroy(this.gameObject);
        }
    }
}
