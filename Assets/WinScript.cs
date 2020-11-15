using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;

public class WinScript : MonoBehaviour
{
    [SerializeField]
    StateManager manager;
    string playername, score;
    [SerializeField]
    TextMeshProUGUI score_TXT;
    string hash = "0cxqsk#ns5|0)";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetScore("http://blackmoon.ir/webserver/paint/setscore.php"));
    }

    // Update is called once per frame
    

    public void ScoreBTN()
    {
        SceneManager.LoadScene("Menu");
    }

    IEnumerator SetScore(string url)
    {
        //yield return new WaitForSeconds(0.3f);
        playername = manager.MyPlayerUsername;
       
        score = score_TXT.text;
        
        WWWForm form = new WWWForm();
        form.AddField("username", playername);
        form.AddField("score", score);
        form.AddField("hash", hash);
        form.AddField("plus", "0");


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
            if( uwr.downloadHandler.text == "0")
            {
                //
            }
            else
            {
                Debug.Log(uwr.downloadHandler.text);
                //SceneManager.LoadScene("Offline");
            }


        }
    }
}
