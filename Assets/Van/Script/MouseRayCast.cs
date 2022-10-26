using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class MouseRayCast : MonoBehaviour
{
    public GameObject rayCastObject;
    public Picker picker;
    //public ScenesManager sceneM;
    public PatatoCollector patatoCollector;
    public Camera camera;
    public FirstPersonDrifter drifter;
    public float storeTimer;
    public float storeLimit;
    public bool isReadyStore;
    public bool isReadySleep;
    public ScenesManager scenesM;
    public float rayDistance = 1;
    public GameObject observeObject;
    public float observeDistance = 100;

    private UIController uiController;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        picker = GetComponent<Picker>();
        rayCastObject = null;
        uiController = FindObjectOfType<UIController>();
        audioSource = GetComponent<AudioSource>();
    }

    public void CompareHitAndObserve()
    {
        if (observeObject != rayCastObject)
        {
            if (rayCastObject != null)
            {
                OutlineReflection outline = rayCastObject.GetComponentInChildren<OutlineReflection>();
                if (outline != null)
                {
                    outline.outlineShader.SetActive(false);
                }

                if (rayCastObject.tag == "Text")
                {
                    rayCastObject.GetComponent<TextMouse>().DecreaseScale();
                    rayCastObject.GetComponent<TextMouse>().isRaycasting = false;

                }

                if(rayCastObject.tag == "PatatoBox")
                {
                    uiController.HidePotatoUI();
                }

                rayCastObject = null;
            }
            
           

        }
      
        
    }

    private void Update()
    {


        CompareHitAndObserve();

        if (!isReadyStore)
        {
            storeTimer += Time.deltaTime;
            if (storeTimer > storeLimit)
            {
                storeTimer = 0;
                isReadyStore = true;
            }
        }

        if (!scenesM.isScene1)
        {
            if (Input.GetMouseButtonDown(0) && picker.pickupGameObject)
            {
                picker.DropGameObject();

            }

            if (Input.GetMouseButtonDown(1) && picker.pickupGameObject)
            {
                picker.ThrowGameObject();

            }
        }



        //�������������������������
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        // used to get the observe info without hitting any collider in hitInfo.
        RaycastHit observeInfo;
        // Bit shift the index of the layer (2) to get a bit mask
        int layerMask = 1 << 2;

        // This would cast rays only against colliders in layer 2.
        // But instead we want to collide against everything except layer 2. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        if (Physics.Raycast(camera.transform.position, transform.forward, out observeInfo, observeDistance,
            layerMask))
        {
            observeObject = observeInfo.collider.gameObject;
        }

        if (Physics.Raycast(camera.transform.position, transform.forward, out hitInfo, rayDistance, layerMask))
        {
            Debug.DrawLine(camera.transform.position, hitInfo.point);

            //�������ߣ�ֻ����scene��ͼ�в��ܿ���
            GameObject gameObj = hitInfo.collider.gameObject;
            //Debug.Log("click object name is " + gameObj.name);
            /*            if (gameObj.name.Contains("Terrain"))
                        {
                            uiController.HideUI();
                        }*/

            CompareHitAndObserve();
            rayCastObject = gameObj;

            OutlineReflection outline1 = rayCastObject.GetComponentInChildren<OutlineReflection>();
            if (outline1)
            {
                outline1.outlineShader.SetActive(true);
            }

            //��������ײĿ��Ϊboot���͵���Ʒ��ִ��ʰȡ����
            //��ײĿ��ΪPatatoʱ��
            if (gameObj.tag == "Patato")
            {
                if (!scenesM.isScene1)
                {
                    uiController.ShowPickUpUI();
                    uiController.HideDropOffUI();
                }

                if (Input.GetMouseButtonUp(0))
                {
                    SoundManager.Instance.PlaySoundEffect(SoundManager.Instance.patatoHarvest);
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
            else if (gameObj.tag == "Chicken")
            {
                uiController.ShowPickUpUI();
                uiController.HideDropOffUI();
                Debug.Log("get chicken");
                if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("pickup the Chicken!");
                    picker.PickUpGameObject(rayCastObject);

                }
            }
            else if (gameObj.tag == "Sheep"|| gameObj.tag =="Cow")

    {
                uiController.ShowWhistleUI();
                uiController.HideDropOffUI();
                Debug.Log("get sheep or cow");
                if (Input.GetMouseButtonUp(0))
                {
                    BehaviorTree bt =  rayCastObject.GetComponent<BehaviorTree>();
                    if(bt)
                    {
                        audioSource.Play();
                        bt.SendEvent<object>("whistle",5);
                        AnimalController animal =rayCastObject.GetComponent<AnimalController>();
                        if (animal&&!animal.IsFavored())
                        {
                            return;
                        }
                        Debug.Log("whistle the sheep or cow!");
                        // bt.enabled = false;
                    }
                   
                    
                }
            }
            else if (gameObj.tag == "Apple")
            {
                uiController.ShowPickUpUI();
                uiController.HideDropOffUI();
                Debug.Log("get apple");
                if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("pickup the apple!");
                    picker.PickUpGameObject(rayCastObject);
                    
                }
            }

            else if(gameObj.tag == "AppleTree")
            {
                AppleTree appleTree = gameObj.GetComponent<AppleTree>();

                if (appleTree.apples.Count > 0)
                {
                  //  uiController.ShowUI();
                  

                    if (Input.GetMouseButtonUp(0))
                    {
                        appleTree.PickUpAppleInTheTree();
                        Debug.Log("get apple from the tree");
                    }

                }
                else
                {
                    if (Input.GetMouseButton(0))
                    {
                        Debug.Log("There is no more apple in the tree");
                    }
                }

            }

            else if (gameObj.tag == "PatatoBox")
            {
                uiController.ShowPotatoUI();

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

            else if (gameObj.tag == "SleepSpot")
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
            else if (gameObj.tag == "Door")
            {
                if (Input.GetMouseButton(0))
                {
                    Debug.Log("Door is open");
                    AnimationManager.Instance.doorAni.SetBool("isOpen", !AnimationManager.Instance.doorAni.GetBool("isOpen"));
                }
            }
            else if (gameObj.tag == "Text")
            {
                TextMouse textMouse = gameObj.GetComponent<TextMouse>();

                if (!textMouse.isRaycasting)
                {
                    textMouse.IncreaseScale();
                    textMouse.isRaycasting = true;
                }

                if (Input.GetMouseButton(0))
                {
                    textMouse.OnClick();
                }
            }
            else
            {
                uiController.HidePickUpUI();
                uiController.HideWhistleUI();
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
