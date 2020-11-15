using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrushSize : MonoBehaviour
{
    public TrailRenderer brush;
    public Slider brushSizeSlider;
    public void OnSliderValueChanged(float value)
    {
        if (value >= 1)
        {
            brush.startWidth = 0.1f;
            brush.endWidth = 0.1f;
        }
        if (value == 2)
        {
            brush.startWidth = 0.2f;
            brush.endWidth = 0.2f;
        }
        if (value == 3)
        {
            brush.startWidth = 0.3f;
            brush.endWidth = 0.3f;
        }
        if (value == 4)
        {
            brush.startWidth = 0.4f;
            brush.endWidth = 0.4f;
        }
        if (value == 5)
        {
            brush.startWidth = 0.5f;
            brush.endWidth = 0.5f;
        }
        if (value == 6)
        {
            brush.startWidth = 0.6f;
            brush.endWidth = 0.6f;
        }
        if (value == 7)
        {
            brush.startWidth = 0.7f;
            brush.endWidth = 0.7f;
        }
    }
}
