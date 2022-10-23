using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextMouse : MonoBehaviour
{
    public bool isRaycasting = false;
    public float changeScale = 1.5f;
    public void IncreaseScale()
    {
        transform.localScale *= changeScale;
    }

    public void DecreaseScale()
    {
        transform.localScale /= changeScale;
    }

    public virtual void OnClick()
    {

    }
    private void Update()
    {

    }

    /*    public void OnPointerEnter(PointerEventData pointerEventData)
        {
            transform.localScale *= 1.2f;



        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            transform.localScale /= 1.2f;

        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            Debug.Log("turn off the clock");
            Destroy(this.gameObject);
        }*/
}
