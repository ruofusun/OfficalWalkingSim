using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patato : MonoBehaviour
{
    public GameObject mashedPotato;
    public GameObject parentRow;
    public List<string> mashPotatoTalk;

    // Start is called before the first frame update
    void Start()
    {
        mashPotatoTalk = new List<string>();
        mashPotatoTalk.Add("Sh*t!");
        mashPotatoTalk.Add("What am I doing?");
        mashPotatoTalk.Add("Stop stepping on the potato!");
        mashPotatoTalk.Add("What's my problem?");
    }

    // Update is called once per frame
    void Update()
    {
        
    } 

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject mashPotato = Instantiate(mashedPotato);
            mashPotato.transform.SetParent(parentRow.transform);
            Debug.Log(parentRow.transform);
            mashPotato.transform.localPosition = this.transform.localPosition;
            SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.patatoCrash);
            if (!TextManager.Instance.isTalk)
            {
                TextManager.Instance.SaySomething(mashPotatoTalk[Random.Range(0,mashPotatoTalk.Count)],1);
            }
            //Audio:²È»µµÄÒôÐ§
            Debug.Log("You step on a patato, what a waste!!!");
            Destroy(this.gameObject);
        }
    }
}
