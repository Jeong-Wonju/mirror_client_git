using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.GZip; 
using ICSharpCode.SharpZipLib.Zip; 
using System.IO;
using System;


namespace FoxSDK
{
	public class GameDefine 
	{
		public const int	HEAD_SIZE = 4;
		
		// use zip compression net msg data. 
		public const bool	USE_ZIP = true;
		
		public const int	BUFFER_SIZE = 102400;
		public const int	TIME_OUT = 5000;

		public static byte[] ObjectToByteA(object obj)
		{
			MemoryStream fs = new MemoryStream();
			byte[] tmp = null;
			try 
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize( fs , obj );
				tmp = fs.ToArray();
			}
			catch(Exception e)
			{
				Debug.LogError( e.Message );
			}
			finally 
			{
				fs.Close();
			}
			return tmp;
		}

		public static object ByteAToObject(byte[] ba)
		{
			MemoryStream fs = new MemoryStream();
			object obj = null;
			try
			{
				fs = new MemoryStream(ba);
				fs.Position = 0;
				BinaryFormatter formatter = new BinaryFormatter();
				obj = formatter.Deserialize(fs);
			}
			catch(Exception e)
			{
			}
			finally 
			{
				fs.Close();
			}
			return obj;
		}

		public static byte[] Compress( byte[] bytesToCompress ) 
		{ 
			byte[] rebyte = null; 
			MemoryStream ms = new MemoryStream(); 

			GZipOutputStream s = new GZipOutputStream( ms ); 

			try 
			{
				s.Write( bytesToCompress , 0 , bytesToCompress.Length ); 
				s.Flush();
				s.Finish();
			}
			catch ( System.Exception ex ) 
			{
				#if UNITY_EDITOR
				Debug.Log( ex );
				#endif
			}

			ms.Seek( 0 , SeekOrigin.Begin );

			rebyte = ms.ToArray(); 

			s.Close(); 
			ms.Close(); 

			return rebyte;
		}

		public static byte[] DeCompress( byte[] bytesToDeCompress ) 
		{ 
			byte[] rebyte = new byte[ bytesToDeCompress.Length * 20 ];

			MemoryStream ms = new MemoryStream( bytesToDeCompress ); 
			MemoryStream outStream = new MemoryStream();

			GZipInputStream s = new GZipInputStream( ms ); 

			int read = s.Read( rebyte , 0 , rebyte.Length ); 
			while ( read > 0 )
			{
				outStream.Write( rebyte, 0 , read );
				read = s.Read( rebyte , 0, rebyte.Length );
			}

			byte[] rebyte1 = outStream.ToArray(); 

			ms.Close();
			s.Close();
			outStream.Close();

			return rebyte1;
		}

		#if USE_UNSAFE

		public unsafe static byte[] fastStructToBytes( byte* buff , int size )
		{
			byte[] bytes = new byte[ size ];
	
			for ( int i = 0; i < size ; i++ ) 
			{
				bytes[ i ] = buff[ i ];
			}

			return bytes;
		}

		private static IntPtr fastBytesToStructBuffer = IntPtr.Zero;
		public unsafe static void* fastBytesToStruct( byte[] bytes , int index , int buffSize , int offset = 0 )
		{
			if ( fastBytesToStructBuffer == IntPtr.Zero ) 
			{
				fastBytesToStructBuffer = Marshal.AllocHGlobal( BUFFER_SIZE );
			}

			Marshal.Copy( bytes , index , (IntPtr)(fastBytesToStructBuffer.ToInt32() + offset ) , buffSize );

			return fastBytesToStructBuffer.ToPointer();
		}

		public static byte[] structToBytes( object structObj )
		{
			int size = Marshal.SizeOf( structObj );
			IntPtr buffer = Marshal.AllocHGlobal( size );

			try
			{
				Marshal.StructureToPtr( structObj , buffer , false );
				byte[] bytes = new byte[ size ];
				Marshal.Copy( buffer , bytes , 0 , size );
				return bytes;
			}
			finally
			{
				Marshal.FreeHGlobal( buffer );
			}
		}

		public static object bytesToStruct( byte[] bytes , int index , Type strcutType )
		{
			int size = Marshal.SizeOf( strcutType );
			IntPtr buffer = Marshal.AllocHGlobal( size );
	
			try
			{
				Marshal.Copy( bytes , index , buffer , size );
				object obj = Marshal.PtrToStructure( buffer , strcutType );
				return obj;
			}
			finally
			{
				Marshal.FreeHGlobal( buffer );
			}
		}

		public static T[] bytesToStructArray< T >( byte[] bytes , int index , Type strcutType , int sizeT , int sizeOf )
		{
			T[] obj = new T[ sizeT ];
	
			for ( int i = 0; i < sizeT ; i++ ) 
			{
				obj[ i ] = (T)bytesToStruct( bytes , index + i * sizeOf , strcutType );
			}
	
			return obj;
		}

		#endif


	}


	
}

