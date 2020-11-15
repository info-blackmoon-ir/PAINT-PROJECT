using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    AudioSource success_SFX,click_SFX;
    public int maxMessages = 25;
    private StateManager stateManager;
    public GameObject chatPanel, textObject,Keyboard_Go;
    public Text chatBox;
    public Color playerMessage, info, correctWord;
    public string username;
    public PaintChoose pc;
    
    public VirtualKeyboard vk;

    private string choosedObjective;

    [SerializeField]
    List<Message> messageList = new List<Message>();
    void Start()
    {
        stateManager = GameObject.FindObjectOfType<StateManager>();
    }
    

    public void AddText()
    {
        if (chatBox.text != "")
        {
            photonView.RPC("RPCFORTEXT", RpcTarget.AllBuffered, PhotonNetwork.NickName, chatBox.text);
            click_SFX.Play();
        }
    }
    [PunRPC]
    void RPCFORTEXT(string un,string text)
    {
        choosedObjective = stateManager.The_Word;

        
            
        if (choosedObjective == text)
        {
            SendMessageToChat(un +" Entered Correct Word!", Message.MessageType.correctWord);
            stateManager.Win(un,10);
            success_SFX.Play();
            

        }
        else
        {
            SendMessageToChat(un + ": " + text, Message.MessageType.playerMessage);
        }
        if (un == stateManager.My_Profile.UserName)
        {
            vk.word = "";
            chatBox.text = "";
        }
    }
   
    public void SendMessageToChat(string text, Message.MessageType messageType)
    {
       
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        Message newMessage = new Message();

        newMessage.text = text;

        GameObject newText = Instantiate(textObject, chatPanel.transform);

        newMessage.textObject = newText.GetComponent<Text>();

        newMessage.textObject.text = newMessage.text;
        newMessage.textObject.color = MessageTypeColor(messageType);

        messageList.Add(newMessage);
    }

    Color MessageTypeColor(Message.MessageType messageType)
    {
        Color color = info;

        switch (messageType)
        {
            case Message.MessageType.playerMessage:
                color = playerMessage;
                break;
            case Message.MessageType.correctWord:
                color = correctWord;
                break;
        }

        return color;
    }
}

[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
    public MessageType messageType;

    public enum MessageType
    {
        playerMessage,
        info,
        correctWord
    }
}