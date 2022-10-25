using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAniEvent : MonoBehaviour
{
    public GameObject player;
    public MouseLook mouseLook;
    public FakePlayer fakePl;

    public void GiveLookRight()
    {
        mouseLook.enabled = true;
        mouseLook.isLookUp = true;
        fakePl.isLookUp = true;
    }
}
