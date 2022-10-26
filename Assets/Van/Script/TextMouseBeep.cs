using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMouseBeep : TextMouse
{
    public bool isActive = false;
    public override void OnClick()
    {
        AnimationManager.Instance.bodyAni.SetTrigger("Up");
        AnimationManager.Instance.clockAni.SetTrigger("ClockOff");
        Destroy(SoundManager.Instance.loopSESource.gameObject);
        Destroy(this.gameObject);
    }

    public void OnMouseEnter()
    {
        transform.localScale *= 1.5f;
        isActive = true;
    }

    public void OnMouseExit()
    {
        transform.localScale /= 1.5f;
        isActive = false;
    }

    private void OnMouseDown()
    {
        Debug.Log("On mouse click on clock");
        OnClick();
    }

    private void Update()
    {
        if (isActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
        }
    }
}
