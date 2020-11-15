using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;
using TMPro;

public class StateManager : MonoBehaviourPunCallbacks
{
    
    const string MAP_PROP_KEY = "Mode";
    public PhotonView pv;
    string GameMode_String;
    public int MaxRound, CurrentRound;
    [SerializeField]
    private GameObject Word_Chooser , PlayerPrefab,Playerlists_Parent,final_Alert_OB,ScoreBoard_Go,Score_Init_Profile,ScoreBoard_Content_Parent,WinScreen_Panel,WinButton;
    [SerializeField]
    private TextMeshProUGUI time_TXT,title_TT,Round_TXT,Waiting_Text_TXT, Round_Alert_TXT,WinTime_TXT,WinRound_TXT,WinPrize_TXT;
    [SerializeField]
    private GameObject WaitingforChoose_Panel, WaitingForSelect_Panel, WaitingForSelect_SelectionPanel, TimeHasFinish_Panel, NextRound_Panel, ScorePanel;
    private GameManager manager;
    PaintChoose paintChoose;
    public string The_Word,MyPlayerUsername;
    
    public int NumberofPlayers,My_PlayerID;
    public string Current_Player_ID;
    public List<string> PlayersList , AllPlayers;
    [SerializeField]
    List<PlayerUsers> PlayersScore = new List<PlayerUsers>();
    public bool isGameInit, istimetoChoose ,istimetoPaint;
    public playerid My_Profile;
    private int chooseindex,answerindex,Final_score;
    private int Saved_Time,Played_Time;

    float waitingtimeforchoose = 10;
    float waitingtimeforpaint = 20;
    float waitingtimeforpaintindex;
    [SerializeField]
     Slider choosingslider, waitingslider,paintingslider;

