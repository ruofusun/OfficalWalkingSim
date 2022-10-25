using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    public List<Task> tasks;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckTaskStatus()
    {
        foreach (var task in tasks)
        {
            if (task.TaskFinished)
            {
                return false;
            }
        }

        //todo: game over
        Debug.Log("game over");
        return true;
    }
}
