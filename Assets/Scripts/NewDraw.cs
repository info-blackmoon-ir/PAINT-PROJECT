using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;

public class NewDraw : MonoBehaviourPunCallbacks
{
    public Camera m_camera;
    public GameObject brush;
    public LayerMask Drawing_Layers;
    public bool isDrawAllowed = true;
    LineRenderer currentLineRenderer;

    Vector2 lastPos;

    private void Update()
    {
        Vector2 mouse_world_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Check if the current mouse position overlaps our image
        Collider2D hit = Physics2D.OverlapPoint(mouse_world_position, Drawing_Layers.value);
        if (hit != null && hit.transform != null && isDrawAllowed && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            photonView.RPC("CreateBrush", RpcTarget.AllBuffered, mousePos);
        }
        else if (hit != null && hit.transform != null && isDrawAllowed && Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            //PointToMousePos(mousePos,lastPos);
            photonView.RPC("PointToMousePos", RpcTarget.AllBuffered, mousePos, lastPos);
        }
        else
        {
            //photonView.RPC("NullRenderr", RpcTarget.AllBuffered);
        }
    }

    void Drawing()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            photonView.RPC("CreateBrush", RpcTarget.AllBuffered, mousePos);
            //CreateBrush(mousePos);
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            //PointToMousePos(mousePos,lastPos);
            photonView.RPC("PointToMousePos", RpcTarget.AllBuffered, mousePos, lastPos);
        }
        //if (Input.GetMouseButtonUp(0))
        //{
        //    photonView.RPC("NullRenderr", RpcTarget.AllBuffered);
        //}
        //else
        //{
        //    photonView.RPC("NotNullRenderr", RpcTarget.AllBuffered);
        //}
        //else
        //{
        //    photonView.RPC("NullRenderr", RpcTarget.AllBuffered);
        //}
    }
    [PunRPC]
    void CreateBrush(Vector2 mousePos)
    {
        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        //because you gotta have 2 points to start a line renderer, 
        

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);

    }

    //[PunRPC]
    void AddAPoint(Vector2 pointPos)
    {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }


    [PunRPC]
    void PointToMousePos(Vector2 _mousePos, Vector2 _lastPos)
    {
        
        if (_lastPos != _mousePos)
        {
            AddAPoint(_mousePos);
            _lastPos = _mousePos;
            lastPos = _mousePos;
        }
    }
    
    [PunRPC]
    void NullRenderr()
    {
        currentLineRenderer = null;
    }
    //[PunRPC]
    //void NotNullRenderr()
    //{
    //    currentLineRenderer = currentLineRenderer;
    //}


}