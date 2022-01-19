using UnityEngine;
using System.Collections;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

using FoxSDK;

public class ChatNetHandler : SingletonMono< ChatNetHandler >
{	
	public void Start()
	{
		// regedit chat msg.
		
		//GameSocketManager.instance.regeditMsg( (int)GameNetMessage.MsgType._MSG_RECV_CHAT_MSG , 
		//	recvChatMsg , 
		//	typeof( GameNetMessage.SEND_RECV_MSG_CHAT_MSG ) ,
		//	new GameNetMessage.SEND_RECV_MSG_CHAT_MSG() );
	}

	/// <summary>
	/// Sends the chat message.
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="chat">Chat.</param>
	/*public void sendChatMsg( string name , string chat )
	{
		GameNetMessage.SEND_RECV_MSG_CHAT_MSG msg = new GameNetMessage.SEND_RECV_MSG_CHAT_MSG();
		msg.initNetHead();

		byte[] b = Encoding.UTF8.GetBytes( name );
		if ( b.Length >= GameNetMessage.MAX_CHAT_MSG )
		{
			b[ GameNetMessage.MAX_NAME - 1 ] = 0;
		}

		for ( int i = 0 ; i < b.Length ; i++ ) 
		{
			msg.Name[ i ] = b[ i ];
		}

		b = Encoding.UTF8.GetBytes( chat );
		if ( b.Length >= GameNetMessage.MAX_CHAT_MSG )
		{
			b[ GameNetMessage.MAX_CHAT_MSG - 1 ] = 0;
		}

		for ( int i = 0 ; i < b.Length ; i++ ) 
		{
			msg.Chat[ i ] = b[ i ];
		}

		GameSocketManager.instance.sendMsg( msg );
	}*/

	/// <summary>
	/// Recvs the chat message.
	/// </summary>
	/// <param name="head">Head.</param>
	/*public void recvChatMsg( GameNetMessage.NetMsgHeadInterface head )
	{
		GameNetMessage.SEND_RECV_MSG_CHAT_MSG msg = ( GameNetMessage.SEND_RECV_MSG_CHAT_MSG )head;

		byte[] name = new byte[ GameNetMessage.MAX_NAME ];
		byte[] chat = new byte[ GameNetMessage.MAX_CHAT_MSG ];

		for ( int i = 0 ; i < GameNetMessage.MAX_NAME ; i++ ) 
		{
			name[ i ] = msg.Name[ i ];
		}

		for ( int i = 0 ; i < GameNetMessage.MAX_CHAT_MSG ; i++ ) 
		{
			chat[ i ] = msg.Chat[ i ];
		}

		string name1 = Encoding.UTF8.GetString( name ).TrimEnd( '\0' );
		string chat1 = Encoding.UTF8.GetString( chat ).TrimEnd( '\0' );

		TextUIHandler.instance.addText( name1 + " : " + chat1 );
	}*/


}


