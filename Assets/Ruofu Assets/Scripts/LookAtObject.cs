using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    // Start is called before the first frame update
    void Start()
    {
        if (_object == null)
        {
            _object = FindObjectOfType<Camera>().gameObject;
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _object.transform.position);
    }
}
