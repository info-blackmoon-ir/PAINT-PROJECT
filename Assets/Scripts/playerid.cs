using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerid : MonoBehaviourPunCallbacks
{

    public bool IsMine, IsMasterClient;

    public string UserName;
    private void Awake()
    {
        this.gameObject.name = photonView.Owner.ActorNumber + " - " + photonView.Owner.NickName;
        if (photonView.IsMine)
        {
            IsMine = true;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        UserName = photonView.Owner.NickName;
    }

    private void Update()
    {
        if (photonView.Owner.IsMasterClient)
        {
            IsMasterClient = true;
        }
    }
}
