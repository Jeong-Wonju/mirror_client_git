using UnityEngine;
using System.Collections;
using System.Net.Sockets; 
using System.Net; 
using System.Threading;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System;
using FoxSDK;


public class GameClientSocket
{
	#if UNITY_WEBGL && !UNITY_EDITOR
	private WebSocket webSocket;
	#else
	private Socket socket;
	#endif
	private bool connected = false;

	public string serverIP;
	public int serverPort;
	public int bufferSize = 1024000;
	public int timeOut = 5000;

	public GameScoketIOBuffer iBuffer;
	public GameScoketIOBuffer oBuffer;

	public byte msgCount;

	public delegate void onConnectHandler( bool b );
	public onConnectHandler onConnect = null;

	public GameClientSocket ()
	{
		iBuffer = new GameScoketIOBuffer();
		iBuffer.initBuffer( bufferSize );
		oBuffer = new GameScoketIOBuffer();
		oBuffer.initBuffer( bufferSize );
	}

	public bool isConnected()
	{
		return connected;
	}

	public IEnumerator connect( string ip , int port )
	{
		#if UNITY_EDITOR
		Debug.Log( ip.ToString() + " port=" + port );
		#endif

		serverIP = ip;
		serverPort = port;

		if ( serverIP.Length == 0 ) 
		{
			Debug.LogError( "IP is invalid." );

			if ( onConnect != null ) 
			{
				onConnect( false );
			}

			yield break;
		}

		#if UNITY_WEBGL && !UNITY_EDITOR

		if ( webSocket == null ) 
		{
		webSocket = new WebSocket();
		}

		//		Debug.Log( "ws://" + ip + ":" + ( port + 1 ) );

		webSocket.Connect( new Uri( "ws://" + ip + ":" + ( port + 1 ) ) );

		int n = 0;
		while ( n < 10 ) 
		{
		n++;

		if ( webSocket.IsOpen() ) 
		{
		connected = true;

		if ( onConnect != null ) 
		{
		onConnect( connected );
		}
		break;
		}
		else
		{
		yield return new WaitForSeconds( 0.5f );
		}

		if ( webSocket.error != null ) 
		{
		break;
		}
		}

		if ( !connected ) 
		{
		if ( onConnect != null ) 
		{
		onConnect( connected );
		}

		webSocket.Close();
		}
		#else

		bool ipV6 = false;

		IPAddress ipAddress = null;
		try
		{
			IPHostEntry dnstoip = new IPHostEntry();           

			IPAddress[] ipHostInfo = Dns.GetHostAddresses( ip ); 
			ipAddress = ipHostInfo[ 0 ]; 

			//			for ( int i = 0 ; i < ipHostInfo.AddressList.Length ; i++ )
			//			{
			//				if ( ipHostInfo.AddressList[ i ].ToString().Contains( "." ) ) 
			//				{
			//					ipAddress = ipHostInfo.AddressList[ i ];
			//				}
			//			}
		}
		catch ( Exception ex ) 
		{
			Debug.LogError( ex );
			Debug.LogError( ip + ":" + port );

			socket = null;

			if ( onConnect != null ) 
			{
				onConnect( false );
			}

			yield break;
		}


		string ipv66 = ipAddress.ToString();

		if ( ipv66.Contains( ":" ) ) 
		{
			ipV6 = true;
		}

		Debug.Log( "ipv6 " + ipV6 );

		//		bool error = false;

		if ( socket == null )
		{
			socket = new Socket ( ipV6 ? AddressFamily.InterNetworkV6 : AddressFamily.InterNetwork , SocketType.Stream , ProtocolType.Tcp );
		}

		socket.Blocking = true;

		//		IPAddress ipAddress = IPAddress.Parse( ip );
		IPEndPoint ipEndpoint = new IPEndPoint( ipAddress , port );

		#if UNITY_EDITOR
		Debug.Log( ipAddress.ToString() );
		#endif

		try 
		{
		#if UNITY_WEBPLAYER
			Security.PrefetchSocketPolicy( ip , 843 );
		#endif
			socket.Connect( ipEndpoint );
		}
		catch ( Exception ex ) 
		{
			Debug.LogError( ex );
			Debug.LogError( ip + ":" + port );

			socket = null;

			if ( onConnect != null ) 
			{
				onConnect( false );
			}

			yield break;
		}

		socket.Blocking = false;

		connected = socket.Connected;

		//		IAsyncResult result = socket.BeginConnect( ipEndpoint , new AsyncCallback( connectCallback ) , socket );
		//
		//		bool success = result.AsyncWaitHandle.WaitOne( timeOut , true );
		//
		#if UNITY_EDITOR
		if ( !connected )
		{
			Debug.LogError( "Connect timeout." );
		}
		else
		{
			Debug.Log( "Connect Host " + ip + ":" + port );
		}
		#endif

		if ( onConnect != null ) 
		{
			onConnect( connected );
		}

		#endif
		yield return 0;
	}


