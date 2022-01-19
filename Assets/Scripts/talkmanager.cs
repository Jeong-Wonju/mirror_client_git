using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoxSDK;

public class talkmanager : SingletonMonoManager<talkmanager>
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("talkmanager:Start");

        GameSocketManager.instance.HostName = ipconfigmanager.instance.loginIp;
        GameSocketManager.instance.HostPort = ipconfigmanager.instance.auth_port;
        GameSocketManager.instance.connect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void initSingletonMono()
    {
        GameSocketManager.instance.init(onConnect, onDisconnect);
        
        Debug.Log("talkmanager:initSingletonMono");
    }

    /// <summary>
	/// On connect.
	/// </summary>
	/// <param name="b"> true Net is connected.</param>
	void onConnect(bool b)
    {
        if (b)
        {
            //TextUIHandler.instance.addText( "<color=#00ff00>Net connected! You can send msg Now!</color>" );
            Debug.Log("<color=#00ff00>Net connected! You can send msg Now!</color>");

            //Localized_Text.instance.Init();

            //MyCard.instance.Init();
            //GuildManager.Instance.Init();
            //MyItem.instance.Init();

            //GetComponent<PacketReceiver>().PacketParse(_PTCODE.ACCEPT_AUTH_ANS, null);
        }
        else
        {
            //TextUIHandler.instance.addText( "<color=#ff0000>Net can't connect to host, Please check it.</color>" );
            Debug.Log("<color=#ff0000>Net can't connect to host, Please check it.</color>");
            //LoginErrorPopup.Instance.OpenErrorPopup("t50072");
            //NoticePopup.instance.OpenPopup();
        }
    }

    /// <summary>
	/// On disconnect.
	/// </summary>
	/// <param name="b"></param>
	void onDisconnect(bool b)
    {
        //IsServerDisConnecting = true;

        // here returen login scene or do someting...

        //TextUIHandler.instance.addText( "<color=#cccccc>Net disonnected...</color>" );
        Debug.Log("<color=#00ff00>Net disonnected...</color>");

        


    }
}
