using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;


namespace FoxSDK
{
#if USE_UNSAFE
	public unsafe class GameSocketManager : SingletonMonoManager< GameSocketManager >
#else
    public class GameSocketManager : SingletonMonoManager<GameSocketManager>
#endif
    {
        // web player
#if UNITY_WEBPLAYER
		[ HideInInspector ]
		public string HostName = "";
#else
        [HideInInspector]
        public string HostName = "";
#endif
        [HideInInspector]
        public int HostPort = 5666;

        public delegate void msgHandler(GameNetMessage.NetMsgHeadInterface head);

        public struct MsgHandler
        {
            public Type type;
            public msgHandler handler;

#if USE_UNSAFE
#else
            public GameNetMessage.NetMsgHeadInterface msg;
#endif
        }

        private GameClientSocket socket = new GameClientSocket();
        private Dictionary<int, MsgHandler> handlerDic = new Dictionary<int, MsgHandler>();
        private byte[] buffer = new byte[50480];
        private float timeDelayHeart;
        private float timeDelayHeartRecv;
        private bool connected = false;


        private GameClientSocket.onConnectHandler connectHandler;
        private GameClientSocket.onConnectHandler reconnectHandler;

        public bool isConnected()
        {
            return connected;
        }

        public void init(GameClientSocket.onConnectHandler c, GameClientSocket.onConnectHandler re)
        {
            connectHandler = c;
            reconnectHandler = re;
        }


        public void regeditMsg(int t, msgHandler handler, Type type, GameNetMessage.NetMsgHeadInterface msg)
        {
            MsgHandler h = new MsgHandler();
            h.type = type;
            h.handler = handler;

#if USE_UNSAFE
#else
            h.msg = msg;
#endif

            handlerDic[t] = h;
        }

        void onConnected(bool b)
        {
            connected = b;

            if (connectHandler != null)
            {
                connectHandler(b);
            }
        }

        public void connect()
        {
            if (HostName.Length == 0 || HostPort == 0)
            {
                onConnected(false);
                return;
            }

            if (socket.isConnected())
            {
                onConnected(true);
                return;
            }

            socket.onConnect = onConnected;

            StartCoroutine(socket.connect(HostName, HostPort));
        }

        public void sendMsg(GameNetMessage.NetMsgHeadInterface msg)
        {
            socket.sendMsg(msg);

#if UNITY_EDITOR
#endif
        }

        public void close()
        {
            socket.close();
        }


        public byte[] getIbuffer(out int index)
        {
            index = socket.iBuffer.getOffset();

            return socket.iBuffer.getBuffer();
        }


        public void update()
        {
            socket.update();

            if (connected && !socket.isConnected())
            {
                // reconnect,,

                connected = false;

                if (reconnectHandler != null)
                {
                    reconnectHandler(connected);
                }

                return;
            }

            timeDelayHeart += Time.deltaTime;
            if (connected && timeDelayHeart > 4.0f)
            {
                timeDelayHeart = 0.0f;
                GameNetMessage.SEND_RECV_MSG_HEART heart = new GameNetMessage.SEND_RECV_MSG_HEART();
                heart.initNetHead();
                sendMsg(heart);

            }

            if (socket.isConnected())
            {
                timeDelayHeartRecv += Time.deltaTime;

                if (timeDelayHeartRecv > 10.0f)
                {
                    timeDelayHeartRecv = 0.0f;
                    //socket.close();
                    return;
                }
            }

            for (int i = 0; i < 1; i++)
            {
                if (socket.iBuffer.getLen() >= 4)
                {
                    byte[] ibuffer = socket.iBuffer.getBuffer();
                    int offset = socket.iBuffer.getOffset();
                    int len = socket.iBuffer.getLen();
#if USE_UNSAFE
					GameNetMessage.NetMsgHead head = ( GameNetMessage.NetMsgHead )GameDefine.bytesToStruct( ibuffer , offset , typeof( GameNetMessage.NetMsgHead ) ); 

					ushort size = head.size;
					ushort type = head.type;
#else
                    ushort size = BitConverter.ToUInt16(ibuffer, offset);
                    ushort type = BitConverter.ToUInt16(ibuffer, offset + 2);
#endif

                    timeDelayHeartRecv = 0.0f;

                    bool b = handlerDic.ContainsKey(type);

                    if (b)
                    {
                        if (len < size)
                        {
                            return;
                        }

                        MsgHandler handler = handlerDic[type];

#if UNITY_EDITOR
                        //Debug.Log( "recv net msg " + type + " class " + handler.type );
                        Debug.Log("(R <--)" + handler.type);
#endif

#if UNITY_WEBGL && !UNITY_EDITOR
						if ( false ) 
#else
                        if (GameDefine.USE_ZIP && size > GameDefine.HEAD_SIZE)
#endif
                        {
                            byte[] bytesToDeCompress = new byte[size - GameDefine.HEAD_SIZE];
                            for (int i1 = 0; i1 < size - GameDefine.HEAD_SIZE; i1++)
                            {
                                bytesToDeCompress[i1] = ibuffer[offset + i1 + GameDefine.HEAD_SIZE];
                            }

                            byte[] d = GameDefine.DeCompress(bytesToDeCompress);

                            d.CopyTo(buffer, GameDefine.HEAD_SIZE);
                            BitConverter.GetBytes(d.Length + GameDefine.HEAD_SIZE).CopyTo(buffer, 0);
                            BitConverter.GetBytes(type).CopyTo(buffer, 2);

#if USE_UNSAFE
							// web player compatibled,, not use pointer.
							//msg = ( GameNetMessage.NetMsgHead* )GameDefine.fastBytesToStruct( ibuffer , offset , size );

							GameNetMessage.NetMsgHeadInterface msg = ( GameNetMessage.NetMsgHeadInterface )GameDefine.bytesToStruct( buffer , 0 , handler.type );
							handler.handler( msg );
#else
                            handler.msg.ReadRecvBuffer(buffer);
                            handler.handler(handler.msg);
#endif
                        }
                        else
                        {
#if USE_UNSAFE
							// web player compatibled,, not use pointer.
							//msg = ( GameNetMessage.NetMsgHead* )GameDefine.fastBytesToStruct( ibuffer , offset , size );

							GameNetMessage.NetMsgHeadInterface msg = ( GameNetMessage.NetMsgHeadInterface )GameDefine.bytesToStruct( ibuffer , offset , handler.type );
							handler.handler( msg );
#else
                            for (int j = 0; j < size; j++)
                            {
                                buffer[j] = ibuffer[j + offset];
                            }
                            handler.msg.ReadRecvBuffer(buffer);
                            handler.handler(handler.msg);
#endif
                        }

                    }
                    else
                    {
#if UNITY_EDITOR
                        Debug.LogWarning("msg not regedit " + type);
#endif
                    }

                    socket.iBuffer.removeBuffer(size);
                }
                else
                {
                    return;
                }
            }


        }


    }

}


