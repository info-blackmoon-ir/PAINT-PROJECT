using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class testwebserver : MonoBehaviour
{
    const string playerNamePrefKey = "PlayerName";
    string playername;
    // Start is called before the first frame update
    void Start()
    {
     
        playername = PlayerPrefs.GetString(playerNamePrefKey);
        if(playername == null)
        {
            SceneManager.LoadScene("Register");
        }
        else
        {
            StartCoroutine(WebserverTesting("http://blackmoon.ir/webserver/paint/Test.php"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TestWEBBTN()
    {
        
    }

    IEnumerator WebserverTesting(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", playername);
        

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
            if (uwr.downloadHandler.text == "0")
            {
                Debug.Log("User is Registered go to Menu");
                SceneManager.LoadScene("Menu");

            }
            else if(uwr.downloadHandler.text == "1")
            {
                Debug.Log("User is not found or login is unsuccessfull go to login page");
                SceneManager.LoadScene("Register");
            }
            else if (uwr.downloadHandler.text == "2")
            {
                Debug.Log("User is null go to login page");
                SceneManager.LoadScene("Register");
            }

        }
    }
}
