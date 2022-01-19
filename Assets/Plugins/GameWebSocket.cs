using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using UnityEngine;
using System.Runtime.InteropServices;

namespace FoxSDK
{
	
#if UNITY_WEBGL && !UNITY_EDITOR

	public class GameWebSocket
	{
		[DllImport("__Internal")]
		private static extern int SocketCreate ();
	
		[DllImport("__Internal")]
		private static extern bool SocketConnect( int i , string url );
	
		[DllImport("__Internal")]
		private static extern bool SocketIsOpen( int i );
	
		[DllImport("__Internal")]
		private static extern int SocketState( int i );
	
		[DllImport("__Internal")]
		private static extern void SocketSend( int i , byte[] ptr , int length );
	
		[DllImport("__Internal")]
		private static extern void SocketRecv( int i , byte[] ptr , int length );
	
		[DllImport("__Internal")]
		private static extern int SocketRecvLength( int i );
	
		[DllImport("__Internal")]
		private static extern void SocketClose( int i );
	
		[DllImport("__Internal")]
		private static extern int SocketError( int i , byte[] ptr , int length );
	
		int instance = 0;
		
		public GameWebSocket()
		{
			instance = SocketCreate();
		}
	
		public void SendString( string str )
		{
			Send( Encoding.UTF8.GetBytes ( str ) );
		}
	
		public string RecvString()
		{
			byte[] r = Recv();
	
			if (r == null)
				return null;
	
			return Encoding.UTF8.GetString( r );
		}
	
		public void Send( byte[] buffer )
		{
			SocketSend( instance , buffer , buffer.Length );
		}
	
		public byte[] Recv()
		{
			int length = SocketRecvLength( instance );
	
			if (length == 0)
				return null;
	
			byte[] buffer = new byte[ length ];
	
			SocketRecv( instance , buffer , length );
			return buffer;
		}
	
		public void Connect( Uri url )
		{	
			string protocol = url.Scheme;
	
			if ( !protocol.Equals( "ws" ) && !protocol.Equals( "wss" ) )
				throw new ArgumentException( "Unsupported protocol: " + protocol );
	
			SocketConnect( instance , url.ToString());
		}
	
		public void Close()
		{
			SocketClose( instance );
		}
	
		public bool IsOpen()
		{
			return SocketIsOpen( instance );
		}

		public string error
		{
			get
			{
				const int bufsize = 1024;
				byte[] buffer = new byte[ bufsize ];
				int result = SocketError( instance , buffer , bufsize );
			
				if ( result == 0 )
				return null;
			
				return Encoding.UTF8.GetString( buffer );				
			}
		}

	}
	

#endif 

}
