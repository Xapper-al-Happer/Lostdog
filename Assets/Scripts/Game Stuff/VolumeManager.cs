using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public FloatValue volumeValue;
    public Slider slider;

    void Start()
    {
        slider.value = volumeValue.RuntimeValue;
    }

    public void SetVolume(Slider newValue)
    {
        volumeValue.RuntimeValue = newValue.value;
    }


}
