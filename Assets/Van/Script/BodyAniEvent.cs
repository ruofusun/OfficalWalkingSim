using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAniEvent : MonoBehaviour
{
    public GameObject player;
    public MouseLook mouseLook;
    public FirstPersonDrifter fpDrifter;
    public FakePlayer fakePl;
    public Animator animator;
    public RectTransform CANVAS;
    
    

    public void GiveLookRight()
    {
        if (ScenesManager.Instance.isScene1)
        {
            TextManager.Instance.SaySomething("Get up and make the f*** money!", 3);
        }

        mouseLook.enabled = true;
        mouseLook.isLookUp = true;
        fpDrifter.isLookUp = true;
        fakePl.isLookUp = true;
        if (animator)
        {
            animator.enabled = true;
            if (CANVAS)
            {
                CANVAS.gameObject.SetActive(false);
            }

            GetComponent<Animator>().enabled = false;
            //fakePl.transform.eulerAngles = new Vector3(player.transform.rotation.eulerAngles.x,
               // player.transform.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z);

            //  Vector3 pos = animator.gameObject.transform.position+ animator.gameObject.transform.parent.position;
            //  animator.gameObject.transform.SetParent(null);
            //   animator.gameObject.transform.position = pos;
        }
    }
    
    
}
