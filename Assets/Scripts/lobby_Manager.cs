using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon;
using Photon.Pun;

public class lobby_Manager : MonoBehaviour
{
    const string MAP_PROP_KEY = "Mode";
    [SerializeField]
    private GameObject players_title , Starting_Panel;
    [SerializeField]
    private GameObject list;
    [SerializeField]
    private TextMeshProUGUI numberofPlayers_TXT;
    public GameObject[] indexlist;
    public List<string> Players;
    public int NumberOfPlayers , MaxPlayers;

    public bool ismatchstarting  , start_the_match;
   
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        NumberOfPlayers = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NumberOfPlayers != PhotonNetwork.CurrentRoom.PlayerCount)
        {
            indexlist = GameObject.FindGameObjectsWithTag("lists");
                for (int i = 0; i < indexlist.Length; i++)
                {
                    Destroy(indexlist[i]);
                }

            Players.Clear();
            foreach (var player in PhotonNetwork.CurrentRoom.Players)
            {

                Players.Add(player.Value.NickName);
                GameObject go = Instantiate(players_title, transform.position, Quaternion.identity) as GameObject;
                go.name = player.Value.NickName;
                
                go.GetComponent<TextMeshProUGUI>().text = Players.Count + " - " + player.Value.NickName;
                go.transform.parent = list.transform;
                go.transform.localScale = new Vector3(1, 1, 1);
            }
            NumberOfPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
           
        }

        if(PhotonNetwork.CurrentRoom.MaxPlayers == NumberOfPlayers && !start_the_match)
        {
            start_the_match = true;
            ismatchstarting = false;
            
                      
        }
        if (!ismatchstarting && start_the_match)
        {
            StartCoroutine(StartMatch());
        }

        numberofPlayers_TXT.text = "( " + NumberOfPlayers + " / " + PhotonNetwork.CurrentRoom.MaxPlayers + " ) Player";
    }


    IEnumerator StartMatch()
    {
        ismatchstarting = true;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        Debug.Log("Room has been Closed");
        yield return new WaitForSeconds(1.5f);
        Starting_Panel.SetActive(true);
        for (int i = 0; i < Players.Count; i++)
        {
            Match_Details.instance.Players.Add(Players[i]);
        }
        //TODO Enable Loading
        Debug.Log("Going To Match...");
        yield return new WaitForSeconds(4);
        if(PlayerPrefs.GetString(MAP_PROP_KEY) == "Round")
        {
            PhotonNetwork.LoadLevel("TestScene");
            Debug.Log("Loading Level : Round");
        }
        else if (PlayerPrefs.GetString(MAP_PROP_KEY) == "Score")
        {
            PhotonNetwork.LoadLevel("Game");
            Debug.Log("Loading Level : Score");
        }
        else if (PlayerPrefs.GetString(MAP_PROP_KEY) == "Time")
        {
            PhotonNetwork.LoadLevel("Game");
            Debug.Log("Loading Level : Time");  
        }
        
        Debug.Log("Loading Level...");
    }
}
