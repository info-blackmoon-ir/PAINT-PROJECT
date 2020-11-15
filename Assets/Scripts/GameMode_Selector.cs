using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode_Selector : MonoBehaviour
{
    const string PlayerGameMode = "GAMEMODE";
    [SerializeField]
    GameObject ModeRound, ModeScore, ModeTime;
    GameObject[] Button_Checks;
    

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt(PlayerGameMode) == 0)
        {
            ModeRound.SetActive(true);
            ModeScore.SetActive(false);
            ModeTime.SetActive(false);
            Button_Checks[0].SetActive(true);
            Button_Checks[1].SetActive(false);
            Button_Checks[2].SetActive(false);
        }
        else if(PlayerPrefs.GetInt(PlayerGameMode) == 1)
        {
            ModeRound.SetActive(false);
            ModeScore.SetActive(true);
            ModeTime.SetActive(false);
            Button_Checks[0].SetActive(false);
            Button_Checks[1].SetActive(true);
            Button_Checks[2].SetActive(false);
        }
        else if (PlayerPrefs.GetInt(PlayerGameMode) == 2)
        {
            ModeRound.SetActive(false);
            ModeScore.SetActive(false);
            ModeTime.SetActive(true);
            Button_Checks[0].SetActive(false);
            Button_Checks[1].SetActive(false);
            Button_Checks[2].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectGameMode(int mode)
    {
        PlayerPrefs.SetInt(PlayerGameMode, mode);
    }
}
