using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAniEvent : MonoBehaviour
{
    public GameObject player;
    public MouseLook mouseLook;
    public FakePlayer fakePl;
    public Animator animator;
    

    public void GiveLookRight()
    {
        mouseLook.enabled = true;
        mouseLook.isLookUp = true;
        fakePl.isLookUp = true;
        if (animator)
        {
            animator.enabled = true;
            GetComponent<Animator>().enabled = false;
            fakePl.transform.eulerAngles = new Vector3(player.transform.rotation.eulerAngles.x,
                player.transform.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z);

            //  Vector3 pos = animator.gameObject.transform.position+ animator.gameObject.transform.parent.position;
            //  animator.gameObject.transform.SetParent(null);
            //   animator.gameObject.transform.position = pos;
        }
    }
    
    
}
