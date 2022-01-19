using UnityEngine;
using System.Collections;

using FoxSDK;
using System;

public class GameManager : SingletonMonoManager< GameManager >
{
	static public bool GameConnect = false;
	static public bool BattleEndGameReturn = false;
	static public bool CreateUserId = false;

	

	public string AuthGoogleKey;

	public bool IsServerDisConnecting = false;

	public bool BattleStartFlag;
	public bool MissionFlag;

	public override void initSingletonMono()
	{
		GameSocketManager.instance.init( onConnect , onDisconnect );
	}

	public void AuthConnect()
    {
		GameSocketManager.instance.HostName = ipconfigmanager.instance.loginIp;
		GameSocketManager.instance.HostPort = ipconfigmanager.instance.auth_port;
		GameSocketManager.instance.connect();
	}
	
	/// <summary>
	/// On connect.
	/// </summary>
	/// <param name="b"> true Net is connected.</param>
	void onConnect( bool b )
	{
		if ( b )
		{
			int a = 0;
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

	

	public void MainServerConnect()
	{
		//GameSocketManager.instance.close();
		//GameSocketManager.instance.HostName = ip;
		//GameSocketManager.instance.HostPort = game_port;
		//GameSocketManager.instance.connect();
		//GameConnect = false;
		//GameManager.BattleEndGameReturn = true;

		GameConnect = true;
		BattleEndGameReturn = true;
		GameSocketManager.instance.close();

	}

	/// <summary>
	/// On disconnect.
	/// </summary>
	/// <param name="b"></param>
	void onDisconnect( bool b )
	{
		IsServerDisConnecting = true;

		// here returen login scene or do someting...

		//TextUIHandler.instance.addText( "<color=#cccccc>Net disonnected...</color>" );
		Debug.Log("<color=#00ff00>Net disonnected...</color>");

		if (GameConnect == false)
		{
			//if(NetworkNoticePopup.instance!=null)
			{
				//NetworkNoticePopup.instance.SetNoticeMessage("t50072");
				//NetworkNoticePopup.instance.OpenPopup();
			}
		}

		if(GameConnect == true)
        {
			GameSocketManager.instance.HostName = ipconfigmanager.instance.ip;
			GameSocketManager.instance.HostPort = ipconfigmanager.instance.game_port;
			GameSocketManager.instance.connect();
			GameConnect = false;
        }

		
	}

	void Update()
	{
		// update socket
		
		GameSocketManager.instance.update();
	}

    private void OnDestroy()
    {
		GameSocketManager.instance.close();
    }

}
