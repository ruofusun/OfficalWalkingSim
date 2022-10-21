using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRayCast : MonoBehaviour
{
    public GameObject rayCastObject;
    public Picker picker;
    //public ScenesManager sceneM;
    public PatatoCollector patatoCollector;
    public Vector3 cameraVector;
    public FirstPersonDrifter drifter;
    public float storeTimer;
    public float storeLimit;
    public bool isReadyStore;
    public bool isReadySleep;
    public ScenesManager scenesM;
    public float rayDistance = 1;

    private UIController uiController;

    // Start is called before the first frame update
    void Start()
    {
        picker = GetComponent<Picker>();
        rayCastObject = null;
        uiController = FindObjectOfType<UIController>();
    }

    private void Update()
    {
        if (!isReadyStore)
        {
            storeTimer += Time.deltaTime;
            if (storeTimer > storeLimit)
            {
                storeTimer = 0;
                isReadyStore = true;
            }
        }

        if (Input.GetMouseButtonDown(0) && picker.pickupGameObject)
        {
            picker.DropGameObject();

        }

        //�������������������������
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        // Bit shift the index of the layer (2) to get a bit mask
        int layerMask = 1 << 2;

        // This would cast rays only against colliders in layer 2.
        // But instead we want to collide against everything except layer 2. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, rayDistance, layerMask))
        {
            Debug.DrawLine(transform.position, hitInfo.point);

            //�������ߣ�ֻ����scene��ͼ�в��ܿ���
            GameObject gameObj = hitInfo.collider.gameObject;
            Debug.Log("click object name is " + gameObj.name);
            if (gameObj.name.Contains("Terrain"))
            {
                uiController.HideUI();
            }

            if (gameObj != rayCastObject)
            {
                if (rayCastObject != null)
                {
                   OutlineReflection outline = rayCastObject.GetComponentInChildren<OutlineReflection>(); 
                    if (outline != null)
                    {
                        outline.outlineShader.SetActive(false);
                    }
                }

                rayCastObject = gameObj;

            }
         
            OutlineReflection outline1 = rayCastObject.GetComponentInChildren<OutlineReflection>(); 
            if (outline1)
            {
                outline1.outlineShader.SetActive(true);
            }

            //��������ײĿ��Ϊboot���͵���Ʒ��ִ��ʰȡ����
            //��ײĿ��ΪPatatoʱ��
            if (gameObj.tag == "Patato")
            {
                uiController.ShowUI();
                if (Input.GetMouseButton(0))
                {
                    Debug.Log("pickup the PaTATO!");

                    if (scenesM.isScene1)
                    {
                        if (!patatoCollector.PatatoCheck())
                        {
                            Debug.Log("Your Bag is Full of patatoes");
                        }
                        else
                        {
                            Destroy(rayCastObject);
                        }
                    }
                    else
                    {
                        picker.PickUpGameObject(rayCastObject);
                    }
                }
            }
            if (gameObj.tag == "Chicken")
            {
                uiController.ShowUI();
                Debug.Log("get chicken");
                if (Input.GetMouseButton(0))
                {
                    Debug.Log("pickup the Chicken!");
                    picker.PickUpGameObject(rayCastObject);
                    
                }
            }
            if (gameObj.tag == "Apple")
            {
                uiController.ShowUI();
                Debug.Log("get apple");
                if (Input.GetMouseButton(0))
                {
                    Debug.Log("pickup the apple!");
                    picker.PickUpGameObject(rayCastObject);
                    
                }
            }

            if (gameObj.tag == "PatatoBox")
            {
                if (Input.GetMouseButton(0) && isReadyStore)
                {
                    isReadyStore = false;

                    Debug.Log(
                        "Restore the patatoes you have in your bag!        " + patatoCollector.amountOfPatatoInBag);

                    if (scenesM.isScene1)
                    {
                        patatoCollector.StorePatato();
                        isReadySleep = patatoCollector.CheckPatatoCondition();
                    }
                    else
                    {
                        picker.PickUpGameObject(rayCastObject);
                    }
                }

            }

            if (gameObj.tag == "SleepSpot")
            {
                if (Input.GetMouseButton(0))
                {
                    if (isReadySleep)
                    {
                        Debug.Log("Make the transition");
                        scenesM.LoadScene();
                    }
                    else
                    {
                        Debug.Log("The work is not done yet!");
                    }
                }
            }
        }
        else
        {
            if (rayCastObject != null)
            {
                if (Input.GetMouseButton(0) && picker.pickupGameObject)
                {
                    Debug.Log("drop the item");
                    picker.DropGameObject();
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector3 focusPoint = new Vector3(Screen.width / 2, Screen.height / 2);
    }
}
