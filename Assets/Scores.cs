using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using TMPro;

public class Scores : MonoBehaviour
{
    StateManager state;
    playerid id;
    public GameObject scoreinit;

    [SerializeField]
    private GameObject scoreParent;
    public bool isListInit = false;
    private void Awake()
    {
        state = GameObject.FindObjectOfType<StateManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < go.Length; i++)
        {
            if (go[i].GetComponent<playerid>().IsMine)
            {
                id = go[i].GetComponent<playerid>();
            }
            else
            {
                //ERROR
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {

        if (!isListInit)
        {
            foreach (var player in state.AllPlayers)
            {
                GameObject pro = Instantiate(scoreinit, transform.position, Quaternion.identity);
                pro.name = player;

                pro.transform.parent = scoreParent.transform;
                pro.GetComponent<Score_Init>().Playername.text = player + "  :";
                pro.GetComponent<Score_Init>().Playerscore.text = "0";
                pro.transform.localScale = new Vector3(1, 1, 1);
            }
            isListInit = true;

        }
        
    }
}
