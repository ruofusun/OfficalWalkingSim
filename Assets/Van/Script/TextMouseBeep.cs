using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMouseBeep : TextMouse
{
    public override void OnClick()
    {
        Destroy(SoundManager.Instance.loopSESource.gameObject);
        Destroy(this.gameObject);
    }
}