	public void	close()
	{
		if ( !connected )
		{
			return;
		}

		msgCount = 0;

		#if UNITY_WEBGL && !UNITY_EDITOR
		webSocket.Close();
		#else
		try 
		{
			socket.Shutdown( SocketShutdown.Both );
		}
		catch ( Exception ex ) 
		{}

		socket.Close();
		socket = null;
		#endif

		connected = false;

		iBuffer.clearBuffer();
		oBuffer.clearBuffer();
	}


	public void	sendData()
	{
		int len = oBuffer.getLen();

		if ( len == 0 )
		{
			return;
		}

		#if UNITY_WEBGL && !UNITY_EDITOR

		byte[] buf = new byte[ len ];

		for ( int i = 0 ; i < len ; i++ ) 
		{
		buf[ i ] = oBuffer.getBuffer()[ i + oBuffer.getOffset() ];
		}

		webSocket.Send( buf );

		if ( webSocket.error != null )
		{
		close();
		return;
		}

		oBuffer.removeBuffer( len );
		#else
		SocketError error;
		int tmp = socket.Send( oBuffer.getBuffer() , oBuffer.getOffset() , len , SocketFlags.None , out error );

		if ( tmp <= 0 )
		{
			if ( error == SocketError.NoBufferSpaceAvailable || 
				error == SocketError.Interrupted || 
				error == SocketError.Success ||
				error == SocketError.WouldBlock  )
			{
				return;
			}
			else
			{
				close();

				return;
			}
		}

		oBuffer.removeBuffer( tmp );
		#endif
	}


	public void recvData()
	{
		if ( iBuffer.getSpace() < 10240 )
		{
			return;
		}

		#if UNITY_WEBGL && !UNITY_EDITOR
		byte[] recv = webSocket.Recv();
		int count = recv.Length;

		if ( webSocket.error != null )
		{
		close();
		return;
		}

		if ( count > 0 )
		{
		iBuffer.write( recv , recv.Length );

		//			Debug.Log( "recvData" + count );
		}

		#else
		SocketError error;
		int count = socket.Receive( iBuffer.getBuffer() , iBuffer.getOffset() + iBuffer.getLen() , iBuffer.getSpace() , SocketFlags.None , out error );

		if ( count > 0 )
		{
			iBuffer.write( count );   
		}
		else if ( count <= 0 )
		{
			if ( error == SocketError.NoBufferSpaceAvailable || 
				error == SocketError.Interrupted || 
				error == SocketError.Success ||
				error == SocketError.WouldBlock )
			{
				// No data available to read (and socket is non-blocking)
				count = 0;
			}
			else
			{
		#if UNITY_EDITOR
				Debug.LogWarning( "Socket error " + error + (int)error );
		#endif
				close();

				return;
			}
		}
		#endif
	}

	public void update()
	{
		if ( !connected )
		{
			return;
		}

		sendData();
		recvData();
	}

	public void sendMsg( GameNetMessage.NetMsgHeadInterface msg )
	{
		#if SA_UNSAFE
		byte[] b = GameDefine.structToBytes( msg );
		#else
		byte[] b = msg.toBytesArray();
		#endif

		ushort size = BitConverter.ToUInt16( b , 0 );
		ushort type = BitConverter.ToUInt16( b , 2 );

		GameNetMessage.NetMsgHead head = new GameNetMessage.NetMsgHead();

		#if UNITY_WEBGL && !UNITY_EDITOR
		if( false )
		#else
		if( GameDefine.USE_ZIP && size > GameDefine.HEAD_SIZE )
		#endif
		{
			msgCount++;

			byte[] buffm = new byte[ size - GameDefine.HEAD_SIZE ];
			for (int i = 0; i < buffm.Length ; i++ ) 
			{
				buffm[ i ] = b[ i + GameDefine.HEAD_SIZE ];
			}

			byte[] buff = GameDefine.Compress( buffm );

			for (int i = 0; i < buff.Length ; i++ ) 
			{
				// decode here
				//buff[ i ] += GameSetting.instance.msgCode[ msgCount ];
			}

			head.size = (ushort)( buff.Length + GameDefine.HEAD_SIZE );
			head.type = type;

			#if SA_UNSAFE
			byte[] bb = GameDefine.structToBytes( head );
			#else
			int pos = 0;
			byte[] bb = new byte[ sizeof( int ) ];
			BitConverter.GetBytes( head.size ).CopyTo( bb , pos ); pos += 2;
			BitConverter.GetBytes( head.type ).CopyTo( bb , pos ); pos += 2;
			#endif

			oBuffer.write( bb , bb.Length );
			oBuffer.write( buff , buff.Length );
		}
		else
		{
			oBuffer.write( b , size );
		}
	}




}
