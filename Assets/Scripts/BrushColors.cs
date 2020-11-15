using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushColors : MonoBehaviour
{
    public TrailRenderer brush;
    // Start is called before the first frame update
    void Start()
    {
        brush = GetComponent<TrailRenderer>();
        GameObject go = GameObject.FindGameObjectWithTag("clones");
        this.transform.parent = go.transform;
    }

    // Update is called once per frame
    public void BrushColor(string colorName)
    {
        if (colorName == "Red")
        {
            brush.startColor = Color.red;
            brush.endColor = Color.red;
        }
         if (colorName == "Orange")
        {
            brush.startColor = new Color(1, 0.7148846f, 0);
            brush.endColor = new Color(1, 0.7148846f, 0);
        }
         if (colorName == "Yellow")
        {
            brush.startColor = new Color(9818833f, 1, 0);
            brush.endColor = new Color(9818833f, 1, 0);
        }
         if (colorName == "Green1")
        {
            brush.startColor = new Color(0.2335865f, 1, 0);
            brush.endColor = new Color(0.2335865f, 1, 0);
        }
         if (colorName == "Green2")
        {
            brush.startColor = new Color(0.1331854f, 0.5660378f, 0);
            brush.endColor = new Color(0.1331854f, 0.5660378f, 0);
        }
         if (colorName == "Green3")
        {
            brush.startColor = new Color(0, 1, 0.5713837f);
            brush.endColor = new Color(0, 1, 0.5713837f);
        }
         if (colorName == "Blue1")
        {
            brush.startColor = new Color(0, 255, 224);
            brush.endColor = new Color(0, 255, 224);
        }
         if (colorName == "Blue2")
        {
            brush.startColor = new Color(0, 0.7287984f, 1);
            brush.endColor = new Color(0, 0.7287984f, 1);
        }
         if (colorName == "Blue3")
        {
            brush.startColor = Color.blue;
            brush.endColor = Color.blue;
        }
         if (colorName == "Purpule")
        {
            brush.startColor = new Color(0.5982585f, 0, 1);
            brush.endColor = new Color(0.5982585f, 0, 1);
        }
         if (colorName == "Black")
        {
            brush.startColor = Color.black;
            brush.endColor = Color.black;
        }
         if (colorName == "White")
        {
            brush.startColor = Color.white;
            brush.endColor = Color.white;
        }
    }
}
