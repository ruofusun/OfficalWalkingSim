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

    // Start is called before the first frame update
    void Start()
    {
        picker = GetComponent<Picker>();
        rayCastObject = null;
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

        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, rayDistance))
        {
            Debug.DrawLine(transform.position, hitInfo.point);

            //�������ߣ�ֻ����scene��ͼ�в��ܿ���
            GameObject gameObj = hitInfo.collider.gameObject;
            Debug.Log("click object name is " + gameObj.name);

            if (gameObj != rayCastObject)
            {
                if (rayCastObject != null)
                {
                    rayCastObject.TryGetComponent(out OutlineReflection outline);
                    if (outline != null)
                    {
                        outline.outlineShader.SetActive(false);
                    }
                }

                rayCastObject = gameObj;

            }

            if (rayCastObject.TryGetComponent(out OutlineReflection outline1))
            {
                outline1.outlineShader.SetActive(true);
            }

            //��������ײĿ��Ϊboot���͵���Ʒ��ִ��ʰȡ����
            //��ײĿ��ΪPatatoʱ��
            if (gameObj.tag == "Patato")
            {
                if (Input.GetMouseButtonDown(0))
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
                if (Input.GetMouseButtonDown(0))// todo: need to check the favorability of chicken 
                {
                    Debug.Log("pickup the Chicken!");
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
                if (Input.GetMouseButtonDown(0) && picker.pickupGameObject)
                {
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
