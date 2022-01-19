using UnityEngine;
using System.Collections;


namespace NetworkDefine
{
	// 패킷 헤더 OFFSET
	enum HEDER_OFFSET
	{
		SIZE = 0,	// 헤더를 포함한 패킷의 크기
		MSG_CODE = 2,	// 해당 패킷의 Message Code
		FUNC_CODE = 4,	// 해당 패킷의 Function Code
		RESULT = 6,	// 패킷의 결과(응답시) =>0: FAIL, 1: SUCCESS
		ERROR_CODE = 8,	// 에러 코드(에러 응답시)
		UID = 12,	// 유저 UID (요청시Java NIO)
	}

	enum NET_DEFINE
	{
		MAX_HEADER_LENGTH = 4,
		REFRESH_TICK = 2000,
		MAX_PACKET_LENGTH = 10240, //10K
		START_TICK = 5000,
	}
//	public const int MAX_HEADER_LENGTH = 16;
//	public const int MAX_PACKET_LENGTH = 2048;
	//-------------------------------------------------------------------------
	//패킷 funciton code
	//-------------------------------------------------------------------------
	//-------------------------------------------------------------------------
	// @brief		Packet Parsing에 사용되어지는 Fucntion code.
	// 서버에서 Packet Parse시에 Function Code를 사용하여 메시지를 Parse 한다.\n
	// 이에 각 패킷들은 자신들의 구현에 맞는 Fucntion Code를 사용하여야 한다. \n
	// FuncCode의 이름은 "FC_" Prefix 이후에 메시지 이름의 대소문자 혼용으로 되어있다.
	//-------------------------------------------------------------------------
	enum FC
	{
		//common
		Ping = 1,
		Login,
		Logout,
		Notice,

		//matching server
		MatchRequest = 100,
		MatchResponse,
		JoinMatch,
		MatchingReady,

		//game server
		RoomUsers = 200,
		GameStart,
		GameHit,
		GameEnd,
		GameReQuery,
		DisconnectedPlayer,

		//server packet 1000번부터.
		ServerMessageStart = 1000,
		SHostConfig,
		SHostRegister,
		SHostUpdate,
		SRegCreateRoom,
		SUpdateRoomUser,
		SCtrlCommand,
		SPlayResult,
		SNextGameSeq,
		STransferPacket,
	}

	enum MSG
	{
		//-------------------------------------------------------------------------
		// 공통 message
		//-------------------------------------------------------------------------
		HDRQ_LOGIN = 1001,		/* 최초 인증 */
		HDRE_LOGIN = 1002,
		HDRQ_PING = 1003,		/* PING		 */
		HDRE_PING = 1004,
		HDRQ_LOGOUT = 1005,
		HDRE_LOGOUT = 1006,
		HDRQ_NOTICE = 1007,
		HDRE_NOTICE = 1008,

		//-------------------------------------------------------------------------
		// 매칭 서버.
		//-------------------------------------------------------------------------
		HDRQ_MATCH_REQUEST = 2001,
		HDRE_MATCH_REQUEST = 2002,
		HDRQ_MATCH_RESPONSE = 2003,
		HDRE_MATCH_RESPONSE = 2004,
		HDRQ_JOINMATCH = 2005,
		HDRE_JOINMATCH = 2006,
		HDRQ_MATCH_READY = 2007,
		HDRE_MATCH_READY = 2008,

		//-------------------------------------------------------------------------
		// 게임 서버.
		//-------------------------------------------------------------------------
		HDRQ_ROOM_USERS = 3001,
		HDRE_ROOM_USERS = 3002,
		HDRQ_GAME_START = 3003,
		HDRE_GAME_START = 3004,
		HDRQ_GAME_HIT = 3005,
		HDRE_GAME_HIT = 3006,
		HDRQ_GAME_END = 3007,
		HDRE_GAME_END = 3008,
		HDRQ_GAME_REQUERY = 3009,
		HDRE_GAME_REQUERY = 3010,
	}
}
