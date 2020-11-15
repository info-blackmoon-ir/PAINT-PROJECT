using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;
using TMPro;



public class Profile_Init : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] money_TXT, score_TXT,Ghalb_TXT;
    [SerializeField]
    GameObject friend_Ob,Friends_Parent;
    public List<Friends> Friends_List = new List<Friends>();
    string playername;
    private string hash = "0cxqsk#ns5|0)";
    const string playerNamePrefKey = "PlayerName";
    public string newfriend_user;
    // Start is called before the first frame update
    void Start()
    {
        playername = PlayerPrefs.GetString(playerNamePrefKey);
        StartCoroutine(GetMoney("http://blackmoon.ir/webserver/paint/getscore.php"));
        StartCoroutine(GetScore("http://blackmoon.ir/webserver/paint/getmoney.php"));
        StartCoroutine(GetHurt("http://blackmoon.ir/webserver/paint/hurt.php"));
        StartCoroutine(Get_Friends("http://blackmoon.ir/webserver/paint/getfriends.php"));
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator GetMoney(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", playername);
        form.AddField("hash", hash);


        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            Debug.Log("User is offline or server is down");
            SceneManager.LoadScene("Offline");
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            for (int i = 0; i < score_TXT.Length; i++)
            {
                score_TXT[i].text = uwr.downloadHandler.text;
            }
            
            

        }
    }
    IEnumerator GetScore(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", playername);
        form.AddField("hash", hash);


        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            Debug.Log("User is offline or server is down");
            SceneManager.LoadScene("Offline");
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            for (int i = 0; i < money_TXT.Length; i++)
            {
                money_TXT[i].text = uwr.downloadHandler.text;
            }
            


        }
    }

    IEnumerator GetHurt(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", playername);
        form.AddField("hash", hash);
        form.AddField("code", "2");


        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            Debug.Log("User is offline or server is down");
            SceneManager.LoadScene("Offline");
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            for (int i = 0; i < money_TXT.Length; i++)
            {
                Ghalb_TXT[i].text = uwr.downloadHandler.text;
            }
            if (int.Parse(uwr.downloadHandler.text) < 1)
            {
                Debug.LogWarning("Your Hurt is Finished");
            }



        }
    }
    IEnumerator SetHurt(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", playername);
        form.AddField("hash", hash);
        form.AddField("code", "1");


        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            Debug.Log("User is offline or server is down");
            SceneManager.LoadScene("Offline");
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            if(uwr.downloadHandler.text == "0")
            {
                //play
            }else if(uwr.downloadHandler.text == "2")
            {
                //dont go
            }



        }
    }
    public void Add_FriendBTN()
    {
        newfriend_user = "PokerFace";
        StartCoroutine(Add_Friend("http://blackmoon.ir/webserver/paint/addfriends.php"));
        StartCoroutine(Get_Friends("http://blackmoon.ir/webserver/paint/getfriends.php"));
    }
    IEnumerator Add_Friend(string url)
    {

        List<Friends> _friends = new List<Friends>();
        _friends.Clear();
        for (int i = 0; i < Friends_List.Count; i++)
        {
            _friends.Add(Friends_List[i]);
        }

        if (newfriend_user != null && newfriend_user != "")
        {
            Friends _fr = new Friends(Friends_List.Count, newfriend_user, "LOL");
            _friends.Add(_fr);
        }
     

        yield return new WaitForSeconds(1);

        string json = JsonConvert.SerializeObject(_friends);
       
        WWWForm form = new WWWForm();
        form.AddField("username", playername);
        form.AddField("hash", hash);
        form.AddField("friend", json);
        Debug.Log(json);

        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            Debug.Log("User is offline or server is down");
            SceneManager.LoadScene("Offline");
        }
        else
        {
           // Debug.Log("Received: " + uwr.downloadHandler.text);
            if (uwr.downloadHandler.text == "0")
            {
                Debug.Log("LOL");
            }
            else if (uwr.downloadHandler.text == "2")
            {
                //dont go
            }



        }
    }


    IEnumerator Get_Friends(string url)
    {

        WWWForm form = new WWWForm();
        form.AddField("username", playername);
        form.AddField("hash", hash);

        

        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            Debug.Log("User is offline or server is down");
            SceneManager.LoadScene("Offline");
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            if (uwr.downloadHandler.text != null)
            {
                
               List<Friends> fl = JsonConvert.DeserializeObject<List<Friends>>(uwr.downloadHandler.text);

                foreach (var item in fl)
                {
                    Debug.Log("LOL" + item.id);
                }
                GameObject[] ob = GameObject.FindGameObjectsWithTag("score");
                for (int i = 0; i < ob.Length; i++)
                {
                    
                    Destroy(ob[i]);
                }
                Friends_List.Clear();
                foreach (var item in fl)
                {
                    GameObject go = Instantiate(friend_Ob, transform.position, Quaternion.identity);
                    go.transform.parent = Friends_Parent.transform;
                    go.GetComponentInChildren<TextMeshProUGUI>().text = item.username;
                    Friends_List.Add(item);
                }
                
            }
            else if (uwr.downloadHandler.text == "2")
            {
                //dont go
            }



        }
    }
}
