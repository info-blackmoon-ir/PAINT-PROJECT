using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintable : MonoBehaviour
{
    public Camera nonVRCamera;
    public GameObject brushPrefab;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Left mouse clicked");
            RaycastHit hit;
            Ray ray = nonVRCamera.ScreenPointToRay(Input.mousePosition);
            Debug.Log(Input.mousePosition.x);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("TES");
                if (hit.transform.name == "Board")
                {
                    Debug.Log("LL");
                    Instantiate(brushPrefab, hit.point, Quaternion.identity);
                }
            }
        }
    }

   
}
