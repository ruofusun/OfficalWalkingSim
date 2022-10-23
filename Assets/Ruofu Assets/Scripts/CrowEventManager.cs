using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowEventManager : MonoBehaviour
{

    public GameObject crow;

    public float spawncd = 50f;

    private float timer = 0;

    private int eventIndex = 1;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > (spawncd* eventIndex))
        {
            timer = 0;
            SpawnCrow();
        }

    }

    void SpawnCrow()
    {
        audioSource.Play();
        eventIndex = Mathf.Min(3, eventIndex);
        for (int i = 0; i < eventIndex; i++)
        {
            Transform Child = transform.GetChild(Random.Range(0, transform.childCount));
            Instantiate(crow, Child.position, Child.rotation);
        }

        eventIndex++;

        if (eventIndex == 3)
        {
            eventIndex = 0;
        }
    }


}
