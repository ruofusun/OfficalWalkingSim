using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MoodCanvasController : MonoBehaviour
{
    public Sprite apple;

    public Sprite potato;

    public Sprite heart;

    public Sprite afraid;


    public Image contentImage;

    public List<Sprite> contents;
    private Mood currentmood = Mood.apple;

    public enum Mood
    {
        apple,
        potato,
        heart,
        afraid
    }



    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadContent()
    {
        Debug.Log("mood" + currentmood + ((int)currentmood-1));
        StartCoroutine(LoadContentRoutine(currentmood));
    }

    public void LoadFoodContent()
    {
        currentmood = Mood.potato;
        LoadContent();
    }
    
    public void LoadHappyContent()
    {
        currentmood = Mood.heart;
        LoadContent();
    }
    
    public void LoadAfraidContent()
    {
        currentmood = Mood.afraid;
        LoadContent();
    }

    public IEnumerator LoadContentRoutine(Mood mood)
    {
        List<Image> children = transform.GetComponentsInChildren<Image>(true).ToList();

        foreach (var child in children)
        {
            child.gameObject.SetActive(true);
        }
        contentImage.sprite = contents[(int)mood];
        yield return new WaitForSeconds(5f);
        foreach (var child in children)
        {
            child.gameObject.SetActive(false);
        }
        
    }
}
