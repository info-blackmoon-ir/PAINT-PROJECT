using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualKeyboard : MonoBehaviour
{
     public string word = null;
     public Text textHolder = null;

     int wordIndex = 0;
     string alpha;
     string alphabet;

    

    public void AlphabetFunction()
    {
        alphabet = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        wordIndex++;
        word = word + alphabet;
        textHolder.text = word;
    }

    public void BackSpace()
    {
        word = word.Remove(word.Length - 1);
        textHolder.text = word;
    }

    
}
