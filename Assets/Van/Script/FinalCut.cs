using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FinalCut : MonoBehaviour
{
    public PostProcessVolume ppVolume;
    public float changeSpd;
    public bool isChanging = false;

    public void AdjustPostProcessEffect()
    {
        ppVolume.weight -= Time.deltaTime * changeSpd;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isChanging)
        {
            AdjustPostProcessEffect();
        }
    }
}
