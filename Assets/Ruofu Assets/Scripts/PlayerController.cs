using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject potatofood;

    public GameObject applefood;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
           // Instantiate(potatofood, transform.position + transform.forward, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //Instantiate(applefood, transform.position + transform.forward, Quaternion.identity);
        }
    }
}
