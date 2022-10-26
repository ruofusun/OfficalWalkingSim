using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCanvasController : MonoBehaviour
{
    public RectTransform whistle;
    public RectTransform  ball;
    public RectTransform  egg;

    public RectTransform popup;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (popup.gameObject.activeSelf == true)
            {
                HideAllUI();
            }
        }
    }

    public void LoadWhistleUI()
    {
        popup.gameObject.SetActive(true);
        whistle.gameObject.SetActive(true);
        ball.gameObject.SetActive(false);
        egg.gameObject.SetActive(false);
        
    }
    public void LoadBallUI()
    {
        popup.gameObject.SetActive(true);
        whistle.gameObject.SetActive(false);
        ball.gameObject.SetActive(true);
        egg.gameObject.SetActive(false);
    }
    
    public void LoadEggUI()
    {
        popup.gameObject.SetActive(true);
        whistle.gameObject.SetActive(false);
        ball.gameObject.SetActive(false);
        egg.gameObject.SetActive(true);
    }

    public void HideAllUI()
    {
        popup.gameObject.SetActive(false);
        whistle.gameObject.SetActive(false);
        ball.gameObject.SetActive(false);
        egg.gameObject.SetActive(false);
    }
}
