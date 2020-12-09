using UnityEngine;

public class NewDraw : MonoBehaviour
{
    public Camera m_camera;
    public GameObject brush;
    public LayerMask Drawing_Layers;

    LineRenderer currentLineRenderer;

    Vector2 lastPos;

    private void FixedUpdate()
    {
        Vector2 mouse_world_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Check if the current mouse position overlaps our image
        Collider2D hit = Physics2D.OverlapPoint(mouse_world_position, Drawing_Layers.value);
        if (hit != null && hit.transform != null /*&& !iswaitforsec*/ /*&& isDrawAllowed*/)
        {
            Drawing();
        }
    }

    void Drawing()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CreateBrush();
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            PointToMousePos();
        }
        else
        {
            currentLineRenderer = null;
        }
    }

    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        //because you gotta have 2 points to start a line renderer, 
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);

    }

    void AddAPoint(Vector2 pointPos)
    {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    void PointToMousePos()
    {
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        if (lastPos != mousePos)
        {
            AddAPoint(mousePos);
            lastPos = mousePos;
        }
    }

}