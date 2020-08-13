using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PhotoChatManager : MonoBehaviour, IChatClientListener
{
    #region Setup
    [SerializeField] GameObject joinChatButton;
    ChatClient chatClient;
    bool isConnected;
    [SerializeField] string username;
    public void UsernameOnValueChange(string valueIn)
    {
        username = valueIn;
    }
    public void ChatConnectOnClick()
    {
        isConnected = true;
        chatClient = new ChatClient(this);
        //chatClient.ChatRegion = "US";
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(username));
        Debug.Log("Connenting");
    }
    #endregion Setup
    #region General
    [SerializeField] GameObject chatPanel;
    string privateReceiver = "";
    string currentChat;
    [SerializeField] InputField chatField;
    [SerializeField] Text chatDisplay;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (isConnected)
        {
            chatClient.Service();
        }
    }
    #endregion General
    #region Callbacks
    public void DebugReturn(DebugLevel level, string message)
    {
        //throw new System.NotImplementedException();
    }
    public void OnChatStateChange(ChatState state)
    {
        //throw new System.NotImplementedException();
        //Debug.Log("Connected");
        //isConnected = true;
        //joinChatButton.SetActive(false);
    }
    public void OnConnected()
    {
        Debug.Log("Connected");
        joinChatButton.SetActive(false);
        chatClient.Subscribe(new string[] { "RegionChannel" });
    }
    public void OnDisconnected()
    {
        throw new System.NotImplementedException();
    }
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        throw new System.NotImplementedException();
    }
    public void OnPrivateMessage(string sender, object message, string channelName)
    {
       throw new System.NotImplementedException();
        
    }
    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }
    public void OnSubscribed(string[] channels, bool[] results)
    {
        chatPanel.SetActive(true);
    }
    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }
    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }
    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }
    #endregion Callbacks
}