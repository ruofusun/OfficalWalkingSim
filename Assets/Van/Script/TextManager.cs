using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{

    public static TextManager Instance;
    public TextMeshProUGUI textBox;
    public bool isTalk;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        //Set the appropriate clips and volume on music and danger loop, then play

    }

    IEnumerator WhiteUp(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isTalk = false;
        textBox.text = "";
    }

    public void SaySomething(string talk,float waitTime)
    {
        textBox.text = talk;
        isTalk = true;
        StartCoroutine(WhiteUp(waitTime));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
