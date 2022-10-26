using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameObject PickUpUI;

    public GameObject DropThrowUI;

    public GameObject WhislteUI;

    public GameObject potatoBoxUI;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShowPickUpUI()
    {
        PickUpUI.gameObject.SetActive(true);
    }
    
    public void HidePickUpUI()
    {
        PickUpUI.gameObject.SetActive(false);
    }
    
    public void ShowWhistleUI()
    {
        WhislteUI.gameObject.SetActive(true);
    }
    
    public void HideWhistleUI()
    {
        WhislteUI.gameObject.SetActive(false);
    }

    public void ShowPotatoUI()
    {
        potatoBoxUI.SetActive(true);
    }

    public void HidePotatoUI()
    {
        potatoBoxUI.SetActive(false);
    }

    public void HideUI()
    {
        Debug.Log("hide ui");
        PickUpUI.gameObject.SetActive(false);
        DropThrowUI.gameObject.SetActive(false);
        WhislteUI.gameObject.SetActive(false);
    }

    public void ShowDropOffUI()
    {
        Debug.Log("show drop off ui");
        DropThrowUI.gameObject.SetActive(true);
    }

    public void HideDropOffUI()
    {
        Debug.Log("hide drop off ui");
        DropThrowUI.gameObject.SetActive(false);
    }
}
