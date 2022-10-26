using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotation : MonoBehaviour
{
    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 relativePos = transform.position - camera.transform.position;
        Quaternion lookAtRotation = Quaternion.LookRotation(relativePos, Vector3.up);
        Vector3 targetV3 = this.transform.eulerAngles;

        if(Mathf.Abs(targetV3.y - lookAtRotation.eulerAngles.y) > 10f)
        {
            targetV3.y = lookAtRotation.eulerAngles.y;
            LeanTween.rotate(this.gameObject, targetV3, 0.2f);
        }
    }
}