    [SerializeField]
    private GameObject Board_GO,BrushPanel_GO,Keyboard_GO,TheBox_Parent,The_Box,DontToch_Go;
    [SerializeField]
    private GameObject[] liststodeactive;
    [SerializeField]
    private FreeDraw.Drawable drawable;
    public enum CurrentState { None, WaitingforChoose,WaitingForSelect,inGame,TimeHasFinish,NextRound,EndGame,Empty};
    CurrentState state;
    private void Awake()
    {
        if (PlayerPrefs.GetString(MAP_PROP_KEY) == "Round")
        {
            GameMode_String = "Round";
        }
        else if (PlayerPrefs.GetString(MAP_PROP_KEY) == "Score")
        {
            GameMode_String = "Score";
        }
        else if (PlayerPrefs.GetString(MAP_PROP_KEY) == "Time")
        {
            GameMode_String = "Time";
        }
        else if (PlayerPrefs.GetString(MAP_PROP_KEY) == "Hads")
        {
            GameMode_String = "Hads";
        }
        else
        {
            Debug.LogError("Somthing Went Wrong");
            Debug.LogError("Somthing Went Wrong");
            Debug.LogError("Somthing Went Wrong");
        }
        if(GameMode_String == "Round")
        {

            MaxRound = 10;

        }else if(GameMode_String == "Score")
        {
            MaxRound = 99;
        }
        else if (GameMode_String == "Time")
        {
            MaxRound = 99;
        }
        else if (GameMode_String == "Hads")
        {
            MaxRound = 10;
        }

        CurrentRound = 0;
        manager = GameObject.FindObjectOfType<GameManager>();
        GameObject go = PhotonNetwork.Instantiate(PlayerPrefab.name, transform.position, Quaternion.identity) as GameObject;
        go.transform.parent = Playerlists_Parent.transform;
        NumberofPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        Current_Player_ID = "0";
    }
    private void Start()
    {
        
        state = CurrentState.None;
        for (int i = 0; i < Match_Details.instance.Players.Count; i++)
        {
            AllPlayers.Add(Match_Details.instance.Players[i]);
            PlayersList.Add(Match_Details.instance.Players[i]);
            PlayersScore.Add(new PlayerUsers(Match_Details.instance.Players[i],0));
        }
        GameObject[] go = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < go.Length; i++)
        {
            if (go[i].GetComponent<playerid>().IsMine)
            {
                My_Profile = go[i].GetComponent<playerid>();
            }
            else
            {
                //ERROR
            }
        }
        paintChoose = GameObject.FindObjectOfType<PaintChoose>();
        foreach (var player in PlayersScore)
        {
            Debug.Log(player.score + "  " + player.name);
        }
        Played_Time = (int)PhotonNetwork.Time;
        BrushPanel_GO.SetActive(false);
        waitingtimeforpaintindex = waitingtimeforpaint;
        
    }

    private void Update()
    {
        if(Current_Player_ID == My_Profile.UserName)
        {
            DontToch_Go.SetActive(false);
            //drawable.isDrawAllowed = true;
            Board_GO.transform.localScale = new Vector3(1f,1f, 1f);
            Board_GO.transform.localPosition = new Vector3(0, 0f, 0);
            Keyboard_GO.SetActive(false);
            BrushPanel_GO.SetActive(true);
            for (int i = 0; i < liststodeactive.Length; i++)
            {
                liststodeactive[i].SetActive(false);
            }
            drawable.isDrawAllowed = true;
        }
        else
        {
            //drawable.isDrawAllowed = false;
            DontToch_Go.SetActive(true);
            Board_GO.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            Board_GO.transform.localPosition = new Vector3(0, 3f, 0);
            BrushPanel_GO.SetActive(false);
            
            for (int i = 0; i < liststodeactive.Length; i++)
            {
                liststodeactive[i].SetActive(true);
            }
            drawable.isDrawAllowed = false;
        }
        if (paintingslider)
        {
            paintingslider.value -= Time.deltaTime;
        }
        if (My_Profile.IsMasterClient)
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount == 1 || PhotonNetwork.CurrentRoom.PlayerCount == 0)
            {
                //Just One Player Stay Here Please Win 
            }
            if (state == CurrentState.None)
            {
                photonView.RPC("SetCurrentPlayer", RpcTarget.AllBuffered, "0");
                state = CurrentState.WaitingforChoose;
                Debug.Log(state);
            }
            else if (state == CurrentState.WaitingforChoose)
            {
                photonView.RPC("ResetButtons", RpcTarget.AllBuffered);
                answerindex = 0;
                photonView.RPC("DeletePainting", RpcTarget.AllBufferedViaServer);
                photonView.RPC("roundplus", RpcTarget.AllBuffered);
                istimetoPaint = false;
                istimetoChoose = false;
                photonView.RPC("ActiveSelectionPanel", RpcTarget.AllBuffered, Current_Player_ID, "0");
                photonView.RPC("Active_WaitingforChoose_Panel", RpcTarget.AllBuffered, true);
                state = CurrentState.Empty;
                StartCoroutine(SetRPCCall("SetCurrentPlayer", 2, ""));
                Debug.Log(state);
                waitingtimeforpaint = waitingtimeforpaintindex;
            }
            else if(state == CurrentState.WaitingForSelect)
            {
                
                Saved_Time = (int)PhotonNetwork.Time;
                istimetoChoose = true;
                photonView.RPC("ActiveSelectionPanel", RpcTarget.AllBuffered, Current_Player_ID, "1");
                photonView.RPC("Active_WaitingforChoose_Panel", RpcTarget.AllBuffered, false);
                state = CurrentState.Empty;
                CalculatingChoosingTime(Current_Player_ID);
                Debug.Log(state);

            }
            else if(state == CurrentState.inGame)
            {
                
                photonView.RPC("InitTheBoxes", RpcTarget.AllBuffered);
                photonView.RPC("ResetTimeSlider", RpcTarget.AllBuffered);
                Saved_Time = (int)PhotonNetwork.Time;


               

                istimetoPaint = true;
                istimetoChoose = false;
                state = CurrentState.Empty;
                Debug.Log(state);
                //Game is On the Road
                photonView.RPC("InGamePublic", RpcTarget.AllBuffered);
            }
            else if(state == CurrentState.TimeHasFinish)
            {
                istimetoPaint = false;
                istimetoChoose = false;
                photonView.RPC("ScoreBoardInit", RpcTarget.AllBuffered);
                //state = CurrentState.WaitingforChoose;
                Debug.Log(state);
                //Players Time Has Finished
                //Current Word is ...
                state = CurrentState.Empty;
            }
            else if(state == CurrentState.NextRound)
            {
                if (GameMode_String == "Round")
                {
                    if (CurrentRound <= MaxRound)
                    {
                        state = CurrentState.WaitingforChoose;
                    }
                    else
                    {
                        state = CurrentState.EndGame;
                    }
                }
                else if (GameMode_String == "Score")
                {
                    foreach (var player in PlayersScore)
                    {
                        if(player.score < 20)
                        {
                            state = CurrentState.WaitingforChoose;
                        }
                        else
                        {
                            state = CurrentState.EndGame;
                            return;
                        }
                    }
                    
                }
                else if (GameMode_String == "Time")
                {
                    if ((int)PhotonNetwork.Time <= Played_Time + 30)
                    {
                        state = CurrentState.WaitingforChoose;
                    }
                    else
                    {
                        state = CurrentState.EndGame;
                    }
                }
                else if (GameMode_String == "Hads")
                {
                    if (CurrentRound <= MaxRound)
                    {
                        state = CurrentState.WaitingforChoose;
                    }
                    else
                    {
                        state = CurrentState.EndGame;
                    }
                }
                Debug.Log(state);
                
                //Next Round is started
                //Lets Go to  state = CurrentState.WaitingforChoose;
            }else if(state == CurrentState.EndGame)
            {
                Debug.Log(state);
                photonView.RPC("Final_Win", RpcTarget.All);
                state = CurrentState.Empty;

            }
            if (istimetoChoose && state == CurrentState.Empty)
            {
                if((int)PhotonNetwork.Time == Saved_Time + waitingtimeforchoose)
                {
                    state = CurrentState.WaitingforChoose;
                }
                
            }
            if (istimetoPaint && state == CurrentState.Empty)
            {
                if (((int)PhotonNetwork.Time == Saved_Time + waitingtimeforpaint) || answerindex == PlayersList.Count)
                {
                    state = CurrentState.TimeHasFinish;
                }
                //state = CurrentState.TimeHasFinish;
            }

            
            

        }
        if(Current_Player_ID == "0")
        {
            //Waiting For Server to Choosing a Player
        }else if(Current_Player_ID == "1")
        {
            //Next Round is Starting
        }
        time_TXT.text = (PhotonNetwork.Time).ToString();
        
        if(My_Profile == null)
        {
            Debug.LogError("ERROR IN PLAYERID");
            return;
        }
        
        
        if (!DoListsMatch(PlayersList, AllPlayers))
        {
            //SomePlayers Are Left The Game
            // Debug.LogWarning("SomePlayers Are Left The Game");
                foreach (var online in PhotonNetwork.CurrentRoom.Players)
                {
                    if (!AllPlayers.Contains(online.Value.NickName))
                    {
                        Debug.Log(AllPlayers + "Left the Room");
                    }
                }
            

        }

        #region Sliders
        if (WaitingForSelect_SelectionPanel.active == true)
        {
            choosingslider.value -= Time.deltaTime;
           
        }
        else
        {
            choosingslider.value = 10;
        }

        if (WaitingForSelect_Panel.active == true)
        {
            waitingslider.value -= Time.deltaTime;
            Waiting_Text_TXT.text = "Waiting For " + Current_Player_ID + " To Choose a Word";

        }
        else
        {
            waitingslider.value = 10;
        }

        #endregion
        if (NumberofPlayers != PhotonNetwork.CurrentRoom.PlayerCount)
        {
            PlayersList.Clear();
            foreach (var player in PhotonNetwork.CurrentRoom.Players)
            {
                PlayersList.Add(player.Value.NickName);
            }
            NumberofPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        }
    }

    private bool DoListsMatch(List<string> list1, List<string> list2)
    {
        var areListsEqual = true;

        if (list1.Count != list2.Count)
            return false;

        list1.Sort(); // Sort list one
        list2.Sort(); // Sort list two

        for (var i = 0; i < list1.Count; i++)
        {
            if (list2[i] != list1[i])
            {
                areListsEqual = false;
            }
        }

        return areListsEqual;
    }

    [PunRPC]
    public void SetCurrentPlayer(string id)
    {
        Current_Player_ID = id;
    }
    
    [PunRPC]
    public void SetActiveChoosingPanel()
    {
        istimetoChoose = true;
    }

    [PunRPC]
    public void ActiveSelectionPanel(string id,string ip)
    {
        if(id == My_Profile.UserName)
        {
            if (ip == "1")
            {
                WaitingForSelect_SelectionPanel.SetActive(true);
                
                //Active My Choosing Panel

            }
            else if(ip == "0")
            {
                WaitingForSelect_SelectionPanel.SetActive(false);
                //DeActive Panel
            }
        }
        else
        {
            if (ip == "1")
            {
                //waitingslider.value -= Time.deltaTime;
                WaitingForSelect_Panel.SetActive(true);
                //Active My Waiting Panel
            }
            else if (ip == "0")
            {
                WaitingForSelect_Panel.SetActive(false);
                //DeActive Panel
            }
            // StartCoroutine(SetRPCCallforChoosing())
        }
    }

    [PunRPC]
    void InGamePublic()
    {
        StartCoroutine(GameInit());
    }

    
    IEnumerator GameInit()
    {
        WaitingForSelect_SelectionPanel.SetActive(false);
        WaitingForSelect_Panel.SetActive(false);
        yield return new WaitForSeconds(2);
        //Active Panel
        //Active Chat
        //Active Timer
    }
    public void Word_Chooser_BTN(string objective)
    {
        photonView.RPC("ChooseWord", RpcTarget.AllBuffered, objective);
    }
    [PunRPC]
    public void ChooseWord(string word)
    {
        The_Word = word;

        paintChoose.KeyBoardGenerate(The_Word);
        //Close All Panels
        if (My_Profile.IsMasterClient)
        {
            state = CurrentState.inGame;
        }
    }

    void CalculatingChoosingTime(string id)
    {
        if (!My_Profile.IsMasterClient)
        {
            return;
        }
        

        
    }


    void Finishedwaitingforchoosing(string userid)
    {
        //Open Info Panel
        //This player is Finished
        Debug.Log("Player  " + userid + "Don't Choose Anything Let's Go For Another Person");

        //Close All Panels

    }


    IEnumerator SetRPCCall(string method,float time,string value)
    {
        state = CurrentState.Empty;
        value = PlayersList[chooseindex];
        
        if (chooseindex == PhotonNetwork.CurrentRoom.PlayerCount - 1)
        {
            chooseindex = 0;
        }
        else
        {
            chooseindex++;
        }
        yield return new WaitForSeconds(time);
        photonView.RPC(method, RpcTarget.AllBuffered, value);
        state = CurrentState.WaitingForSelect;
    }

    [PunRPC]
    void Active_WaitingforChoose_Panel(bool s)
    {
        WaitingforChoose_Panel.SetActive(s);
    }

    public void MessegeBTN()
    {
        photonView.RPC("MessegeSend", RpcTarget.Others, My_Profile.UserName, manager.chatBox, Message.MessageType.playerMessage);
    }

    [PunRPC]
    void MessegeSend(string username,string messege,Message.MessageType type)
    {
        manager.SendMessageToChat(username +" :"+ messege, type);
        manager.AddText();

    }


    #region Round_Propert
    [PunRPC]
    void roundplus()
    {
        CurrentRound++;
        Round_TXT.text = CurrentRound + " / " + MaxRound;
        Round_Alert_TXT.text = "Round " + CurrentRound.ToString();
        if(CurrentRound == MaxRound)
        {
            //Final Round
            StartCoroutine(Final_Round_Alert());
        }else if(CurrentRound > MaxRound)
        {
            //End Of The Game
        }
        
    }
   
    IEnumerator Final_Round_Alert()
    {
        yield return new WaitForSeconds(2);
        final_Alert_OB.SetActive(true);
        yield return new WaitForSeconds(5);
        final_Alert_OB.SetActive(false);
    }
    [PunRPC]
    void InitTheBoxes()
    {
        Keyboard_GO.SetActive(true);
        GameObject[] boxgo = GameObject.FindGameObjectsWithTag("box");
        for (int i = 0; i < boxgo.Length; i++)
        {
            Destroy(boxgo[i]);
        }
        for (int i = 0; i < The_Word.Length; i++)
        {
            GameObject go = Instantiate(The_Box, transform.position, Quaternion.identity) as GameObject;
            
            go.transform.parent = TheBox_Parent.transform;
            
            go.GetComponentInChildren<TextMeshProUGUI>().text = "";
            go.transform.localScale = new Vector3(1, 1, 1);
            go.transform.position = new Vector3(0, 0, 0);
            go.transform.localPosition = new Vector3(0, 0, 0);
            
        }
    }
    #endregion

    #region WinProperty
    public void Win(string userid,int score)
    {
       int index = PlayersScore.FindIndex(PlayersScore => PlayersScore.name == userid);
        PlayersScore[index].score = PlayersScore[index].score + score;
        UpdateScores(userid, PlayersScore[index].score.ToString());
        if(userid == PhotonNetwork.NickName)
        {
            Keyboard_GO.SetActive(false);
        }
        
    }

    void UpdateScores(string userid,string _score)
    {
        Score_Init[] scores = GameObject.FindObjectsOfType<Score_Init>();
        foreach (var score_in in scores)
        {
            if(score_in.Playername.text == userid+ "  :")
            {
                score_in.Playerscore.text = _score;
            }
        }
    }
    [PunRPC]
    void Final_Win()
    {
        StartCoroutine(WinCorutine());
    }

    IEnumerator WinCorutine()
    {
        yield return new WaitForSeconds(1);
        WinScreen_Panel.SetActive(true);
        WinButton.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        WinTime_TXT.text = ((int)(Time.timeSinceLevelLoad)).ToString();
        int index = PlayersScore.FindIndex(PlayersScore => PlayersScore.name == PhotonNetwork.NickName);
        yield return new WaitForSeconds(0.3f);
        WinPrize_TXT.text = PlayersScore[index].score.ToString();
        yield return new WaitForSeconds(0.3f);
        WinRound_TXT.text = MaxRound.ToString();
        MyPlayerUsername = PhotonNetwork.NickName;
        WinButton.SetActive(true);
    }

    #endregion

    #region ScoreBoard

    [PunRPC]
    void ScoreBoardInit()
    {
        StartCoroutine(score_Spawn());
    }

    IEnumerator score_Spawn()
    {
        yield return new WaitForSeconds(2);
        ScoreBoard_Go.SetActive(true);
        yield return new WaitForSeconds(1);
        foreach (var player in PlayersScore)
        {
            GameObject go = Instantiate(Score_Init_Profile, transform.position, Quaternion.identity, ScoreBoard_Content_Parent.transform) as GameObject;
            go.GetComponent<RectTransform>().localPosition = new Vector3(go.GetComponent<RectTransform>().position.x, go.GetComponent<RectTransform>().localPosition.y, 0);
            
            go.name = player.name;
            go.transform.localScale = new Vector3(1, 1, 1);
            ScoreBoard scoreBoard = go.GetComponent<ScoreBoard>();
            scoreBoard.score.text = player.score.ToString();
            scoreBoard.title.text = player.name;
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(5);
        if (PhotonNetwork.IsMasterClient)
        {
            if(CurrentRound < MaxRound)
            {
                state = CurrentState.NextRound;
            }
            else if(CurrentRound == MaxRound)
            {
                //Game is Finished
                state = CurrentState.EndGame;
            }
        }
        
        GameObject[] scores = GameObject.FindGameObjectsWithTag("score");
        for (int i = 0; i < scores.Length; i++)
        {
            Destroy(scores[i]);
        }
        ScoreBoard_Go.SetActive(false);
        drawable.ResetCanvas();

    }
    #endregion

    #region brushProperty

    [PunRPC]
    void DeletePainting()
    {
        drawable.ResetCanvas();
        Debug.Log("Reset Board");
    }

    public void DeleteButtonsBTN()
    {
        photonView.RPC("DeleteButtons", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void DeleteButtons()
    {
        Ghabelliyatha ghabelliyatha = GameObject.FindObjectOfType<Ghabelliyatha>();
        ghabelliyatha.DeleteonWord();
    }
    [PunRPC]
    void ResetButtons()
    {
        Ghabelliyatha ghabelliyatha = GameObject.FindObjectOfType<Ghabelliyatha>();
        ghabelliyatha.ResetButtons();

    }
    #endregion

    #region ExtraTime

    public void GiveExtraTimeBTN()
    {
        photonView.RPC("GiveExtraTime", RpcTarget.AllBufferedViaServer);
    }
    [PunRPC]
    void GiveExtraTime()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Time was " + waitingtimeforpaint);
            waitingtimeforpaint = waitingtimeforpaint + 10;
            Debug.Log("and now " + waitingtimeforpaint);
        }
        else
        {
            
        }
        paintingslider.value += 10;
    }

    [PunRPC]
    void ResetTimeSlider()
    {
        paintingslider.value = waitingtimeforpaint;
    }
    #endregion
}
