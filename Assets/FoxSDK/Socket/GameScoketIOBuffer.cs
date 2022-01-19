using System.Runtime;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;



[ StructLayout( LayoutKind.Sequential , Pack = 1 ) ]
public struct GameScoketIOBuffer
{
	int begin;
	int len;

	byte[] buffer;
	int maxLen;


	public void	clearBuffer()
	{
		begin = 0;
		len = 0;
	}


	public void	initBuffer( int m )
	{
		maxLen = m;

		if ( buffer == null )
		{
			buffer = new byte[ m ];
		}

		begin = 0;
		len = 0;
	}


	public void	releaseBuffer()
	{
		begin = 0;
		len = 0;
		maxLen = 0;
		buffer = null;
	}

	public void	write( int l )
	{
		len += l;
	}

	public void	removeBuffer( int l )
	{
		if ( len < l ) 
		{
			return;
		}

		begin += l;
		len -= l;

		if ( len == 0 ) 
		{
			begin = 0;
		}
	}

	public bool write( byte[] b , int l )
	{
		if ( getSpace() < l )
		{
			return false;
		}

		for ( int i = 0 ; i < l ; i++ )
		{
			buffer[ i + begin + len ] = b[ i ];
		}

		len += l;

		return true;
	}

	public int getLen()
	{
		return len;
	}

	public int getOffset()
	{
		return begin;
	}

	public int getSpace()
	{
		return maxLen - begin - len;
	}

	public byte[] getBuffer()
	{
		return buffer;
	}



	#if SA_UNSAFE
	public unsafe bool fastWrite( byte* b , int l )
	{
	if ( getSpace() < l )
	{
	return false;
	}

	for ( int i = 0 ; i < l ; i++ )
	{
	fixed( byte* pb = &buffer[ i + begin + len ] )
	{
	*pb = b[ i ];
	}
	}

	len += l;

	return true;
	}

	#endif



}

