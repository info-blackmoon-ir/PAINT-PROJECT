using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Objects_activator : MonoBehaviourPun
{
    public GameObject[] ob;
    public GameObject statemanager;
    private void Awake()
    {
        for (int i = 0; i < ob.Length; i++)
        {
            ob[i].SetActive(true);
        }
        PhotonNetwork.Instantiate(statemanager.name, transform.position, Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
