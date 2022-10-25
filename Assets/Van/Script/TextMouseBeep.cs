using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMouseBeep : TextMouse
{
    public override void OnClick()
    {
        AnimationManager.Instance.bodyAni.SetTrigger("Up");
        Destroy(SoundManager.Instance.loopSESource.gameObject);
        Destroy(this.gameObject);
    }

    public void OnMouseEnter()
    {
        transform.localScale *= 1.5f;
    }

    public void OnMouseExit()
    {
        transform.localScale /= 1.5f;

    }

    private void OnMouseDown()
    {
        OnClick();
    }
}
