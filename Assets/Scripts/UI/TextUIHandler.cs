using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using FoxSDK;
using System;

public class TextUIHandler : SingletonMono< TextUIHandler >
{
	private Text text;
	private Scrollbar scrollbar;
	private InputField inputFieldName;
	private InputField inputFieldPort;
	private InputField inputFieldText;

	private string testName;

	// Use this for initialization
	void Start()
	{
		text = transform.Find( "Scroll View" ).Find( "Viewport" ).Find( "Text" ).GetComponent< Text >();
		scrollbar = transform.Find( "Scroll View" ).Find( "Scrollbar Vertical" ).GetComponent< Scrollbar >();
		inputFieldName = transform.Find( "InputFieldName" ).GetComponent< InputField >();
		inputFieldPort = transform.Find( "InputFieldPort" ).GetComponent< InputField >();
		inputFieldText = transform.Find( "InputFieldText" ).GetComponent< InputField >();
		
		scrollbar.value = 0.0f;

		string sfsfsf = GameSocketManager.instance.HostName;
		inputFieldName.text = GameSocketManager.instance.HostName;
		inputFieldPort.text = GameSocketManager.instance.HostPort.ToString();

		testName = "test" + UnityEngine.Random.Range( 0 , 10000 );
	}
	
	public void onConnectClick()
	{
		if ( GameSocketManager.instance.isConnected() )
		{
			addText( "<color=#00ff00>Net already connected...</color>" );
			return;
		}
		
		GameSocketManager.instance.HostName = inputFieldName.text;
		GameSocketManager.instance.HostPort = Convert.ToInt32( inputFieldPort.text );
		GameSocketManager.instance.connect();
	}
	
	public void onSendClick()
	{
		if ( !GameSocketManager.instance.isConnected() )
		{
			addText( "<color=#ff0000>Please connect firest...</color>" );
			return;
		}

		if ( inputFieldText.text.Length == 0 )
		{
			addText( "<color=#ffff00>Please insert some string...</color>" );
			return;
		}
		
		//ChatNetHandler.instance.sendChatMsg( testName , inputFieldText.text ); 
		inputFieldText.text = "";
	}
	
	public void addText( string str )
	{
		text.text += str + "\n";
		scrollbar.value = 0.0f;
	}
	
}
