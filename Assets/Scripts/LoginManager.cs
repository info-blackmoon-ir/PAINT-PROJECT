using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [SerializeField]
    GameObject loader;
    [SerializeField]
    Button registerBTN;
    [SerializeField]
    Text err;
   public string username, password, cpassword, fullname;
    bool ispasswordcorrect, isregistrable;
    const string playerNamePrefKey = "PlayerName";
    [SerializeField]
    TMP_InputField userinput, passinput, cpassinput, fullnameinput, loginpassword, loginusername;

    public void ONLoginUsernameChange()
    {
        username = loginusername.text;
    }
    public void OnLoginPasswordChange()
    {
        password = loginpassword.text;
    }
    public void LoginBTN()
    {
        loader.SetActive(true);
        if (username != "" && password != "")
        {
            StartCoroutine(LoginWithUserNameAndPassword("http://blackmoon.ir/webserver/paint/login.php"));
        }
        else
        {
            loader.SetActive(false);

        }
    }

    IEnumerator LoginWithUserNameAndPassword(string url)
    {
        yield return new WaitForSeconds(2f);
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("pass", password);




        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            Debug.Log("User is offline or server is down");
            SceneManager.LoadScene("Splash");
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            if (uwr.downloadHandler.text == "0")
            {
                err.text = "User is logged in go to Menu";
                err.color = Color.green;
                Debug.Log("User is Registered go to Menu");
                UnityEngine.SceneManagement.SceneManager.LoadScene("Splash");
                PlayerPrefs.SetString(playerNamePrefKey, username);
            }
            else if(uwr.downloadHandler.text == "1")
            {
                err.text = "Username or Password is Wrong!";
                err.color = Color.green;
                Debug.Log("Username or Password is Wrong");
                loader.SetActive(false);

            }
        }
    }
    public void OnUsernameChanged()
    {
        
        if (userinput.text.Length >= 5)
        {
            username = userinput.text;
            err.text = "";
            isregistrable = true;
            if (username != null && password != null && cpassword != null && fullname != null && isregistrable)
            {
                registerBTN.interactable = true;
            }
            else
            {
                registerBTN.interactable = false;
            }
        }
        else
        {
            isregistrable = false;
            err.text = "User Must Be More Than 5 Character";
        }
    }
    public void OnNameChanged()
    {
        if (fullnameinput.text.Length >= 2)
        {
            fullname = fullnameinput.text;
            err.text = "";
            isregistrable = true;
            if (username != null && password != null && cpassword != null && fullname != null && isregistrable)
            {
                registerBTN.interactable = true;
            }
            else
            {
                registerBTN.interactable = false;
            }
        }
        else
        {
            isregistrable = false;
            err.text = "name must be valid";
        }
    }
    public void OnPasswordChanged()
    {
        ispasswordcorrect = false;
        if (passinput.text.Length >= 8)
        {
            password = passinput.text;
            err.text = "";
            isregistrable = true;
            if (username != null && password != null && cpassword != null && fullname != null && isregistrable)
            {
                registerBTN.interactable = true;
            }
            else
            {
                registerBTN.interactable = false;
            }
        }
        else
        {
            isregistrable = false;
            err.text = "name must be valid";
        }
    }

    public void OnPasswordCorrectionChanged()
    {
        cpassword = cpassinput.text;
        if (cpassword == password)
        {
            ispasswordcorrect = true;
            err.text = "";
            isregistrable = true;
            if (username != null && password != null && cpassword != null && fullname != null && isregistrable)
            {
                registerBTN.interactable = true;
            }
            else
            {
                registerBTN.interactable = false;
            }
        }
        else
        {
            isregistrable = false;
            ispasswordcorrect = false;
            err.text = "Password Must Be Match";
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        loader.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterButton()
    {
        loader.SetActive(true);
        StartCoroutine(RegisterWithUsernameAndPassword("http://blackmoon.ir/webserver/paint/register.php"));
    }

    IEnumerator RegisterWithUsernameAndPassword(string url)
    {
        yield return new WaitForSeconds(2f);
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("pass", cpassword);
        form.AddField("fname", fullname);
        


        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            Debug.Log("User is offline or server is down");

        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            if (uwr.downloadHandler.text == "0")
            {
                err.text = "User is Registered go to Menu";
                err.color = Color.green;
                Debug.Log("User is Registered go to Menu");
                UnityEngine.SceneManagement.SceneManager.LoadScene("Splash");
                PlayerPrefs.SetString(playerNamePrefKey, username);
            }
            else if (uwr.downloadHandler.text == "1")
            {
                err.text = "User is Registered Before";
                Debug.Log("User is Registered Before");
                loader.SetActive(false);
            }
            else if (uwr.downloadHandler.text == "null")
            {
                err.text = "User is not found or login is unsuccessfull go to login page";
                Debug.Log("User is not found or login is unsuccessfull go to login page");
                loader.SetActive(false);
            }
            else if (uwr.downloadHandler.text == "2")
            {
                SceneManager.LoadScene("Splash");
                err.text = "User is null go to login page";
                Debug.Log("User is null go to login page");
                loader.SetActive(false);
            }

        }
    }
}
