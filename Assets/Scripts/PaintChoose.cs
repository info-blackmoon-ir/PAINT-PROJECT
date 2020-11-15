using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PaintChoose : MonoBehaviour
{
    public string[] paintObjectives;
    public string[] paintObjectives2;
    public string[] paintObjectives3;
    public char[] englishChars;
    char[] charArr;
    public Text[] keyboardWords;
    public string choosedObjective;
    public char remainChars;

    GameObject Row1;

    [SerializeField] TextMeshProUGUI button1, button2, button3;
    public GameObject choosePanel;
    public float timeLenght = 10f;

    private bool isAllowed = false;
    public StateManager stateManager;
    void Start()
    {
        ChooseObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAllowed)
        {
            isAllowed = false;
            InvokeRepeating("ChooseObject", timeLenght, timeLenght + 10f);
        }

    }

    public void ChooseObject()
    {
        //for (int i = 0; i < keyboardWords.Length; i++)
        //{
        //    keyboardWords[i].text = ""; 
        //}
        
        button1.text = paintObjectives[Mathf.FloorToInt(Random.Range(0f, paintObjectives.Length))];
        button2.text = paintObjectives2[Mathf.FloorToInt(Random.Range(0f, paintObjectives2.Length))];
        button3.text = paintObjectives3[Mathf.FloorToInt(Random.Range(0f, paintObjectives3.Length))];
    }

    public void ChooseButton1()
    {
        Destroy(GameObject.Find("Brushes"));
        Row1 = new GameObject();
        GameObject Row2 = Instantiate(Row1, Vector3.zero, Quaternion.identity) as GameObject;
        Row2.transform.name = "Brushes";
        isAllowed = true;
        choosePanel.SetActive(false);
       choosedObjective = button1.text;
        stateManager.Word_Chooser_BTN(choosedObjective);
        // choosedObjective = choosedObjective.ToUpperInvariant().ToString();
        charArr = choosedObjective.ToCharArray();
        foreach (char ch in charArr)
        {
            Debug.Log(ch);
        }
        for (int j = 0; j < charArr.Length; j++)
        {
            for (int i = 0; i < englishChars.Length; i++)
            {     
                if (englishChars[i] == charArr[j])
                {
                    keyboardWords[Mathf.FloorToInt(Random.Range(0f, keyboardWords.Length))].text = englishChars[i].ToString();
                }
            }
        }

        for (int i = 0; i < keyboardWords.Length; i++)
        {
            if (keyboardWords[i].text == "")
            {
                for (int j = 0; j < charArr.Length; j++)
                {
                    do
                    {
                        remainChars = englishChars[Mathf.FloorToInt(Random.Range(0f,26f))];
                    } while (remainChars == charArr[j]);
                    
                }
                keyboardWords[i].text = remainChars.ToString();
            }
        }
    }
    public void ChooseButton2()
    {
        Destroy(GameObject.Find("Brushes"));
        Row1 = new GameObject();
        GameObject Row2 = Instantiate(Row1, Vector3.zero, Quaternion.identity) as GameObject;
        Row2.transform.name = "Brushes";
        isAllowed = true;
        choosePanel.SetActive(false);
        choosedObjective = button2.text;
        stateManager.Word_Chooser_BTN(choosedObjective);
        //choosedObjective = choosedObjective.ToUpperInvariant().ToString();
        charArr = choosedObjective.ToCharArray();
        for (int j = 0; j < charArr.Length; j++)
        {
            for (int i = 0; i < englishChars.Length; i++)
            {
                if (englishChars[i] == charArr[j])
                {
                    keyboardWords[Mathf.FloorToInt(Random.Range(0f, keyboardWords.Length))].text = charArr[j].ToString();
                }
            }
        }

        for (int i = 0; i < keyboardWords.Length; i++)
        {
            if (keyboardWords[i].text == "")
            {
                for (int j = 0; j < charArr.Length; j++)
                {
                    do
                    {
                        remainChars = englishChars[Mathf.FloorToInt(Random.Range(0f, 26f))];
                    } while (remainChars == charArr[j]);
                    
                }
                keyboardWords[i].text = remainChars.ToString();
            }
        }
    }
    public void ChooseButton3()
    {
        Destroy(GameObject.Find("Brushes"));
        Row1 = new GameObject();
        GameObject Row2 = Instantiate(Row1, Vector3.zero, Quaternion.identity) as GameObject;
        Row2.transform.name = "Brushes";
        isAllowed = true;
        choosePanel.SetActive(false);
        choosedObjective = button3.text;
        stateManager.Word_Chooser_BTN(choosedObjective);
        //choosedObjective = choosedObjective.ToUpperInvariant().ToString();
        charArr = choosedObjective.ToCharArray();
        for (int j = 0; j < charArr.Length; j++)
        {
            for (int i = 0; i < englishChars.Length; i++)
            {
                if (englishChars[i] == charArr[j])
                {
                    keyboardWords[Mathf.FloorToInt(Random.Range(0f, keyboardWords.Length))].text = charArr[j].ToString();
                }
            }
        }

        for (int i = 0; i < keyboardWords.Length; i++)
        {
            if (keyboardWords[i].text == "")
            {
                for (int j = 0; j < charArr.Length; j++)
                {
                    do
                    {
                        remainChars = englishChars[Mathf.FloorToInt(Random.Range(0f, 26f))];
                    } while (remainChars == charArr[j]);
                    
                }
                keyboardWords[i].text = remainChars.ToString();
            }
        }
    }

    public void KeyBoardGenerate(string choosedObjective)
    {
        charArr = choosedObjective.ToCharArray();
        for (int j = 0; j < charArr.Length; j++)
        {
            for (int i = 0; i < englishChars.Length; i++)
            {
                if (englishChars[i] == charArr[j])
                {
                    keyboardWords[Mathf.FloorToInt(Random.Range(0f, keyboardWords.Length))].text = charArr[j].ToString();
                }
            }
        }

        for (int i = 0; i < keyboardWords.Length; i++)
        {
            if (keyboardWords[i].text == "")
            {
                for (int j = 0; j < charArr.Length; j++)
                {
                    do
                    {
                        remainChars = englishChars[Mathf.FloorToInt(Random.Range(0f, 26f))];
                    } while (remainChars == charArr[j]);

                }
                keyboardWords[i].text = remainChars.ToString();
            }
        }
    }
}
