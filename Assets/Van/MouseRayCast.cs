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
            if(storeTimer > storeLimit)
            {
                storeTimer = 0;
                isReadyStore = true;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector3 focusPoint = new Vector3(Screen.width / 2, Screen.height / 2);

        //从摄像机发出到点击坐标的射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo,1))
        {
            Debug.DrawLine(ray.origin, hitInfo.point);

            //划出射线，只有在scene视图中才能看到
            GameObject gameObj = hitInfo.collider.gameObject;
            Debug.Log("click object name is " + gameObj.name);

            if(gameObj != rayCastObject)
            {
                if(rayCastObject != null)
                {
                    rayCastObject.TryGetComponent(out OutlineReflection outline);
                    if(outline != null)
                    {
                        outline.outlineShader.SetActive(false);
                    }
                }

                rayCastObject = gameObj;

            }

            if(rayCastObject.TryGetComponent(out OutlineReflection outline1))
            {
                outline1.outlineShader.SetActive(true);
            }

            //当射线碰撞目标为boot类型的物品，执行拾取操作
            //碰撞目标为Patato时：
            if (gameObj.tag == "Patato")
            {
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
            if (gameObj.tag == "PatatoBox")
            {
                if (Input.GetMouseButton(0) && isReadyStore)
                {
                    isReadyStore = false;
                    
                    Debug.Log("Restore the patatoes you have in your bag!        " + patatoCollector.amountOfPatatoInBag);

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

            if(gameObj.tag == "SleepSpot")
            {
                if(Input.GetMouseButton(0))
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
                if(rayCastObject != null)
                {
                    rayCastObject.TryGetComponent(out OutlineReflection outline);
                    if(outline != null)
                    {
                        outline.outlineShader.SetActive(false);
                    }
                }
        }

    }
}
