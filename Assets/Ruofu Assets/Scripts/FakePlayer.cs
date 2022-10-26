using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayer : MonoBehaviour
{
    public bool invertY = false;
   
    public float sensitivityX = 10F;
    public float sensitivityY = 9F;
 
    public float minimumX = -360F;
    public float maximumX = 360F;
 
    public float minimumY = -360F;
    public float maximumY = 360F;
 
    float rotationX = 0F;
    float rotationY = 0F;
 
    private List<float> rotArrayX = new List<float>();
    float rotAverageX = 0F;    
 
    private List<float> rotArrayY = new List<float>();
    float rotAverageY = 0F;
 
    public float framesOfSmoothing = 5;
 
    Quaternion originalRotation;

    public bool isLookUp;

    private void Start()
    {
        originalRotation = transform.localRotation;
    }

    void Update ()
    {
        if (isLookUp)
        {
            rotAverageX = 0f;

            rotationX += Input.GetAxis("Mouse X") * sensitivityX * Time.timeScale;

            rotArrayX.Add(rotationX);

            if (rotArrayX.Count >= framesOfSmoothing)
            {
                rotArrayX.RemoveAt(0);
            }
            for (int i = 0; i < rotArrayX.Count; i++)
            {
                rotAverageX += rotArrayX[i];
            }
            rotAverageX /= rotArrayX.Count;
            rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);

            Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);
            //transform.localRotation = originalRotation * xQuaternion;          




            Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
            transform.localRotation = originalRotation * xQuaternion;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            // transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y, 0,
            // 0);

        }
    }

    public static float ClampAngle (float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F)) {
            if (angle < -360F) {
                angle += 360F;
            }
            if (angle > 360F) {
                angle -= 360F;
            }        
        }
        return Mathf.Clamp (angle, min, max);
    }

}
