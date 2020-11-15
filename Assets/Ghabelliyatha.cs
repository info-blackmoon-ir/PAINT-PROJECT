using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Ghabelliyatha : MonoBehaviour
{
    [SerializeField] Button[] keyboards;
    public Text[] texts;
    [SerializeField] StateManager stateManager;
    [SerializeField] GameObject deletebutton_OBJ,Showbutton_OBJ;
    [SerializeField] GameObject[] boxes;
    bool isdelettime;
    [SerializeField] TextMeshProUGUI money_TXT;
    // Start is called before the first frame update
    const string playerNamePrefKey = "PlayerName";
    private string hash = "0cxqsk#ns5|0)";
    string playername;
    private void Start()
    {
        playername = PlayerPrefs.GetString(playerNamePrefKey);
        StartCoroutine(GetMoney("http://blackmoon.ir/webserver/paint/getmoney.php"));
    }



    #region Web_Services
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
          
                money_TXT.text = uwr.downloadHandler.text;
            



        }
    }


    
    IEnumerator SetMoney(string url,string plus,string money)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", playername);
        form.AddField("hash", hash);
        form.AddField("money", money);
        form.AddField("plus", plus);

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
                StartCoroutine(GetMoney("http://blackmoon.ir/webserver/paint/getmoney.php"));
            }
        }
    }

    #endregion

    public void DeleteonWord()
    {
        isdelettime = true;
        Debug.Log("started");
        
        int ra = Random.Range(1, keyboards.Length -1);
        Debug.Log(ra);
        char[] words = stateManager.The_Word.ToCharArray();
        char[] textchar = texts[ra].text.ToCharArray();
        foreach (var word in words)
        {
            if (isdelettime)
            {
                if (word != textchar[0])
                {
                    Debug.Log(word);
                    isdelettime = false;
                    deletebutton_OBJ.SetActive(false);
                    keyboards[ra].interactable = false;
                    return;
                }
            }
            
        }
       
        

    }

    public void moneyback(string _money)
    {
        StartCoroutine(SetMoney("http://blackmoon.ir/webserver/paint/setmoney.php", "1", _money));
    }
    public void ResetButtons()
    {
        for (int i = 0; i < keyboards.Length; i++)
        {
            keyboards[i].interactable = true;
        }
    }

    public void ShowWord()
    {
        boxes = GameObject.FindGameObjectsWithTag("box");
        int ra = Random.Range(1, boxes.Length - 1);
        char[] words = stateManager.The_Word.ToCharArray();
        boxes[ra].GetComponentInChildren<TextMeshProUGUI>().text = words[ra].ToString();
        Showbutton_OBJ.SetActive(false);
        StartCoroutine(SetMoney("http://blackmoon.ir/webserver/paint/setmoney.php", "1", "10"));
    }
}
