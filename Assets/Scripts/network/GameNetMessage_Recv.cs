using System;
using System.Runtime;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;


namespace FoxSDK
{
    public partial struct GameNetMessage
    {
        static public ulong ReaduLong(byte[] buffer, ref int offset)
        {
            ulong u = System.BitConverter.ToUInt64(buffer, offset);
            offset += 8;
            return u;
        }

        // 2byte 얻어오기 
        static public ushort ReaduShort(byte[] buffer, ref int offset)
        {
            ushort u = System.BitConverter.ToUInt16(buffer, offset);
            offset += 2;
            return u;
        }

        static public short ReadShort(byte[] buffer, ref int offset)
        {
            short u = System.BitConverter.ToInt16(buffer, offset);
            offset += 2;
            return u;
        }

        // 1byte 얻어오기 
        static public byte ReadByte(byte[] buffer, ref int offset)
        {
            byte u = buffer[offset];
            offset += 1;
            return u;
        }

        // 4byte 얻어오기 
        static public int ReadInt(byte[] buffer, ref int offset)
        {
            int i = System.BitConverter.ToInt32(buffer, offset);
            offset += 4;
            return i;
        }

        static public float ReadFloat(byte[] buffer, ref int offset)
        {
            float f = System.BitConverter.ToSingle(buffer, offset);
            offset += 4;
            return f;
        }

        static public bool Readboolean(byte[] buffer, ref int offset)
        {
            bool i = System.BitConverter.ToBoolean(buffer, offset);
            offset += 1;
            return i;
        }

        // 문자열 얻어오기 
        static public Byte[] ReadStringA(byte[] buffer, ref int offset, int strlen, int maxlen)
        {
            byte[] temp = new byte[strlen];
            temp.Initialize();
            System.Buffer.BlockCopy(buffer, offset, temp, 0, strlen);
            string s = System.Text.Encoding.UTF8.GetString(temp);
            offset += maxlen;
            return temp;
        }

        

        static public Byte[] ReadStringW(byte[] buffer, ref int offset, int strlen, int maxlen)
        {
            byte[] temp = new byte[strlen * 2];
            temp.Initialize();
            System.Buffer.BlockCopy(buffer, offset, temp, 0, strlen * 2);
            string s = System.Text.Encoding.Unicode.GetString(temp);
            offset += (maxlen * 2);
            return temp;
        }

        static public string ReadStringWEx(byte[] buffer, ref int offset, int strlen, int maxlen)
        {
            byte[] temp = new byte[strlen * 2];
            temp.Initialize();
            System.Buffer.BlockCopy(buffer, offset, temp, 0, strlen * 2);
            string s = System.Text.Encoding.Unicode.GetString(temp);
            offset += (maxlen * 2);
            return s;
        }

        /// <summary>
        /// 2020-12-24
        /// </summary>

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _ACCEPT_AUTH_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.ACCEPT_AUTH_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _VESITON_CHECK_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.VESITON_CHECK_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _ACCEPT_BATTLE_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.ACCEPT_BATTLE_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);
            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BATTLE_CHARACTER_DATA : NetMsgHeadInterface
        {
            public struct _STATUS_DATA
			{
                public int Attack;
                public int Stength;
                public int Defence;
                public int Speed;
                public int MaxHealth;
                public int CurrentHealth;
                public int MaxMana;
                public int CurrentMana;
                public int MoveMent;
                public int StackCount;
            };

            public struct _BUFF_DATA
			{
                public int SkillID;
                public int EffectID;
                public int AtkValue;
                public int StrValue;
                public int DefValue;
                public int SpdValue;
                public int HpValue;
                public int DamageValue;
                public int TurnCount;
            };

            public struct _POSITION_DATA
			{
                public int PosX;
                public int PosY;
			}
           

            public void initNetHead()
            {
              
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public int BattleUnitID;
            public int CardID;
            public int Size;
            public _STATUS_DATA Status;
            public int BuffCount;
            public _BUFF_DATA[] BuffList;
            public _POSITION_DATA PositionData;

            public void ReadRecvBuffer(byte[] bytes)
            {
                
            }

            public void ReadRecvBuffer(byte[] bytes, ref int pos)
            {

                BattleUnitID = ReadInt(bytes, ref pos);
                CardID = ReadInt(bytes, ref pos);
                Size = ReadInt(bytes, ref pos);

                Status.Attack = ReadInt(bytes, ref pos);
                Status.Stength = ReadInt(bytes, ref pos);
                Status.Defence = ReadInt(bytes, ref pos);
                Status.Speed = ReadInt(bytes, ref pos);
                Status.MaxHealth = ReadInt(bytes, ref pos);
                Status.CurrentHealth = ReadInt(bytes, ref pos);
                Status.MaxMana = ReadInt(bytes, ref pos);
                Status.CurrentMana = ReadInt(bytes, ref pos);
                Status.MoveMent = ReadInt(bytes, ref pos);
                Status.StackCount = ReadInt(bytes, ref pos);

                BuffCount = ReadInt(bytes, ref pos);

                BuffList = new _BUFF_DATA[Define.BUFF_MAX_COUNT];

                for(int i=0;i< Define.BUFF_MAX_COUNT; i++)
				{
                    BuffList[i].SkillID = ReadInt(bytes, ref pos);
                    BuffList[i].EffectID = ReadInt(bytes, ref pos);
                    BuffList[i].AtkValue = ReadInt(bytes, ref pos);
                    BuffList[i].StrValue = ReadInt(bytes, ref pos);
                    BuffList[i].DefValue = ReadInt(bytes, ref pos);
                    BuffList[i].SpdValue = ReadInt(bytes, ref pos);
                    BuffList[i].HpValue = ReadInt(bytes, ref pos);
                    BuffList[i].DamageValue = ReadInt(bytes, ref pos);
                    BuffList[i].TurnCount = ReadInt(bytes, ref pos);
                }

                PositionData.PosX = ReadInt(bytes, ref pos);
                PositionData.PosY = ReadInt(bytes, ref pos);
            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _BATTLE_CONNECT_ANS : NetMsgHeadInterface
        {

            public struct BattleHeroStatData
			{
               
                public int Attack;
                public int Stength;
                public int Defence;
                public int Speed;
                public int MaxHealth;
                public int CurrentHealth;
                public int MaxMana;
                public int CurrentMana;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.BATTLE_CONNECT_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int BattleType;

            public BATTLE_CHARACTER_DATA[] PlayerCharacterData;
            public BATTLE_CHARACTER_DATA[] EnemyCharacterData;

            public int HeroID;
            public int HeroLevel;
            public BattleHeroStatData StatusData;
            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                BattleType = ReadInt(bytes, ref pos);

                PlayerCharacterData = new BATTLE_CHARACTER_DATA[Define.BATTLE_ALL_MAX];
                for(int i=0;i< PlayerCharacterData.Length; i++)
				{
                    PlayerCharacterData[i].ReadRecvBuffer(bytes, ref pos);
                }

                EnemyCharacterData = new BATTLE_CHARACTER_DATA[Define.BATTLE_ALL_MAX];
                for (int i = 0; i < EnemyCharacterData.Length; i++)
                {
                    EnemyCharacterData[i].ReadRecvBuffer(bytes, ref pos);
                }

                HeroID = ReadInt(bytes, ref pos);
                HeroLevel = ReadInt(bytes, ref pos);

                StatusData.Attack = ReadInt(bytes, ref pos);
                StatusData.Stength = ReadInt(bytes, ref pos);
                StatusData.Defence = ReadInt(bytes, ref pos);
                StatusData.Speed = ReadInt(bytes, ref pos);
                StatusData.MaxHealth = ReadInt(bytes, ref pos);
                StatusData.CurrentHealth = ReadInt(bytes, ref pos);
                StatusData.MaxMana = ReadInt(bytes, ref pos);
                StatusData.CurrentMana = ReadInt(bytes, ref pos);

            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _SKIP_BATTLE_CONNECT_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.SKIP_BATTLE_CONNECT_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int BattleType;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                BattleType = ReadInt(bytes, ref pos);
            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _MEMBERJOIN_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.MEMBERJOIN_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int Answer;
            public ushort szNickLen;
            public byte[] szNick;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);
                Answer = ReadInt(bytes, ref pos);
                szNickLen = ReaduShort(bytes, ref pos);
                szNick = ReadStringW(bytes, ref pos, szNickLen, Define.MAX_NICKLEN);

            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _CREATE_GAME_ID_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.CREATE_GAME_ID_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public ushort UserIdLen;
            public byte[] NewUserId;

            public ushort NickLen;
            public byte[] NewUserNick;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                UserIdLen = ReaduShort(bytes, ref pos);
                NewUserId = ReadStringW(bytes, ref pos, UserIdLen, Define.MAX_NICKLEN);

                NickLen = ReaduShort(bytes, ref pos);
                NewUserNick = ReadStringW(bytes, ref pos, NickLen, Define.MAX_NICKLEN);

            }
        };

        public struct _GOOGLE_PLAY_LOGIN_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GOOGLE_PLAY_LOGIN_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public ushort UserIdLen;
            public byte[] UserId;

            public ushort GoogleKeyLen;
            public byte[] GoogleKey;

            public ushort NickKeyLen;
            public byte[] NickName;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                UserIdLen = ReaduShort(bytes, ref pos);
                UserId = ReadStringW(bytes, ref pos, UserIdLen, Define.MAX_NICKLEN);

                GoogleKeyLen = ReaduShort(bytes, ref pos);
                GoogleKey = ReadStringW(bytes, ref pos, GoogleKeyLen, Define.MAX_GOOGLE_KEY_LEN);

                NickKeyLen = ReaduShort(bytes, ref pos);
                NickName = ReadStringW(bytes, ref pos, NickKeyLen, Define.MAX_NICKLEN);

            }
        };

        public struct _GOOGLE_ACCOUNT_LINK_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GOOGLE_ACCOUNT_LINK_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public ushort UserIdLen;
            public byte[] UserId;

            public ushort GoogleKeyLen;
            public byte[] GoogleKey;

            public ushort NickKeyLen;
            public byte[] NickName;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                UserIdLen = ReaduShort(bytes, ref pos);
                UserId = ReadStringW(bytes, ref pos, UserIdLen, Define.MAX_NICKLEN);

                GoogleKeyLen = ReaduShort(bytes, ref pos);
                GoogleKey = ReadStringW(bytes, ref pos, UserIdLen, Define.MAX_GOOGLE_KEY_LEN);

                NickKeyLen = ReaduShort(bytes, ref pos);
                NickName = ReadStringW(bytes, ref pos, NickKeyLen, Define.MAX_NICKLEN);

            }
        };

        public struct _GOOGLE_KEY_CHECK_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GOOGLE_KEY_CHECK_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int ExistFlag;

            public ushort UserIdLen;
            public byte[] UserId;

            public ushort GoogleKeyLen;
            public byte[] GoogleKey;

            public ushort NickKeyLen;
            public byte[] NickName;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                ExistFlag = ReadInt(bytes, ref pos);

                UserIdLen = ReaduShort(bytes, ref pos);
                UserId = ReadStringW(bytes, ref pos, UserIdLen, Define.MAX_NICKLEN);

                GoogleKeyLen = ReaduShort(bytes, ref pos);
                GoogleKey = ReadStringW(bytes, ref pos, GoogleKeyLen, Define.MAX_GOOGLE_KEY_LEN);

                NickKeyLen = ReaduShort(bytes, ref pos);
                NickName = ReadStringW(bytes, ref pos, NickKeyLen, Define.MAX_NICKLEN);

            }
        };



        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _REMEMBERJOIN_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.REMEMBERJOIN_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;



            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);


            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _LOGIN_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int) + sizeof(int) + sizeof(ushort));
                head.type = (ushort)MsgType.LOGIN_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int userIndex;

            public ushort UserIDLen;
            public byte[] UserID;

            public ushort UserNickLen;
            public byte[] UserNick;

            public int Gold;
            public int Food;
            public int Crystal;

            public int nomal_ticket;
            public int premium_ticket;

            public int cur_world_idx;
            public int cur_stage_idx;
            public int cur_stage_clear;

            public int pvp_rank_point;

            public int EventShopSlotCount;
            public _EVENT_SHOP_INFO[] EventShopInfo;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;

                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                userIndex = ReadInt(bytes, ref pos);
                UserIDLen = ReaduShort(bytes, ref pos);
                UserID = ReadStringW(bytes, ref pos, UserIDLen, Define.MAX_NICKLEN);

                UserNickLen = ReaduShort(bytes, ref pos);
                UserNick = ReadStringW(bytes, ref pos, UserNickLen, Define.MAX_NICKLEN);

                Gold = ReadInt(bytes, ref pos);
                Food = ReadInt(bytes, ref pos);
                Crystal = ReadInt(bytes, ref pos);

                nomal_ticket = ReadInt(bytes, ref pos);
                premium_ticket = ReadInt(bytes, ref pos);

                cur_world_idx = ReadInt(bytes, ref pos);
                cur_stage_idx = ReadInt(bytes, ref pos);
                cur_stage_clear = ReadInt(bytes, ref pos);

                pvp_rank_point = ReadInt(bytes, ref pos);

                EventShopSlotCount = ReadInt(bytes, ref pos);
                EventShopInfo = new _EVENT_SHOP_INFO[Define.MAX_EVENT_SHOP_SLOT];

                for (int i = 0; i < Define.MAX_EVENT_SHOP_SLOT; i++)
                {
                    EventShopInfo[i].KeyLength = ReaduShort(bytes, ref pos);

                    byte[] byteKey = ReadStringW(bytes, ref pos, EventShopInfo[i].KeyLength, Define.SHOP_KEY_MAX_LENGTH);

                    EventShopInfo[i].ShopKey = System.Text.Encoding.Unicode.GetString(byteKey);
                    EventShopInfo[i].UnixTime = ReadInt(bytes, ref pos);
                }
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _SERVERLIST_ANS : NetMsgHeadInterface
        {
            public struct _SERVERINFO
            {
                public int ipLen;
                public byte[] server_ip;
                public int port;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int) + sizeof(int) + sizeof(ushort));
                head.type = (ushort)MsgType.SERVERLIST_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int server_count;
            public _SERVERINFO[] server_list;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;

                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                server_count = ReadInt(bytes, ref pos);
                server_list = new _SERVERINFO[server_count];
                for (int i = 0; i < server_count; ++i)
                {
                    server_list[i].ipLen = ReadInt(bytes, ref pos);
                    server_list[i].server_ip = ReadStringA(bytes, ref pos, server_list[i].ipLen, Define.SERVERIP_MAX_LEN);
                    server_list[i].port = ReadInt(bytes, ref pos);
                }
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _ACCEPT_GAME_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.ACCEPT_AUTH_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

            }
        };

        public struct _EVENT_SHOP_INFO
        {
            public ushort KeyLength;
            public string ShopKey;
            public int UnixTime;

        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _USER_IDENTIFIER_ANS : NetMsgHeadInterface
        {
            public struct _STAGE_INFO
            {
                public int Cur_World_idx; //현재 월드 인덱스 값(휴먼,엘프)
                public int Cur_Stage_idx; //현재 종족의 인덱스 값(1-1,1-2)
                public int Cur_Stage_Object; //현재 어떤 오브젝트인지
                public int Cur_Stage_Lv; // 현재 스테이지 레벨(금광, 크리스탈 레벨)

                public int Respawn_Time_yy; //현재 시간 
                public int Respawn_Time_mm;
                public int Respawn_Time_dd;

                public int Respawn_Time_h; //현재 시간 
                public int Respawn_Time_m;
                public int Respawn_Time_s;

                public int Object_state; //현재 오브젝트 상태(점령여부, 반란군 여부 )
                public int Stage_Object_id; //현재 스테이지의 오브젝트 id

                public int Rebellion_Time_yy; //반란군 시간
                public int Rebellion_Time_mm;
                public int Rebellion_Time_dd;

                public int Rebellion_Time_h; //반란군 시간
                public int Rebellion_Time_m;
                public int Rebellion_Time_s;
            };

            public struct _CARD_INFO
            {
                public int card_id;//카드 id
                public int guid;
                public int card_check;
                public int stack_count;
            };

            public struct _CASTLE_INFO
            {
                public int object_id;
                public int cur_world_idx;
                public int cur_stage_idx;
                public int cur_object_idx;
                public int cur_castle_lv;

                public ushort yy1;
                public byte mm1;
                public byte dd1;
                public byte h1;
                public byte m1;
                public byte s1;

                public ushort yy2;
                public byte mm2;
                public byte dd2;
                public byte h2;
                public byte m2;
                public byte s2;

                public ushort yy3;
                public byte mm3;
                public byte dd3;
                public byte h3;
                public byte m3;
                public byte s3;

                public ushort yy4;
                public byte mm4;
                public byte dd4;
                public byte h4;
                public byte m4;
                public byte s4;

                public ushort yy5;
                public byte mm5;
                public byte dd5;
                public byte h5;
                public byte m5;
                public byte s5;

                public ushort yy6;
                public byte mm6;
                public byte dd6;
                public byte h6;
                public byte m6;
                public byte s6;

                public ushort yy7;
                public byte mm7;
                public byte dd7;
                public byte h7;
                public byte m7;
                public byte s7;

                public ushort yy8;
                public byte mm8;
                public byte dd8;
                public byte h8;
                public byte m8;
                public byte s8;
            }

            public struct _STAGE_OBJ_INFO
            {
                public int cur_world_idx;
                public int cur_stage_idx;
                public int cur_object_idx;
            };

            public struct _DRAGON_CARD_INFO
            {
                public int tier;
                public int card_id1;
                public int card_id2;
                public int card_id3;
                public int card_id4;
                public int card_id5;
                public int card_id6;
                public int card_id7;
                public int card_id8;
            };
            public struct _DRAGON_INFO
            {
                public int challenge_count;
                public int drop_id1;
                public int drop_id2;
                public int drop_id3;
                public int drop_id4;
                public int drop_id5;
                public int drop_id6;
                public int day_count;
            };

            public struct _PVP_HISTORY_INFO
            {
                public int userID_Nick_Len;
                public byte[] userNick;

                public int my_score;
                public int my_rank_point;
                public int pvp_user_score;
                public int pvp_rank_point;
            }

            public struct _PVP_INFO
            {
                public int Challenge_count;
                public int day_count;
            }

            public struct _ATTENDANCE_INFO
            {
                public int step;
                public int step_check;
                public int attendance_check;
            }

            public struct _ATTENDANCE_CHECK_INFO
            {
                public int step;
                public int step_check;
            }

            public struct _CONTENTS_ONOFF
            {
                public int type; //type 1:mail
                public int on_off;
            }

            public struct _ITEM_INFO
            {
                public int item_id;
                public int item_check;
                public int guid;
            }

            public struct _HERO_INFO
            {
                public int Id;
                public int Level;
                public int Exp;
            }

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.USER_IDENTIFIER_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int userIndex;

            public ushort UserIDLen;
            public byte[] UserID;

            public ushort UserNickLen;
            public byte[] UserNick;

            public int isGuildJoined; //길드에 가입 되어 있는가?
            public int isGuildMaster; //길드장인가?
            public int guild_icn_idx; //길드 아이콘

            public int mail_count;
            public _CONTENTS_ONOFF[] contents_onoff;

            public int StageCount;
            public _STAGE_INFO[] stage_info;

            public int CardCount;
            public _CARD_INFO[] card_info;

            public int CastleCount;
            public _CASTLE_INFO[] castle_info;

            public int StageObjCount;
            public _STAGE_OBJ_INFO[] stage_obj_info;

            public int DragonCardCount;
            public _DRAGON_CARD_INFO[] dragon_card_info;

            public int DragonCount;
            public _DRAGON_INFO[] dragon_info;

            public int pvp_history_count;
            public _PVP_HISTORY_INFO[] PvpHistoryInfo;

            public int pvp_info_count;
            public _PVP_INFO[] pvpInfo;

            public int tutorial_check;
            public int tutorial_step;

            public int cardid1;
            public int cardid2;
            public int cardid3;
            public int cardid4;

            public int cardid5;
            public int cardid6;
            public int cardid7;
            public int cardid8;

            public int user_power;

            public int lucket_ticket;

            public _ATTENDANCE_INFO AttendanceInfo;

            public int check_count;
            public _ATTENDANCE_CHECK_INFO[] AttendanceCheckInfo;

            public int tutorial_event_info;

            public _ITEM_INFO[] ItemInfo;
            public int item_count;

            public int character_id;
            public int sword_id;
            public int armor_id;
            public int boots_id;
            public int helmets_id;

            public int HeroCount;
            public _HERO_INFO[] HeroInfo;
            
            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                userIndex = ReadInt(bytes, ref pos);
                UserIDLen = ReaduShort(bytes, ref pos);
                UserID = ReadStringW(bytes, ref pos, UserIDLen, Define.MAX_NICKLEN);

                UserNickLen = ReaduShort(bytes, ref pos);
                UserNick = ReadStringW(bytes, ref pos, UserNickLen, Define.MAX_NICKLEN);

                isGuildJoined = ReadInt(bytes, ref pos);
                isGuildMaster = ReadInt(bytes, ref pos);
                guild_icn_idx = ReadInt(bytes, ref pos);

                mail_count = ReadInt(bytes, ref pos);
                contents_onoff = new _CONTENTS_ONOFF[Define.CONTENTS_MAX_COUNT];
                for (int i = 0; i < Define.CONTENTS_MAX_COUNT; ++i)
                {
                    contents_onoff[i].type = ReadInt(bytes, ref pos);
                    contents_onoff[i].on_off = ReadInt(bytes, ref pos);
                }

                StageCount = ReadInt(bytes, ref pos);
                stage_info = new _STAGE_INFO[Define.STAGE_MAX_COUNT];

                for (int i = 0; i < stage_info.Length; i++)
                {
                    stage_info[i].Cur_World_idx = ReadInt(bytes, ref pos);
                    stage_info[i].Cur_Stage_idx = ReadInt(bytes, ref pos);
                    stage_info[i].Cur_Stage_Object = ReadInt(bytes, ref pos);
                    stage_info[i].Cur_Stage_Lv = ReadInt(bytes, ref pos);

                    stage_info[i].Respawn_Time_yy = ReadInt(bytes, ref pos);
                    stage_info[i].Respawn_Time_mm = ReadInt(bytes, ref pos);
                    stage_info[i].Respawn_Time_dd = ReadInt(bytes, ref pos);

                    stage_info[i].Respawn_Time_h = ReadInt(bytes, ref pos);
                    stage_info[i].Respawn_Time_m = ReadInt(bytes, ref pos);
                    stage_info[i].Respawn_Time_s = ReadInt(bytes, ref pos);

                    stage_info[i].Object_state = ReadInt(bytes, ref pos);
                    stage_info[i].Stage_Object_id = ReadInt(bytes, ref pos);

                    stage_info[i].Rebellion_Time_yy = ReadInt(bytes, ref pos);
                    stage_info[i].Rebellion_Time_mm = ReadInt(bytes, ref pos);
                    stage_info[i].Rebellion_Time_dd = ReadInt(bytes, ref pos);

                    stage_info[i].Rebellion_Time_h = ReadInt(bytes, ref pos);
                    stage_info[i].Rebellion_Time_m = ReadInt(bytes, ref pos);
                    stage_info[i].Rebellion_Time_s = ReadInt(bytes, ref pos);
                }

                CardCount = ReadInt(bytes, ref pos);
                card_info = new _CARD_INFO[Define.CARD_MAX_COUNT];

                for (int i = 0; i < card_info.Length; i++)
                {
                    card_info[i].card_id = ReadInt(bytes, ref pos);
                    card_info[i].guid = ReadInt(bytes, ref pos);
                    card_info[i].card_check = ReadInt(bytes, ref pos);
                    card_info[i].stack_count = ReadInt(bytes, ref pos);
                }

                CastleCount = ReadInt(bytes, ref pos);
                castle_info = new _CASTLE_INFO[Define.CASTLE_MAX_COUNT];

                for (int i = 0; i < castle_info.Length; i++)
                {
                    castle_info[i].object_id = ReadInt(bytes, ref pos);
                    castle_info[i].cur_world_idx = ReadInt(bytes, ref pos);
                    castle_info[i].cur_stage_idx = ReadInt(bytes, ref pos);
                    castle_info[i].cur_object_idx = ReadInt(bytes, ref pos);
                    castle_info[i].cur_castle_lv = ReadInt(bytes, ref pos);

                    castle_info[i].yy1 = ReaduShort(bytes, ref pos);
                    castle_info[i].mm1 = ReadByte(bytes, ref pos);
                    castle_info[i].dd1 = ReadByte(bytes, ref pos);
                    castle_info[i].h1 = ReadByte(bytes, ref pos);
                    castle_info[i].m1 = ReadByte(bytes, ref pos);
                    castle_info[i].s1 = ReadByte(bytes, ref pos);

                    castle_info[i].yy2 = ReaduShort(bytes, ref pos);
                    castle_info[i].mm2 = ReadByte(bytes, ref pos);
                    castle_info[i].dd2 = ReadByte(bytes, ref pos);
                    castle_info[i].h2 = ReadByte(bytes, ref pos);
                    castle_info[i].m2 = ReadByte(bytes, ref pos);
                    castle_info[i].s2 = ReadByte(bytes, ref pos);

                    castle_info[i].yy3 = ReaduShort(bytes, ref pos);
                    castle_info[i].mm3 = ReadByte(bytes, ref pos);
                    castle_info[i].dd3 = ReadByte(bytes, ref pos);
                    castle_info[i].h3 = ReadByte(bytes, ref pos);
                    castle_info[i].m3 = ReadByte(bytes, ref pos);
                    castle_info[i].s3 = ReadByte(bytes, ref pos);

                    castle_info[i].yy4 = ReaduShort(bytes, ref pos);
                    castle_info[i].mm4 = ReadByte(bytes, ref pos);
                    castle_info[i].dd4 = ReadByte(bytes, ref pos);
                    castle_info[i].h4 = ReadByte(bytes, ref pos);
                    castle_info[i].m4 = ReadByte(bytes, ref pos);
                    castle_info[i].s4 = ReadByte(bytes, ref pos);

                    castle_info[i].yy5 = ReaduShort(bytes, ref pos);
                    castle_info[i].mm5 = ReadByte(bytes, ref pos);
                    castle_info[i].dd5 = ReadByte(bytes, ref pos);
                    castle_info[i].h5 = ReadByte(bytes, ref pos);
                    castle_info[i].m5 = ReadByte(bytes, ref pos);
                    castle_info[i].s5 = ReadByte(bytes, ref pos);

                    castle_info[i].yy6 = ReaduShort(bytes, ref pos);
                    castle_info[i].mm6 = ReadByte(bytes, ref pos);
                    castle_info[i].dd6 = ReadByte(bytes, ref pos);
                    castle_info[i].h6 = ReadByte(bytes, ref pos);
                    castle_info[i].m6 = ReadByte(bytes, ref pos);
                    castle_info[i].s6 = ReadByte(bytes, ref pos);

                    castle_info[i].yy7 = ReaduShort(bytes, ref pos);
                    castle_info[i].mm7 = ReadByte(bytes, ref pos);
                    castle_info[i].dd7 = ReadByte(bytes, ref pos);
                    castle_info[i].h7 = ReadByte(bytes, ref pos);
                    castle_info[i].m7 = ReadByte(bytes, ref pos);
                    castle_info[i].s7 = ReadByte(bytes, ref pos);

                    castle_info[i].yy8 = ReaduShort(bytes, ref pos);
                    castle_info[i].mm8 = ReadByte(bytes, ref pos);
                    castle_info[i].dd8 = ReadByte(bytes, ref pos);
                    castle_info[i].h8 = ReadByte(bytes, ref pos);
                    castle_info[i].m8 = ReadByte(bytes, ref pos);
                    castle_info[i].s8 = ReadByte(bytes, ref pos);
                }

                StageObjCount = ReadInt(bytes, ref pos);
                stage_obj_info = new _STAGE_OBJ_INFO[Define.STAGE_OBJ_COUNT];
                for (int i = 0; i < stage_obj_info.Length; i++)
                {
                    stage_obj_info[i].cur_world_idx = ReadInt(bytes, ref pos);
                    stage_obj_info[i].cur_stage_idx = ReadInt(bytes, ref pos);
                    stage_obj_info[i].cur_object_idx = ReadInt(bytes, ref pos);
                }

                DragonCardCount = ReadInt(bytes, ref pos);
                dragon_card_info = new _DRAGON_CARD_INFO[Define.DRAGON_CARD_MAX_COUNT];

                for (int i = 0; i < dragon_card_info.Length; i++)
                {
                    dragon_card_info[i].tier = ReadInt(bytes, ref pos);
                    dragon_card_info[i].card_id1 = ReadInt(bytes, ref pos);
                    dragon_card_info[i].card_id2 = ReadInt(bytes, ref pos);
                    dragon_card_info[i].card_id3 = ReadInt(bytes, ref pos);
                    dragon_card_info[i].card_id4 = ReadInt(bytes, ref pos);
                    dragon_card_info[i].card_id5 = ReadInt(bytes, ref pos);
                    dragon_card_info[i].card_id6 = ReadInt(bytes, ref pos);
                    dragon_card_info[i].card_id7 = ReadInt(bytes, ref pos);
                    dragon_card_info[i].card_id8 = ReadInt(bytes, ref pos);
                }

                DragonCount = ReadInt(bytes, ref pos);
                dragon_info = new _DRAGON_INFO[Define.DRAGON_MAX_COUNT];

                for (int i = 0; i < dragon_info.Length; i++)
                {
                    dragon_info[i].challenge_count = ReadInt(bytes, ref pos);
                    dragon_info[i].drop_id1 = ReadInt(bytes, ref pos);
                    dragon_info[i].drop_id2 = ReadInt(bytes, ref pos);
                    dragon_info[i].drop_id3 = ReadInt(bytes, ref pos);
                    dragon_info[i].drop_id4 = ReadInt(bytes, ref pos);
                    dragon_info[i].drop_id5 = ReadInt(bytes, ref pos);
                    dragon_info[i].drop_id6 = ReadInt(bytes, ref pos);
                    dragon_info[i].day_count = ReadInt(bytes, ref pos);
                }

                pvp_history_count = ReadInt(bytes, ref pos);
                PvpHistoryInfo = new _PVP_HISTORY_INFO[Define.PVP_HISTORY_MAX_COUNT];

                for (int i = 0; i < Define.PVP_HISTORY_MAX_COUNT; i++)
                {
                    PvpHistoryInfo[i].userID_Nick_Len = ReadInt(bytes, ref pos);
                    PvpHistoryInfo[i].userNick = ReadStringW(bytes, ref pos, PvpHistoryInfo[i].userID_Nick_Len, Define.MAX_NICKLEN);

                    PvpHistoryInfo[i].my_score = ReadInt(bytes, ref pos);
                    PvpHistoryInfo[i].my_rank_point = ReadInt(bytes, ref pos);

                    PvpHistoryInfo[i].pvp_user_score = ReadInt(bytes, ref pos);
                    PvpHistoryInfo[i].pvp_rank_point = ReadInt(bytes, ref pos);
                }

                pvp_info_count = ReadInt(bytes, ref pos);
                pvpInfo = new _PVP_INFO[Define.PVP_INFO_MAX_COUNT];

                for (int i = 0; i < Define.PVP_INFO_MAX_COUNT; i++)
                {
                    pvpInfo[i].Challenge_count = ReadInt(bytes, ref pos);
                    pvpInfo[i].day_count = ReadInt(bytes, ref pos);
                }

                tutorial_check = ReadInt(bytes, ref pos);
                tutorial_step = ReadInt(bytes, ref pos);

                cardid1 = ReadInt(bytes, ref pos);
                cardid2 = ReadInt(bytes, ref pos);
                cardid3 = ReadInt(bytes, ref pos);
                cardid4 = ReadInt(bytes, ref pos);

                cardid5 = ReadInt(bytes, ref pos);
                cardid6 = ReadInt(bytes, ref pos);
                cardid7 = ReadInt(bytes, ref pos);
                cardid8 = ReadInt(bytes, ref pos);

                user_power = ReadInt(bytes, ref pos);

                lucket_ticket = ReadInt(bytes, ref pos);

                AttendanceInfo.step = ReadInt(bytes, ref pos);
                AttendanceInfo.step_check = ReadInt(bytes, ref pos);
                AttendanceInfo.attendance_check = ReadInt(bytes, ref pos);

                check_count = ReadInt(bytes, ref pos);
                AttendanceCheckInfo = new _ATTENDANCE_CHECK_INFO[Define.ATTENDANCE_MAX_COUNT];

                for (int i = 0; i < Define.ATTENDANCE_MAX_COUNT; i++)
                {
                    AttendanceCheckInfo[i].step = ReadInt(bytes, ref pos);
                    AttendanceCheckInfo[i].step_check = ReadInt(bytes, ref pos);
                }

                tutorial_event_info = ReadInt(bytes, ref pos);

                item_count = ReadInt(bytes, ref pos);
                ItemInfo = new _ITEM_INFO[Define.ITEM_MAX_COUNT];

                for (int i = 0; i < Define.ITEM_MAX_COUNT; i++)
                {
                    ItemInfo[i].item_id = ReadInt(bytes, ref pos);
                    ItemInfo[i].item_check = ReadInt(bytes, ref pos);
                    ItemInfo[i].guid = ReadInt(bytes, ref pos);
                }

                character_id = ReadInt(bytes, ref pos);
                sword_id = ReadInt(bytes, ref pos);
                armor_id = ReadInt(bytes, ref pos);
                boots_id = ReadInt(bytes, ref pos);
                helmets_id = ReadInt(bytes, ref pos);

                HeroCount = ReadInt(bytes, ref pos);
                HeroInfo = new _HERO_INFO[Define.MAX_HERO_COUNT];

                for (int i = 0; i < HeroCount; i++)
                {
                    HeroInfo[i].Id = ReadInt(bytes, ref pos);
                    HeroInfo[i].Level = ReadInt(bytes, ref pos);
                    HeroInfo[i].Exp = ReadInt(bytes, ref pos);
                }
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _USER_GOODS_UPGRADE_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.USER_GOODS_UPGRADE_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int gold;
            public int food;
            public int crystal;

            public int next_objectid;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                gold = ReadInt(bytes, ref pos);
                food = ReadInt(bytes, ref pos);
                crystal = ReadInt(bytes, ref pos);

                next_objectid = ReadInt(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _USER_GOODS_GET_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.USER_GOODS_GET_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int gold;
            public int food;

            public int db_time_yy;
            public int db_time_mm;
            public int db_time_dd;

            public int db_time_h;
            public int db_time_m;
            public int db_time_s;

            public int time_yy;
            public int time_mm;
            public int time_dd;

            public int time_h;
            public int time_m;
            public int time_s;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                gold = ReadInt(bytes, ref pos);
                food = ReadInt(bytes, ref pos);

                db_time_yy = ReadInt(bytes, ref pos);
                db_time_mm = ReadInt(bytes, ref pos);
                db_time_dd = ReadInt(bytes, ref pos);

                db_time_h = ReadInt(bytes, ref pos);
                db_time_m = ReadInt(bytes, ref pos);
                db_time_s = ReadInt(bytes, ref pos);

                time_yy = ReadInt(bytes, ref pos);
                time_mm = ReadInt(bytes, ref pos);
                time_dd = ReadInt(bytes, ref pos);

                time_h = ReadInt(bytes, ref pos);
                time_m = ReadInt(bytes, ref pos);
                time_s = ReadInt(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _CONQUEST_ANS : NetMsgHeadInterface
        {
            public struct _REWARD_INFO
            {
                public int item_id;
                public int count;
                public int type;
                public int guid;
                public int stack_count;
                public int gold;
            };

            public struct _STAGE_OBJECT_ID
            {
                public int object_id;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.CONQUEST_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int time_yy;
            public int time_mm;
            public int time_dd;

            public int time_h;
            public int time_m;
            public int time_s;

            public int object_id;
            public int stage_object_id;

            public int win_lose;//승리 패배

            public int reward_count;

            public int cur_object_idx; //현재 클리어 한 정보

            public int cur_world_idx; //현재 월드맵 인덱스
            public int cur_stage_idx; //현재 스테이지 인덱스

            public int rebellion_time_yy;
            public int rebellion_time_mm;
            public int rebellion_time_dd;

            public int rebellion_time_h; //반란군 시간
            public int rebellion_time_m;
            public int rebellion_time_s;

            public int Resultfood;

            public _REWARD_INFO[] reward_info;
                     

            public int stage_object_count;
            public _STAGE_OBJECT_ID[] StageObjectID;

            public int castle_objectid;
            public int castle_worldidx;
            public int castle_stageidx;
            public int castle_objectidx;

            public int castle_lv;

            public int castle_yy;
            public int castle_mm;
            public int castle_dd;
            public int castle_h;
            public int castle_m;
            public int castle_s;

            public int clear_cur_world_idx;
            public int clear_cur_stage_idx;
            public int clear_cur_object_idx;
            public int clear_end_stage_clear;
            public int[] clear_objectid;

            public int castleFlag;
            public int stageFlag;

            public int HeroID;
            public int lv;
            public int exp;
            public int GetExp;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                time_yy = ReadInt(bytes, ref pos);
                time_mm = ReadInt(bytes, ref pos);
                time_dd = ReadInt(bytes, ref pos);

                time_h = ReadInt(bytes, ref pos);
                time_m = ReadInt(bytes, ref pos);
                time_s = ReadInt(bytes, ref pos);

                object_id = ReadInt(bytes, ref pos);
                stage_object_id = ReadInt(bytes, ref pos);

                win_lose = ReadInt(bytes, ref pos);
                reward_count = ReadInt(bytes, ref pos);
                cur_object_idx = ReadInt(bytes, ref pos);
                cur_world_idx = ReadInt(bytes, ref pos);
                cur_stage_idx = ReadInt(bytes, ref pos);

                rebellion_time_yy = ReadInt(bytes, ref pos);
                rebellion_time_mm = ReadInt(bytes, ref pos);
                rebellion_time_dd = ReadInt(bytes, ref pos);

                rebellion_time_h = ReadInt(bytes, ref pos);
                rebellion_time_m = ReadInt(bytes, ref pos);
                rebellion_time_s = ReadInt(bytes, ref pos);

                Resultfood = ReadInt(bytes, ref pos);

                reward_info = new _REWARD_INFO[Define.REWARD_MAX_COUNT];

                for (int i = 0; i < Define.REWARD_MAX_COUNT; ++i)
                {
                    reward_info[i].item_id = ReadInt(bytes, ref pos);
                    reward_info[i].count = ReadInt(bytes, ref pos);
                    reward_info[i].type = ReadInt(bytes, ref pos);
                    reward_info[i].guid = ReadInt(bytes, ref pos);
                    reward_info[i].stack_count = ReadInt(bytes, ref pos);
                    reward_info[i].gold = ReadInt(bytes, ref pos);
                }
                            

                stage_object_count = ReadInt(bytes, ref pos);
                StageObjectID = new _STAGE_OBJECT_ID[Define.STAGE_OBJECT_ID_MAX_COUNT];

                for (int i = 0; i < Define.STAGE_OBJECT_ID_MAX_COUNT; i++)
                {
                    StageObjectID[i].object_id = ReadInt(bytes, ref pos);
                }

                castle_objectid = ReadInt(bytes, ref pos);
                castle_worldidx = ReadInt(bytes, ref pos);
                castle_stageidx = ReadInt(bytes, ref pos);
                castle_objectidx = ReadInt(bytes, ref pos);

                castle_lv = ReadInt(bytes, ref pos);

                castle_yy = ReadInt(bytes, ref pos);
                castle_mm = ReadInt(bytes, ref pos);
                castle_dd = ReadInt(bytes, ref pos);
                castle_h = ReadInt(bytes, ref pos);
                castle_m = ReadInt(bytes, ref pos);
                castle_s = ReadInt(bytes, ref pos);

                clear_cur_world_idx = ReadInt(bytes, ref pos);
                clear_cur_stage_idx = ReadInt(bytes, ref pos);
                clear_cur_object_idx = ReadInt(bytes, ref pos);
                clear_end_stage_clear = ReadInt(bytes, ref pos);

                clear_objectid = new int[18];

                for (int i = 0; i < 18; i++)
                {
                    clear_objectid[i] = ReadInt(bytes, ref pos);
                }

                castleFlag = ReadInt(bytes, ref pos);
                stageFlag = ReadInt(bytes, ref pos);

                HeroID = ReadInt(bytes, ref pos);
                lv = ReadInt(bytes, ref pos);
                exp = ReadInt(bytes, ref pos);
                GetExp = ReadInt(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _SKIP_CONQUEST_ANS : NetMsgHeadInterface
        {
            public struct _REWARD_INFO
            {
                public int item_id;
                public int count;
                public int type;
                public int guid;
                public int stack_count;
            };

            public struct _STAGE_OBJECT_ID
            {
                public int object_id;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.SKIP_CONQUEST_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int time_yy;
            public int time_mm;
            public int time_dd;

            public int time_h;
            public int time_m;
            public int time_s;

            public int object_id;
            public int stage_object_id;

            public int win_lose;//승리 패배

            public int reward_count;

            public int cur_object_idx; //현재 클리어 한 정보

            public int cur_world_idx; //현재 월드맵 인덱스
            public int cur_stage_idx; //현재 스테이지 인덱스

            public int rebellion_time_yy;
            public int rebellion_time_mm;
            public int rebellion_time_dd;

            public int rebellion_time_h; //반란군 시간
            public int rebellion_time_m;
            public int rebellion_time_s;

            public int Resultfood;

            public _REWARD_INFO[] reward_info;

            public int stage_object_count;
            public _STAGE_OBJECT_ID[] StageObjectID;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                time_yy = ReadInt(bytes, ref pos);
                time_mm = ReadInt(bytes, ref pos);
                time_dd = ReadInt(bytes, ref pos);

                time_h = ReadInt(bytes, ref pos);
                time_m = ReadInt(bytes, ref pos);
                time_s = ReadInt(bytes, ref pos);

                object_id = ReadInt(bytes, ref pos);
                stage_object_id = ReadInt(bytes, ref pos);

                win_lose = ReadInt(bytes, ref pos);
                reward_count = ReadInt(bytes, ref pos);
                cur_object_idx = ReadInt(bytes, ref pos);
                cur_world_idx = ReadInt(bytes, ref pos);
                cur_stage_idx = ReadInt(bytes, ref pos);

                rebellion_time_yy = ReadInt(bytes, ref pos);
                rebellion_time_mm = ReadInt(bytes, ref pos);
                rebellion_time_dd = ReadInt(bytes, ref pos);

                rebellion_time_h = ReadInt(bytes, ref pos);
                rebellion_time_m = ReadInt(bytes, ref pos);
                rebellion_time_s = ReadInt(bytes, ref pos);

                Resultfood = ReadInt(bytes, ref pos);

                reward_info = new _REWARD_INFO[Define.REWARD_MAX_COUNT];

                for (int i = 0; i < Define.REWARD_MAX_COUNT; ++i)
                {
                    reward_info[i].item_id = ReadInt(bytes, ref pos);
                    reward_info[i].count = ReadInt(bytes, ref pos);
                    reward_info[i].type = ReadInt(bytes, ref pos);
                    reward_info[i].guid = ReadInt(bytes, ref pos);
                    reward_info[i].stack_count = ReadInt(bytes, ref pos);
                }

                stage_object_count = ReadInt(bytes, ref pos);
                StageObjectID = new _STAGE_OBJECT_ID[Define.STAGE_OBJECT_ID_MAX_COUNT];

                for (int i = 0; i < Define.STAGE_OBJECT_ID_MAX_COUNT; i++)
                {
                    StageObjectID[i].object_id = ReadInt(bytes, ref pos);
                }
            }
        };




        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _CASTLE_CONQUEST_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.CASTLE_CONQUEST_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int object_id;
            public int cur_world_idx;
            public int cur_stage_idx;
            public int cur_object_idx;
            public int cur_castle_lv;

            public int respawn_castle_Time_yy;
            public int respawn_castle_Time_mm;
            public int respawn_castle_Time_dd;
            public int respawn_castle_Time_h;
            public int respawn_castle_Time_m;
            public int respawn_castle_Time_s;

            public int StageClearFlag;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                object_id = ReadInt(bytes, ref pos);
                cur_world_idx = ReadInt(bytes, ref pos);
                cur_stage_idx = ReadInt(bytes, ref pos);
                cur_object_idx = ReadInt(bytes, ref pos);
                cur_castle_lv = ReadInt(bytes, ref pos);

                respawn_castle_Time_yy = ReadInt(bytes, ref pos);
                respawn_castle_Time_mm = ReadInt(bytes, ref pos);
                respawn_castle_Time_dd = ReadInt(bytes, ref pos);
                respawn_castle_Time_h = ReadInt(bytes, ref pos);
                respawn_castle_Time_m = ReadInt(bytes, ref pos);
                respawn_castle_Time_s = ReadInt(bytes, ref pos);

                StageClearFlag = ReadInt(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _STAGE_CLEAR_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.STAGE_CLEAR_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;
            public int cur_world_idx;
            public int cur_stage_idx;
            public int cur_object_idx;
            public int end_stage_clear;
            //public int stage_count;
            public int[] objectid;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                cur_world_idx = ReadInt(bytes, ref pos);
                cur_stage_idx = ReadInt(bytes, ref pos);
                cur_object_idx = ReadInt(bytes, ref pos);
                end_stage_clear = ReadInt(bytes, ref pos);
                //stage_count = ReadInt(bytes, ref pos);
                objectid = new int[18];

                for (int i = 0; i < 18; i++)
                {
                    objectid[i] = ReadInt(bytes, ref pos);
                }
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _GET_CARD_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GET_CARD_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int card_id;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                card_id = ReadInt(bytes, ref pos);
            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _MIX_CARD_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.MIX_CARD_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int gold;
            public int new_card_id;
            public int new_guid;
            public int new_stack_count;

            public int baseGuid;

            public int Slot1_mat_card_count;
            public int[] Slot1_mat_guids;

            public int Slot2_mat_card_count;
            public int[] Slot2_mat_guids;

            public int Slot3_mat_card_count;
            public int[] Slot3_mat_guids;


            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                gold = ReadInt(bytes, ref pos);
                new_card_id = ReadInt(bytes, ref pos);
                new_guid = ReadInt(bytes, ref pos);
                new_stack_count = ReadInt(bytes, ref pos);

                baseGuid = ReadInt(bytes, ref pos);

                Slot1_mat_card_count = ReadInt(bytes, ref pos);
                Slot1_mat_guids = new int[5];

                for (int i = 0; i < 5; ++i)
                {
                    Slot1_mat_guids[i] = ReadInt(bytes, ref pos);
                }

                Slot2_mat_card_count = ReadInt(bytes, ref pos);
                Slot2_mat_guids = new int[5];

                for (int i = 0; i < 5; ++i)
                {
                    Slot2_mat_guids[i] = ReadInt(bytes, ref pos);
                }

                Slot3_mat_card_count = ReadInt(bytes, ref pos);
                Slot3_mat_guids = new int[5];

                for (int i = 0; i < 5; ++i)
                {
                    Slot3_mat_guids[i] = ReadInt(bytes, ref pos);
                }


            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _UNIT_CARD_APPOINT_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.UNIT_CARD_APPOINT_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int card_id1;
            public int card_id2;
            public int card_id3;
            public int card_id4;

            public int card_id5;
            public int card_id6;
            public int card_id7;
            public int card_id8;

            public int my_power;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                card_id1 = ReadInt(bytes, ref pos);
                card_id2 = ReadInt(bytes, ref pos);
                card_id3 = ReadInt(bytes, ref pos);
                card_id4 = ReadInt(bytes, ref pos);

                card_id5 = ReadInt(bytes, ref pos);
                card_id6 = ReadInt(bytes, ref pos);
                card_id7 = ReadInt(bytes, ref pos);
                card_id8 = ReadInt(bytes, ref pos);

                my_power = ReadInt(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _SELECT_DROP_ID_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.SELECT_DROP_ID_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int drop_id;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                drop_id = ReadInt(bytes, ref pos);

            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _BATTLE_START_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.BATTLE_START_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int card_id1;
            public int card_id2;
            public int card_id3;
            public int card_id4;

            public int card_id5;
            public int card_id6;
            public int card_id7;
            public int card_id8;

            public int MonsterGroupId;
            public int ItemDropId;

            public int my_power;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                card_id1 = ReadInt(bytes, ref pos);
                card_id2 = ReadInt(bytes, ref pos);
                card_id3 = ReadInt(bytes, ref pos);
                card_id4 = ReadInt(bytes, ref pos);

                card_id5 = ReadInt(bytes, ref pos);
                card_id6 = ReadInt(bytes, ref pos);
                card_id7 = ReadInt(bytes, ref pos);
                card_id8 = ReadInt(bytes, ref pos);

                MonsterGroupId = ReadInt(bytes, ref pos);
                ItemDropId = ReadInt(bytes, ref pos);

                my_power = ReadInt(bytes, ref pos);
            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _SKIP_BATTLE_START_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.SKIP_BATTLE_START_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int card_id1;
            public int card_id2;
            public int card_id3;
            public int card_id4;

            public int card_id5;
            public int card_id6;
            public int card_id7;
            public int card_id8;

            public int MonsterGroupId;
            public int ItemDropId;

            public int my_power;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                card_id1 = ReadInt(bytes, ref pos);
                card_id2 = ReadInt(bytes, ref pos);
                card_id3 = ReadInt(bytes, ref pos);
                card_id4 = ReadInt(bytes, ref pos);

                card_id5 = ReadInt(bytes, ref pos);
                card_id6 = ReadInt(bytes, ref pos);
                card_id7 = ReadInt(bytes, ref pos);
                card_id8 = ReadInt(bytes, ref pos);

                MonsterGroupId = ReadInt(bytes, ref pos);
                ItemDropId = ReadInt(bytes, ref pos);

                my_power = ReadInt(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _CASTLE_UPGRADE_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.CASTLE_UPGRADE_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int RemainGold;
            public int RemainFood;
            public int RemainCrystal;

            public int OpenSlot_Year;
            public int OpenSlot_Month;
            public int OpenSlot_Day;
            public int OpenSlot_Hour;
            public int OpenSlot_Min;
            public int OpenSlot_Sec;
            public int CastleObjectId;

            public int result;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                RemainGold = ReadInt(bytes, ref pos);
                RemainFood = ReadInt(bytes, ref pos);
                RemainCrystal = ReadInt(bytes, ref pos);

                OpenSlot_Year = ReadInt(bytes, ref pos);
                OpenSlot_Month = ReadInt(bytes, ref pos);
                OpenSlot_Day = ReadInt(bytes, ref pos);
                OpenSlot_Hour = ReadInt(bytes, ref pos);
                OpenSlot_Min = ReadInt(bytes, ref pos);
                OpenSlot_Sec = ReadInt(bytes, ref pos);

                CastleObjectId = ReadInt(bytes, ref pos);

                result = ReadInt(bytes, ref pos);
            }
        };

        

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _PVP_STAGEINFO_ANS : NetMsgHeadInterface
        {
            public struct _PVP_STAGE_INFO
            {
                public int world_idx;
                public int stage_idx;
                public int stage_object;
                public int cur_stage_lv;
                public int time_yy;
                public int time_mm;
                public int time_dd;
                public int time_h;
                public int time_m;
                public int time_s;
                public int object_state;
                public int stage_object_id;
                public int rebellion_time_yy;
                public int rebellion_time_mm;
                public int rebellion_time_dd;
                public int rebellion_time_h;
                public int rebellion_time_m;
                public int rebellion_time_s;
                public int get_check;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.PVP_STAGEINFO_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int stage_count;
            public int pvp_time;
            public int pvp_end_clear;
            public int pvp_end_world_idx;

            public _PVP_STAGE_INFO[] pvp_stage_info;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                stage_count = ReadInt(bytes, ref pos);
                pvp_time = ReadInt(bytes, ref pos);
                pvp_end_clear = ReadInt(bytes, ref pos);
                pvp_end_world_idx = ReadInt(bytes, ref pos);

                pvp_stage_info = new _PVP_STAGE_INFO[Define.STAGE_MAX_COUNT];

                for (int i = 0; i < stage_count; i++)
                {
                    pvp_stage_info[i].world_idx = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].stage_idx = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].stage_object = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].cur_stage_lv = ReadInt(bytes, ref pos);

                    pvp_stage_info[i].time_yy = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].time_mm = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].time_dd = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].time_h = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].time_m = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].time_s = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].object_state = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].stage_object_id = ReadInt(bytes, ref pos);

                    pvp_stage_info[i].rebellion_time_yy = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].rebellion_time_mm = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].rebellion_time_dd = ReadInt(bytes, ref pos);

                    pvp_stage_info[i].rebellion_time_h = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].rebellion_time_m = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].rebellion_time_s = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].get_check = ReadInt(bytes, ref pos);
                }
            }
        };

        //PVP_LOSE_NOTI
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _PVP_LOSE_NOTI : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.PVP_LOSE_NOTI;
            }



            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int gold;
            public int food;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                gold = ReadInt(bytes, ref pos);
                food = ReadInt(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _PVP_LOGIN_LOSE_NOTI_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.PVP_LOGIN_LOSE_NOTI_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int gold;
            public int food;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                gold = ReadInt(bytes, ref pos);
                food = ReadInt(bytes, ref pos);
            }
        };

        ///////////////////////////////////////////////////////////////////////////////////////////		
        /////////////////////////배틀 데이터 공용 구조체////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////
        public struct _BATTLESTATUS_ANS
        {
            public int Attack;
            public int Defence;
            public int Speed;
            public int MaxHealth;
            public int CurrentHealth;
            public int StackCount;
        };

        public struct _BATTLECHARACTER_ANS
        {
            public int IsAlly;
            public int index;
            public int ProgressState;
            public _BATTLESTATUS_ANS CharacterBattleStat;
        };

        public struct _BATTLEREPORT_ANS
        {
            public int ActionCount;
            public int ActiveSkillNumber;
            public int InoutFlag;
            public _BATTLECHARACTER_ANS ActiveCharacter;
            public _BATTLECHARACTER_ANS[] TargetCharacter;
        };

        ///////////////////////////////////////////////////////////////////////////////////////////		
        /////////////////////////배틀 데이터 공용 구조체////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _BATTLE_REPORT_ANS : NetMsgHeadInterface
        {
            public struct _BATTLE_UNIT_ANS
            {
                public int IsAlly;
                public int index;
                public int CardId;
                public _BATTLESTATUS_ANS CharacterBattleStat;
            };


            public void initNetHead()
            {

            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public _BATTLE_UNIT_ANS[] PlayerUnits;
            public _BATTLE_UNIT_ANS[] EnemyUnits;
            public int WinLose;
            public int BattleEndFlag;
            public int ReportCount;
            public _BATTLEREPORT_ANS[] ReportList;

            public void ReadRecvBuffer(byte[] bytes)
            {

            }

            public void ReadRecvBuffer(byte[] bytes, ref int pos)
            {
                PlayerUnits = new _BATTLE_UNIT_ANS[Define.BATTLE_ALL_MAX];
                EnemyUnits = new _BATTLE_UNIT_ANS[Define.BATTLE_ALL_MAX];

                for (int i = 0; i < Define.BATTLE_ALL_MAX; i++)
                {
                    PlayerUnits[i].IsAlly = ReadInt(bytes, ref pos);
                    PlayerUnits[i].index = ReadInt(bytes, ref pos);
                    PlayerUnits[i].CardId = ReadInt(bytes, ref pos);

                    PlayerUnits[i].CharacterBattleStat.Attack = ReadInt(bytes, ref pos);
                    PlayerUnits[i].CharacterBattleStat.Defence = ReadInt(bytes, ref pos);
                    PlayerUnits[i].CharacterBattleStat.Speed = ReadInt(bytes, ref pos);
                    PlayerUnits[i].CharacterBattleStat.MaxHealth = ReadInt(bytes, ref pos);
                    PlayerUnits[i].CharacterBattleStat.CurrentHealth = ReadInt(bytes, ref pos);
                    PlayerUnits[i].CharacterBattleStat.StackCount = ReadInt(bytes, ref pos);
                }

                for (int i = 0; i < Define.BATTLE_ALL_MAX; i++)
                {
                    EnemyUnits[i].IsAlly = ReadInt(bytes, ref pos);
                    EnemyUnits[i].index = ReadInt(bytes, ref pos);
                    EnemyUnits[i].CardId = ReadInt(bytes, ref pos);

                    EnemyUnits[i].CharacterBattleStat.Attack = ReadInt(bytes, ref pos);
                    EnemyUnits[i].CharacterBattleStat.Defence = ReadInt(bytes, ref pos);
                    EnemyUnits[i].CharacterBattleStat.Speed = ReadInt(bytes, ref pos);
                    EnemyUnits[i].CharacterBattleStat.MaxHealth = ReadInt(bytes, ref pos);
                    EnemyUnits[i].CharacterBattleStat.CurrentHealth = ReadInt(bytes, ref pos);
                    EnemyUnits[i].CharacterBattleStat.StackCount = ReadInt(bytes, ref pos);
                }

                WinLose = ReadInt(bytes, ref pos);
                BattleEndFlag = ReadInt(bytes, ref pos);
                ReportCount = ReadInt(bytes, ref pos);
                ReportList = new _BATTLEREPORT_ANS[Define.BATTLE_REPORT_MAX];

                for (int i = 0; i < Define.BATTLE_REPORT_MAX; i++)
                {
                    ReportList[i].ActionCount = ReadInt(bytes, ref pos);
                    ReportList[i].ActiveSkillNumber = ReadInt(bytes, ref pos);
                    ReportList[i].InoutFlag = ReadInt(bytes, ref pos);

                    ReportList[i].ActiveCharacter.IsAlly = ReadInt(bytes, ref pos);
                    ReportList[i].ActiveCharacter.index = ReadInt(bytes, ref pos);
                    ReportList[i].ActiveCharacter.ProgressState = ReadInt(bytes, ref pos);

                    ReportList[i].ActiveCharacter.CharacterBattleStat.Attack = ReadInt(bytes, ref pos);
                    ReportList[i].ActiveCharacter.CharacterBattleStat.Defence = ReadInt(bytes, ref pos);
                    ReportList[i].ActiveCharacter.CharacterBattleStat.Speed = ReadInt(bytes, ref pos);
                    ReportList[i].ActiveCharacter.CharacterBattleStat.MaxHealth = ReadInt(bytes, ref pos);
                    ReportList[i].ActiveCharacter.CharacterBattleStat.CurrentHealth = ReadInt(bytes, ref pos);
                    ReportList[i].ActiveCharacter.CharacterBattleStat.StackCount = ReadInt(bytes, ref pos);

                    ReportList[i].TargetCharacter = new _BATTLECHARACTER_ANS[Define.BATTLE_TARGET_MAX];

                    for (int j = 0; j < Define.BATTLE_TARGET_MAX; j++)
                    {
                        ReportList[i].TargetCharacter[j].IsAlly = ReadInt(bytes, ref pos);
                        ReportList[i].TargetCharacter[j].index = ReadInt(bytes, ref pos);
                        ReportList[i].TargetCharacter[j].ProgressState = ReadInt(bytes, ref pos);

                        ReportList[i].TargetCharacter[j].CharacterBattleStat.Attack = ReadInt(bytes, ref pos);
                        ReportList[i].TargetCharacter[j].CharacterBattleStat.Defence = ReadInt(bytes, ref pos);
                        ReportList[i].TargetCharacter[j].CharacterBattleStat.Speed = ReadInt(bytes, ref pos);
                        ReportList[i].TargetCharacter[j].CharacterBattleStat.MaxHealth = ReadInt(bytes, ref pos);
                        ReportList[i].TargetCharacter[j].CharacterBattleStat.CurrentHealth = ReadInt(bytes, ref pos);
                        ReportList[i].TargetCharacter[j].CharacterBattleStat.StackCount = ReadInt(bytes, ref pos);
                    }
                }
            }

        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _CONTINUE_BATTLE_REPORT_ANS : NetMsgHeadInterface
        {

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.CONTINUE_BATTLE_REPORT_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;
            public int BattleEndFlag;
            public int ReportCount;
            public _BATTLEREPORT_ANS[] ReportList;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;

                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                BattleEndFlag = ReadInt(bytes, ref pos);
                ReportCount = ReadInt(bytes, ref pos);
                ReportList = new _BATTLEREPORT_ANS[Define.BATTLE_REPORT_MAX];

                for (int i = 0; i < ReportCount; i++)
                {
                    ReportList[i].ActionCount = ReadInt(bytes, ref pos);
                    ReportList[i].ActiveSkillNumber = ReadInt(bytes, ref pos);
                    ReportList[i].InoutFlag = ReadInt(bytes, ref pos);

                    ReportList[i].ActiveCharacter.IsAlly = ReadInt(bytes, ref pos);
                    ReportList[i].ActiveCharacter.index = ReadInt(bytes, ref pos);
                    ReportList[i].ActiveCharacter.ProgressState = ReadInt(bytes, ref pos);

                    ReportList[i].ActiveCharacter.CharacterBattleStat.Attack = ReadInt(bytes, ref pos);
                    ReportList[i].ActiveCharacter.CharacterBattleStat.Defence = ReadInt(bytes, ref pos);
                    ReportList[i].ActiveCharacter.CharacterBattleStat.Speed = ReadInt(bytes, ref pos);
                    ReportList[i].ActiveCharacter.CharacterBattleStat.MaxHealth = ReadInt(bytes, ref pos);
                    ReportList[i].ActiveCharacter.CharacterBattleStat.CurrentHealth = ReadInt(bytes, ref pos);
                    ReportList[i].ActiveCharacter.CharacterBattleStat.StackCount = ReadInt(bytes, ref pos);

                    ReportList[i].TargetCharacter = new _BATTLECHARACTER_ANS[Define.BATTLE_TARGET_MAX];

                    for (int j = 0; j < Define.BATTLE_TARGET_MAX; j++)
                    {
                        ReportList[i].TargetCharacter[j].IsAlly = ReadInt(bytes, ref pos);
                        ReportList[i].TargetCharacter[j].index = ReadInt(bytes, ref pos);
                        ReportList[i].TargetCharacter[j].ProgressState = ReadInt(bytes, ref pos);

                        ReportList[i].TargetCharacter[j].CharacterBattleStat.Attack = ReadInt(bytes, ref pos);
                        ReportList[i].TargetCharacter[j].CharacterBattleStat.Defence = ReadInt(bytes, ref pos);
                        ReportList[i].TargetCharacter[j].CharacterBattleStat.Speed = ReadInt(bytes, ref pos);
                        ReportList[i].TargetCharacter[j].CharacterBattleStat.MaxHealth = ReadInt(bytes, ref pos);
                        ReportList[i].TargetCharacter[j].CharacterBattleStat.CurrentHealth = ReadInt(bytes, ref pos);
                        ReportList[i].TargetCharacter[j].CharacterBattleStat.StackCount = ReadInt(bytes, ref pos);
                    }
                }
            }

        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _BATTLE_END_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.BATTLE_END_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _REBELLION_ANS : NetMsgHeadInterface
        {
            public struct _REWARD_INFO
            {
                public int item_id;
                public int count;
                public int type;
                public int guid;
                public int stackcount;
                public int gold;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.REBELLION_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int time_yy;
            public int time_mm;
            public int time_dd;

            public int time_h;
            public int time_m;
            public int time_s;

            public int db_time_yy;
            public int db_time_mm;
            public int db_time_dd;

            public int db_time_h;
            public int db_time_m;
            public int db_time_s;

            public int win_lose;

            public int reward_count;
            public int result_food;

            public int HeroID;
            public int lv;
            public int exp;
            public int GetExp;

            public _REWARD_INFO[] reward_info;
            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                time_yy = ReadInt(bytes, ref pos);
                time_mm = ReadInt(bytes, ref pos);
                time_dd = ReadInt(bytes, ref pos);

                time_h = ReadInt(bytes, ref pos);
                time_m = ReadInt(bytes, ref pos);
                time_s = ReadInt(bytes, ref pos);

                db_time_yy = ReadInt(bytes, ref pos);
                db_time_mm = ReadInt(bytes, ref pos);
                db_time_dd = ReadInt(bytes, ref pos);

                db_time_h = ReadInt(bytes, ref pos);
                db_time_m = ReadInt(bytes, ref pos);
                db_time_s = ReadInt(bytes, ref pos);

                win_lose = ReadInt(bytes, ref pos);

                reward_count = ReadInt(bytes, ref pos);
                result_food = ReadInt(bytes, ref pos);

                HeroID = ReadInt(bytes, ref pos);
                lv = ReadInt(bytes, ref pos);
                exp = ReadInt(bytes, ref pos);
                GetExp = ReadInt(bytes, ref pos);

                reward_info = new _REWARD_INFO[Define.REWARD_MAX_COUNT];

                for (int i = 0; i < reward_count; ++i)
                {
                    reward_info[i].item_id = ReadInt(bytes, ref pos);
                    reward_info[i].count = ReadInt(bytes, ref pos);
                    reward_info[i].type = ReadInt(bytes, ref pos);
                    reward_info[i].guid = ReadInt(bytes, ref pos);
                    reward_info[i].stackcount = ReadInt(bytes, ref pos);
                    reward_info[i].gold = ReadInt(bytes, ref pos);
                }
                
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _SKIP_REBELLION_ANS : NetMsgHeadInterface
        {
            public struct _REWARD_INFO
            {
                public int item_id;
                public int count;
                public int type;
                public int guid;
                public int stackcount;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.SKIP_REBELLION_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int time_yy;
            public int time_mm;
            public int time_dd;

            public int time_h;
            public int time_m;
            public int time_s;

            public int db_time_yy;
            public int db_time_mm;
            public int db_time_dd;

            public int db_time_h;
            public int db_time_m;
            public int db_time_s;

            public int win_lose;

            public int reward_count;
            public int result_food;

            public _REWARD_INFO[] reward_info;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                time_yy = ReadInt(bytes, ref pos);
                time_mm = ReadInt(bytes, ref pos);
                time_dd = ReadInt(bytes, ref pos);

                time_h = ReadInt(bytes, ref pos);
                time_m = ReadInt(bytes, ref pos);
                time_s = ReadInt(bytes, ref pos);

                db_time_yy = ReadInt(bytes, ref pos);
                db_time_mm = ReadInt(bytes, ref pos);
                db_time_dd = ReadInt(bytes, ref pos);

                db_time_h = ReadInt(bytes, ref pos);
                db_time_m = ReadInt(bytes, ref pos);
                db_time_s = ReadInt(bytes, ref pos);

                win_lose = ReadInt(bytes, ref pos);

                reward_count = ReadInt(bytes, ref pos);
                result_food = ReadInt(bytes, ref pos);

                reward_info = new _REWARD_INFO[Define.REWARD_MAX_COUNT];

                for (int i = 0; i < reward_count; ++i)
                {
                    reward_info[i].item_id = ReadInt(bytes, ref pos);
                    reward_info[i].count = ReadInt(bytes, ref pos);
                    reward_info[i].type = ReadInt(bytes, ref pos);
                    reward_info[i].guid = ReadInt(bytes, ref pos);
                    reward_info[i].stackcount = ReadInt(bytes, ref pos);
                }
            }
        };

        public struct _PVP_HISTORY_INFO
        {
            public int userID_Nick_Len;
            public byte[] userNick;

            public int my_score;
            public int my_rank_point;

            public int pvp_user_score;
            public int pvp_rank_point;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _PVP_ANS : NetMsgHeadInterface
        {
            public struct _REWARD_INFO
            {
                public int item_id;
                public int count;
                public int type;
                public int guid;
                public int stackcount;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.PVP_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int my_add_point;
            public int pvp_user_add_point;
            public int my_rank_point;
            public int count;
            public int HistoryCount;
            public int ResultFood;
            public bool WinLoseFlag;
            //public _PVP_HISTORY_INFO[] pvp_history_info;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                my_add_point = ReadInt(bytes, ref pos);
                pvp_user_add_point = ReadInt(bytes, ref pos);
                my_rank_point = ReadInt(bytes, ref pos);
                count = ReadInt(bytes, ref pos);
                HistoryCount = ReadInt(bytes, ref pos);
                ResultFood = ReadInt(bytes, ref pos);
                WinLoseFlag = Readboolean(bytes,ref pos);
                //pvp_history_info = new _PVP_HISTORY_INFO[Define.PVP_HISTORY_MAX_COUNT];

                //for (int i = 0; i < Define.PVP_HISTORY_MAX_COUNT; i++)
                //            {
                //	pvp_history_info[i].userID_Nick_Len = ReadInt(bytes, ref pos);
                //	pvp_history_info[i].userNick = ReadStringW(bytes, ref pos, pvp_history_info[i].userID_Nick_Len, Define.MAX_NICKLEN);

                //	pvp_history_info[i].my_score = ReadInt(bytes, ref pos);
                //	pvp_history_info[i].my_rank_point = ReadInt(bytes, ref pos);

                //	pvp_history_info[i].pvp_user_score = ReadInt(bytes, ref pos);
                //	pvp_history_info[i].pvp_rank_point = ReadInt(bytes, ref pos);
                //}
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _SKIP_PVP_ANS : NetMsgHeadInterface
        {
            public struct _REWARD_INFO
            {
                public int item_id;
                public int count;
                public int type;
                public int guid;
                public int stackcount;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.SKIP_PVP_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int my_rank_point;
            public int count;
            public int HistoryCount;
            public int ResultFood;
            public int WinLose;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                my_rank_point = ReadInt(bytes, ref pos);
                count = ReadInt(bytes, ref pos);
                HistoryCount = ReadInt(bytes, ref pos);
                ResultFood = ReadInt(bytes, ref pos);
                WinLose = ReadInt(bytes, ref pos);

            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _GACHA_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GACHA_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int nomal_ticket;
            public int premium_ticket;
            public int gacha_num;
            public int[] card_id;
            public int[] guid;
            public int[] stackcount;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                nomal_ticket = ReadInt(bytes, ref pos);
                premium_ticket = ReadInt(bytes, ref pos);
                gacha_num = ReadInt(bytes, ref pos);

                card_id = new int[10];
                guid = new int[10];
                stackcount = new int[10];

                for (int i = 0; i < 10; i++)
                {
                    card_id[i] = ReadInt(bytes, ref pos);
                }
                for (int i = 0; i < 10; i++)
                {
                    guid[i] = ReadInt(bytes, ref pos);
                }
                for (int i = 0; i < 10; i++)
                {
                    stackcount[i] = ReadInt(bytes, ref pos);
                }
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _CRYSTAL_ANS : NetMsgHeadInterface
        {

            public struct _CARD_INFO
            {
                public int card_id;//카드 id
                public int guid;
                public int card_check;
                public int stack_count;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.CRYSTAL_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int crystal;
            public int gold;
            public int food;
            public int normal_ticket;
            public int premium_ticket;
            public int Lucky_ticket;

            public int EventShopCount;
            public _EVENT_SHOP_INFO[] EventShopList;

            public int CardCount;
            public _CARD_INFO[] CardList;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                crystal = ReadInt(bytes, ref pos);
                gold = ReadInt(bytes, ref pos);
                food = ReadInt(bytes, ref pos);
                normal_ticket = ReadInt(bytes, ref pos);
                premium_ticket = ReadInt(bytes, ref pos);
                Lucky_ticket = ReadInt(bytes, ref pos);


                EventShopCount = ReadInt(bytes, ref pos);
                EventShopList = new _EVENT_SHOP_INFO[Define.MAX_EVENT_SHOP_SLOT];

                for (int i = 0; i < Define.MAX_EVENT_SHOP_SLOT; i++)
                {
                    EventShopList[i].KeyLength = ReaduShort(bytes, ref pos);

                    byte[] byteKey = ReadStringW(bytes, ref pos, EventShopList[i].KeyLength, Define.SHOP_KEY_MAX_LENGTH);

                    EventShopList[i].ShopKey = System.Text.Encoding.Unicode.GetString(byteKey);
                    EventShopList[i].UnixTime = ReadInt(bytes, ref pos);
                }

                CardCount = ReadInt(bytes, ref pos);
                CardList = new _CARD_INFO[Define.CARD_MAX_COUNT];

                for (int i = 0; i < CardCount; i++)
                {
                    CardList[i].card_id = ReadInt(bytes, ref pos);
                    CardList[i].guid = ReadInt(bytes, ref pos);
                    CardList[i].card_check = ReadInt(bytes, ref pos);
                    CardList[i].stack_count = ReadInt(bytes, ref pos);
                }
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _DRAGON_EXPEDITION_ANS : NetMsgHeadInterface
        {
            public struct _DRAGON_INFO
            {
                public int tier;
                public int card_id1;
                public int card_id2;
                public int card_id3;
                public int card_id4;
                public int card_id5;
                public int card_id6;
                public int card_id7;
                public int card_id8;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.DRAGON_EXPEDITION_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int challenge_count;
            public int drop_id1;
            public int drop_id2;
            public int drop_id3;
            public int drop_id4;
            public int drop_id5;
            public int drop_id6;
            public int crystal;

            public _DRAGON_INFO[] dragon_info;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                challenge_count = ReadInt(bytes, ref pos);
                drop_id1 = ReadInt(bytes, ref pos);
                drop_id2 = ReadInt(bytes, ref pos);
                drop_id3 = ReadInt(bytes, ref pos);
                drop_id4 = ReadInt(bytes, ref pos);
                drop_id5 = ReadInt(bytes, ref pos);
                drop_id6 = ReadInt(bytes, ref pos);
                crystal = ReadInt(bytes, ref pos);

                dragon_info = new _DRAGON_INFO[Define.DRAGON_CARD_MAX_COUNT];

                for (int i = 0; i < Define.DRAGON_CARD_MAX_COUNT; i++)
                {
                    dragon_info[i].tier = ReadInt(bytes, ref pos);
                    dragon_info[i].card_id1 = ReadInt(bytes, ref pos);
                    dragon_info[i].card_id2 = ReadInt(bytes, ref pos);
                    dragon_info[i].card_id3 = ReadInt(bytes, ref pos);
                    dragon_info[i].card_id4 = ReadInt(bytes, ref pos);
                    dragon_info[i].card_id5 = ReadInt(bytes, ref pos);
                    dragon_info[i].card_id6 = ReadInt(bytes, ref pos);
                    dragon_info[i].card_id7 = ReadInt(bytes, ref pos);
                    dragon_info[i].card_id8 = ReadInt(bytes, ref pos);
                }
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _PVP_USERLIST_ANS : NetMsgHeadInterface
        {
            public struct _PVP_USERINFO
            {
                public int userID_Len;
                //wchar_t userID[MAX_NICKLEN];
                public byte[] userID;

                public int userID_Nick_Len;
                public byte[] userNick;

                public int combat_point;
                public int pvp_score;

                public int rank;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.PVP_USERLIST_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int my_rank;
            public int user_count;

            public _PVP_USERINFO[] pvp_userinfo;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                my_rank = ReadInt(bytes, ref pos);
                user_count = ReadInt(bytes, ref pos);
                pvp_userinfo = new _PVP_USERINFO[20];
                for (int i = 0; i < 20; ++i)
                {
                    pvp_userinfo[i].userID_Len = ReadInt(bytes, ref pos);
                    pvp_userinfo[i].userID = ReadStringW(bytes, ref pos, pvp_userinfo[i].userID_Len, Define.MAX_NICKLEN);

                    pvp_userinfo[i].userID_Nick_Len = ReadInt(bytes, ref pos);
                    pvp_userinfo[i].userNick = ReadStringW(bytes, ref pos, pvp_userinfo[i].userID_Nick_Len, Define.MAX_NICKLEN);

                    pvp_userinfo[i].combat_point = ReadInt(bytes, ref pos);
                    pvp_userinfo[i].pvp_score = ReadInt(bytes, ref pos);
                    pvp_userinfo[i].rank = ReadInt(bytes, ref pos);
                }
            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _USER_UNIT_DECK_SAVE_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.USER_UNIT_DECK_SAVE_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int card_id1;
            public int card_id2;
            public int card_id3;
            public int card_id4;

            public int card_id5;
            public int card_id6;
            public int card_id7;
            public int card_id8;

            public int MonsterGroupId;
            public int ItemDropId;


            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                card_id1 = ReadInt(bytes, ref pos);
                card_id2 = ReadInt(bytes, ref pos);
                card_id3 = ReadInt(bytes, ref pos);
                card_id4 = ReadInt(bytes, ref pos);

                card_id5 = ReadInt(bytes, ref pos);
                card_id6 = ReadInt(bytes, ref pos);
                card_id7 = ReadInt(bytes, ref pos);
                card_id8 = ReadInt(bytes, ref pos);

                MonsterGroupId = ReadInt(bytes, ref pos);
                ItemDropId = ReadInt(bytes, ref pos);
            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _CARD_STACK_ANS : NetMsgHeadInterface
        {

            public struct _CARD_INFO
            {
                public int card_id;//카드 id
                public int guid;
                public int card_check;
                public int stack_count;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.CARD_STACK_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public _CARD_INFO UpdateCardInfo;
            public int DeleteCardCount;
            public int[] DeleteCardGuidList;
            public int AddCardCount;
            public _CARD_INFO[] AddCardList;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                UpdateCardInfo.card_id = ReadInt(bytes, ref pos);
                UpdateCardInfo.guid = ReadInt(bytes, ref pos);
                UpdateCardInfo.card_check = ReadInt(bytes, ref pos);
                UpdateCardInfo.stack_count = ReadInt(bytes, ref pos);

                DeleteCardCount = ReadInt(bytes, ref pos);

                DeleteCardGuidList = new int[Define.CARD_MAX_COUNT];

                for (int i = 0; i < DeleteCardGuidList.Length; i++)
                {
                    DeleteCardGuidList[i] = ReadInt(bytes, ref pos);
                }

                AddCardCount = ReadInt(bytes, ref pos);

                AddCardList = new _CARD_INFO[Define.CARD_MAX_COUNT];

                for (int i = 0; i < AddCardCount; i++)
                {
                    AddCardList[i].card_id = ReadInt(bytes, ref pos);
                    AddCardList[i].guid = ReadInt(bytes, ref pos);
                    AddCardList[i].card_check = ReadInt(bytes, ref pos);
                    AddCardList[i].stack_count = ReadInt(bytes, ref pos);
                }
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _CARD_DIVISION_ANS : NetMsgHeadInterface
        {

            public struct _CARD_INFO
            {
                public int card_id;//카드 id
                public int guid;
                public int card_check;
                public int stack_count;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.CARD_DIVISION_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public _CARD_INFO UpdateCardInfo;
            public _CARD_INFO AddCardInfo;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                UpdateCardInfo.card_id = ReadInt(bytes, ref pos);
                UpdateCardInfo.guid = ReadInt(bytes, ref pos);
                UpdateCardInfo.card_check = ReadInt(bytes, ref pos);
                UpdateCardInfo.stack_count = ReadInt(bytes, ref pos);

                AddCardInfo.card_id = ReadInt(bytes, ref pos);
                AddCardInfo.guid = ReadInt(bytes, ref pos);
                AddCardInfo.card_check = ReadInt(bytes, ref pos);
                AddCardInfo.stack_count = ReadInt(bytes, ref pos);
            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _BATTLE_DRAGON_EXPEDITION_ANS : NetMsgHeadInterface
        {
            public struct _REWARD_INFO
            {
                public int item_id;
                public int count;
                public int type;
                public int guid;
                public int stackcount;
                public int gold;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.BATTLE_DRAGON_EXPEDITION_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;
           
            public int win_lose;
            public int reward_count;
            public int result_food;
            public int HeroID;
            public int lv;
            public int exp;
            public int Getexp;

            public _REWARD_INFO[] reward_info;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                win_lose = ReadInt(bytes, ref pos);
                reward_count = ReadInt(bytes, ref pos);

                result_food = ReadInt(bytes, ref pos);

                HeroID = ReadInt(bytes, ref pos);
                lv = ReadInt(bytes, ref pos);
                exp = ReadInt(bytes, ref pos);
                Getexp = ReadInt(bytes, ref pos);

                reward_info = new _REWARD_INFO[Define.REWARD_MAX_COUNT];

                for (int i = 0; i < reward_count; ++i)
                {
                    reward_info[i].item_id = ReadInt(bytes, ref pos);
                    reward_info[i].count = ReadInt(bytes, ref pos);
                    reward_info[i].type = ReadInt(bytes, ref pos);
                    reward_info[i].guid = ReadInt(bytes, ref pos);
                    reward_info[i].stackcount = ReadInt(bytes, ref pos);
                    reward_info[i].gold = ReadInt(bytes, ref pos);
                }
            }
        };
        #region TEMP
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _SKIP_DRAGON_EXPEDION_ANS : NetMsgHeadInterface
        {
            public struct _REWARD_INFO
            {
                public int item_id;
                public int count;
                public int type;
                public int guid;
                public int stackcount;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.SKIP_DRAGON_EXPEDION_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int win_lose;
            public int reward_count;
            public int result_food;
            public _REWARD_INFO[] reward_info;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                win_lose = ReadInt(bytes, ref pos);
                reward_count = ReadInt(bytes, ref pos);

                result_food = ReadInt(bytes, ref pos);

                reward_info = new _REWARD_INFO[Define.REWARD_MAX_COUNT];

                for (int i = 0; i < reward_count; ++i)
                {
                    reward_info[i].item_id = ReadInt(bytes, ref pos);
                    reward_info[i].count = ReadInt(bytes, ref pos);
                    reward_info[i].type = ReadInt(bytes, ref pos);
                    reward_info[i].guid = ReadInt(bytes, ref pos);
                    reward_info[i].stackcount = ReadInt(bytes, ref pos);
                }

            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _OPEN_SHOP_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.OPEN_SHOP_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);
            }

        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _BUY_SHOP_PRODUCT_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.BUY_SHOP_PRODUCT_ANS;
            }
            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int CurrentGold;
            public int CurrentFood;

            public int CurrentCrystal;
            public int CurrentTicket;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                CurrentGold = ReadInt(bytes, ref pos);
                CurrentFood = ReadInt(bytes, ref pos);

                CurrentCrystal = ReadInt(bytes, ref pos);
                CurrentTicket = ReadInt(bytes, ref pos);


            }

        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _PVP_GOODSGET_ANS : NetMsgHeadInterface
        {
            public struct _PVP_STAGE_INFO
            {
                public int world_idx;
                public int stage_idx;
                public int stage_object;
                public int cur_stage_lv;
                public int time_yy;
                public int time_mm;
                public int time_dd;
                public int time_h;
                public int time_m;
                public int time_s;
                public int object_state;
                public int stage_object_id;
                public int rebellion_time_yy;
                public int rebellion_time_mm;
                public int rebellion_time_dd;
                public int rebellion_time_h;
                public int rebellion_time_m;
                public int rebellion_time_s;
                public int get_check;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.PVP_GOODSGET_ANS;
            }
            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int gold;
            public int food;
            public int crystal;
            public int stage_count;
            public _PVP_STAGE_INFO[] pvp_stage_info;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                gold = ReadInt(bytes, ref pos);
                food = ReadInt(bytes, ref pos);
                crystal = ReadInt(bytes, ref pos);

                stage_count = ReadInt(bytes, ref pos);
                pvp_stage_info = new _PVP_STAGE_INFO[Define.STAGE_MAX_COUNT];

                for (int i = 0; i < stage_count; i++)
                {
                    pvp_stage_info[i].world_idx = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].stage_idx = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].stage_object = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].cur_stage_lv = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].time_yy = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].time_mm = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].time_dd = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].time_h = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].time_m = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].time_s = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].object_state = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].stage_object_id = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].rebellion_time_yy = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].rebellion_time_mm = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].rebellion_time_dd = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].rebellion_time_h = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].rebellion_time_m = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].rebellion_time_s = ReadInt(bytes, ref pos);
                    pvp_stage_info[i].get_check = ReadInt(bytes, ref pos);
                }
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _PVP_END_ANS : NetMsgHeadInterface
        {
            public struct _PVP_HISTORY_INFO
            {
                public int userID_Nick_Len;
                public byte[] userNick;

                public int my_score;
                public int my_rank_point;
                public int pvp_user_score;
                public int pvp_rank_point;
            }

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.PVP_END_ANS;
            }
            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int count;
            public _PVP_HISTORY_INFO[] PvpHistoryInfo;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                count = ReadInt(bytes, ref pos);
                PvpHistoryInfo = new _PVP_HISTORY_INFO[Define.PVP_HISTORY_MAX_COUNT];

                for (int i = 0; i < count; i++)
                {
                    PvpHistoryInfo[i].userID_Nick_Len = ReadInt(bytes, ref pos);
                    PvpHistoryInfo[i].userNick = ReadStringW(bytes, ref pos, PvpHistoryInfo[i].userID_Nick_Len, Define.MAX_NICKLEN);

                    PvpHistoryInfo[i].my_score = ReadInt(bytes, ref pos);
                    PvpHistoryInfo[i].my_rank_point = ReadInt(bytes, ref pos);

                    PvpHistoryInfo[i].pvp_user_score = ReadInt(bytes, ref pos);
                    PvpHistoryInfo[i].pvp_rank_point = ReadInt(bytes, ref pos);
                }
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _LOGIN_CHECK_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.LOGIN_CHECK_ANS;
            }
            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);
            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _REFRESH_CARD_LIST_ANS : NetMsgHeadInterface
        {
            public struct _CARD_INFO
            {
                public int card_id;//카드 id
                public int guid;
                public int card_check;
                public int stack_count;
            };
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.REFRESH_CARD_LIST_ANS;
            }
            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int CardCount;
            public _CARD_INFO[] card_info;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                CardCount = ReadInt(bytes, ref pos);
                card_info = new _CARD_INFO[Define.CARD_MAX_COUNT];

                for (int i = 0; i < CardCount; i++)
                {
                    card_info[i].card_id = ReadInt(bytes, ref pos);
                    card_info[i].guid = ReadInt(bytes, ref pos);
                    card_info[i].card_check = ReadInt(bytes, ref pos);
                    card_info[i].stack_count = ReadInt(bytes, ref pos);
                }
            }
        };

        public struct _MISSION_INFO
        {
            public int missionId;
            public int missionCount;
            public int clearFlag;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _GET_MISSION_LIST_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GET_MISSION_LIST_ANS;
            }
            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int MissionCount;
            public _MISSION_INFO[] mission_infos;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                MissionCount = ReadInt(bytes, ref pos);
                mission_infos = new _MISSION_INFO[Define.MISSION_MAX_COUNT];

                for (int i = 0; i < MissionCount; i++)
                {
                    mission_infos[i].missionId = ReadInt(bytes, ref pos);
                    mission_infos[i].missionCount = ReadInt(bytes, ref pos);
                    mission_infos[i].clearFlag = ReadInt(bytes, ref pos);
                }
            }
        };

        public struct _QUEST_CLEAR_ANS : NetMsgHeadInterface
        {

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.QUEST_CLEAR_ANS;
            }
            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int Gold;
            public int Food;
            public int Cyrstal;
            public int CurrentMissionId;
            public _MISSION_INFO UpdateMissionData;
            public _EVENT_SHOP_INFO EventShopInfo;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                Gold = ReadInt(bytes, ref pos);
                Food = ReadInt(bytes, ref pos);
                Cyrstal = ReadInt(bytes, ref pos);
                CurrentMissionId = ReadInt(bytes, ref pos);
                UpdateMissionData.missionId = ReadInt(bytes, ref pos);
                UpdateMissionData.missionCount = ReadInt(bytes, ref pos);
                UpdateMissionData.clearFlag = ReadInt(bytes, ref pos);

                EventShopInfo.KeyLength = ReaduShort(bytes, ref pos);

                byte[] byteKey = ReadStringW(bytes, ref pos, EventShopInfo.KeyLength, Define.SHOP_KEY_MAX_LENGTH);
                EventShopInfo.ShopKey = System.Text.Encoding.Unicode.GetString(byteKey);

                EventShopInfo.UnixTime = ReadInt(bytes, ref pos);

            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _TUTORIAL_STEP_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.TUTORIAL_STEP_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);
            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _TUTORIAL_COMPLETE_ANS : NetMsgHeadInterface
        {
            public struct CardData
            {
                public int CardID;
                public int CardStack;
                public int CardCheck;
                public int Guid;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.TUTORIAL_COMPLETE_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int ClearFlag;
            public int gold;
            public int food;
            public int crystal;
            public int premium_ticket;

            public int RewardCardCount;
            public CardData[] RewardCardList;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                ClearFlag = ReadInt(bytes, ref pos);
                gold = ReadInt(bytes, ref pos);
                food = ReadInt(bytes, ref pos);
                crystal = ReadInt(bytes, ref pos);
                premium_ticket = ReadInt(bytes, ref pos);

                RewardCardCount = ReadInt(bytes, ref pos);
                RewardCardList = new CardData[10];

                for (int i = 0; i < RewardCardCount; i++)
                {
                    RewardCardList[i].CardID = ReadInt(bytes, ref pos);
                    RewardCardList[i].CardStack = ReadInt(bytes, ref pos);
                    RewardCardList[i].CardCheck = ReadInt(bytes, ref pos);
                    RewardCardList[i].Guid = ReadInt(bytes, ref pos);
                }
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _DRAGON_EXPEDITION_ADD_COUNT_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.DRAGON_EXPEDITION_ADD_COUNT_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int count;
            public int day_count;
            public int crystal;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                count = ReadInt(bytes, ref pos);
                day_count = ReadInt(bytes, ref pos);
                crystal = ReadInt(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _GAME_ERROR_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GAME_ERROR_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int ErrorCode;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                ErrorCode = ReadInt(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _PVP_COUNT_ADD_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.PVP_COUNT_ADD_ANS;
            }
            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int count;
            public int crystal;
            public int day_count;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                count = ReadInt(bytes, ref pos);
                crystal = ReadInt(bytes, ref pos);
                day_count = ReadInt(bytes, ref pos);
            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _SELL_CARD_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.SELL_CARD_ANS;
            }
            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int SellCardGuid;

            public int gold;
            public int food;
            public int crystal;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                SellCardGuid = ReadInt(bytes, ref pos);
                gold = ReadInt(bytes, ref pos);
                food = ReadInt(bytes, ref pos);
                crystal = ReadInt(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _TUTORIAL_GACHA_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.TUTORIAL_GACHA_ANS;
            }
            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int guid;
            public int cardid;


            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                guid = ReadInt(bytes, ref pos);
                cardid = ReadInt(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _TURORIAL_GOODSGET_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.TURORIAL_GOODSGET_ANS;
            }
            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int ResultFood;


            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                ResultFood = ReadInt(bytes, ref pos);

            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _TUTORIAL_BATTLE_ANS : NetMsgHeadInterface
        {
            public struct _REWARD_INFO
            {
                public int item_id;
                public int count;
                public int type;
                public int guid;
                public int stack_count;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.TUTORIAL_BATTLE_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int win_lose;//승리 패배

            public int reward_count;

            public int Resultfood;

            public _REWARD_INFO[] reward_info;

            public _BATTLE_REPORT_ANS BattleReportData;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                win_lose = ReadInt(bytes, ref pos);
                reward_count = ReadInt(bytes, ref pos);


                Resultfood = ReadInt(bytes, ref pos);

                reward_info = new _REWARD_INFO[Define.REWARD_MAX_COUNT];

                for (int i = 0; i < Define.REWARD_MAX_COUNT; ++i)
                {
                    reward_info[i].item_id = ReadInt(bytes, ref pos);
                    reward_info[i].count = ReadInt(bytes, ref pos);
                    reward_info[i].type = ReadInt(bytes, ref pos);
                    reward_info[i].guid = ReadInt(bytes, ref pos);
                    reward_info[i].stack_count = ReadInt(bytes, ref pos);
                }

                BattleReportData.ReadRecvBuffer(bytes, ref pos);
            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _PVP_MATCHING_LIST_ANS : NetMsgHeadInterface
        {
            public struct _PVP_USERINFO
            {
                public int userID_Len;
                //wchar_t userID[MAX_NICKLEN];
                public byte[] userID;

                public int userID_Nick_Len;
                public byte[] userNick;

                public int combat_point;
                public int pvp_score;

                public int rank;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.PVP_MATCHING_LIST_ANS;
            }
            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public _PVP_USERINFO[] MathcingUserList;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                MathcingUserList = new _PVP_USERINFO[3];

                for (int i = 0; i < 3; ++i)
                {
                    MathcingUserList[i].userID_Len = ReadInt(bytes, ref pos);
                    MathcingUserList[i].userID = ReadStringW(bytes, ref pos, MathcingUserList[i].userID_Len, Define.MAX_NICKLEN);

                    MathcingUserList[i].userID_Nick_Len = ReadInt(bytes, ref pos);
                    MathcingUserList[i].userNick = ReadStringW(bytes, ref pos, MathcingUserList[i].userID_Nick_Len, Define.MAX_NICKLEN);

                    MathcingUserList[i].combat_point = ReadInt(bytes, ref pos);
                    MathcingUserList[i].pvp_score = ReadInt(bytes, ref pos);
                    MathcingUserList[i].rank = ReadInt(bytes, ref pos);
                }
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _STAGE_REBELLION_CHECK_ANS : NetMsgHeadInterface
        {
            public struct _STAGE_OBJECT_ID
            {
                public int object_id;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.STAGE_REBELLION_CHECK_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int stage_object_count;
            public _STAGE_OBJECT_ID[] Stage_obj_ID;

            public int stage_goods_count;
            public int[] stage_goods_object;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                stage_object_count = ReadInt(bytes, ref pos);
                Stage_obj_ID = new _STAGE_OBJECT_ID[stage_object_count];

                for (int i = 0; i < stage_object_count; i++)
                {
                    Stage_obj_ID[i].object_id = ReadInt(bytes, ref pos);
                }

                stage_goods_count = ReadInt(bytes, ref pos);
                stage_goods_object = new int[18];

                for (int i = 0; i < 18; i++)
                {
                    stage_goods_object[i] = ReadInt(bytes, ref pos);
                }
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _GAMBLE_ANS : NetMsgHeadInterface
        {

            public struct GambleRewardInfo
            {
                public int item_type;
                public int card_id;
                public int item_amount;
                public int guid;
                public int card_stack;
            }
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GAMBLE_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int crystal;
            public int reward_count;
            public int ticket;
            public GambleRewardInfo[] GambleReward;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                crystal = ReadInt(bytes, ref pos);
                reward_count = ReadInt(bytes, ref pos);
                ticket = ReadInt(bytes, ref pos);
                GambleReward = new GambleRewardInfo[Define.GAMBLE_MAX_COUNT];

                for (int i = 0; i < reward_count; i++)
                {
                    GambleReward[i].item_type = ReadInt(bytes, ref pos);
                    GambleReward[i].card_id = ReadInt(bytes, ref pos);
                    GambleReward[i].item_amount = ReadInt(bytes, ref pos);
                    GambleReward[i].guid = ReadInt(bytes, ref pos);
                    GambleReward[i].card_stack = ReadInt(bytes, ref pos);
                }
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _ALL_USER_GOODS_GET_ANS : NetMsgHeadInterface
        {
            public struct _STAGE_INFO
            {
                public int Cur_World_idx; //현재 월드 인덱스 값(휴먼,엘프)
                public int Cur_Stage_idx; //현재 종족의 인덱스 값(1-1,1-2)
                public int Cur_Stage_Object; //현재 어떤 오브젝트인지
                public int Cur_Stage_Lv; // 현재 스테이지 레벨(금광, 크리스탈 레벨)

                public int Respawn_Time_yy; //현재 시간 
                public int Respawn_Time_mm;
                public int Respawn_Time_dd;

                public int Respawn_Time_h; //현재 시간 
                public int Respawn_Time_m;
                public int Respawn_Time_s;

                public int Object_state; //현재 오브젝트 상태(점령여부, 반란군 여부 )
                public int Stage_Object_id; //현재 스테이지의 오브젝트 id

                public int Rebellion_Time_yy; //반란군 시간
                public int Rebellion_Time_mm;
                public int Rebellion_Time_dd;

                public int Rebellion_Time_h; //반란군 시간
                public int Rebellion_Time_m;
                public int Rebellion_Time_s;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.ALL_USER_GOODS_GET_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int gold;
            public int food;

            public int StageCount;
            public _STAGE_INFO[] stage_info;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                gold = ReadInt(bytes, ref pos);
                food = ReadInt(bytes, ref pos);

                StageCount = ReadInt(bytes, ref pos);
                stage_info = new _STAGE_INFO[Define.STAGE_MAX_COUNT];

                for (int i = 0; i < StageCount; i++)
                {
                    stage_info[i].Cur_World_idx = ReadInt(bytes, ref pos);
                    stage_info[i].Cur_Stage_idx = ReadInt(bytes, ref pos);
                    stage_info[i].Cur_Stage_Object = ReadInt(bytes, ref pos);
                    stage_info[i].Cur_Stage_Lv = ReadInt(bytes, ref pos);

                    stage_info[i].Respawn_Time_yy = ReadInt(bytes, ref pos);
                    stage_info[i].Respawn_Time_mm = ReadInt(bytes, ref pos);
                    stage_info[i].Respawn_Time_dd = ReadInt(bytes, ref pos);

                    stage_info[i].Respawn_Time_h = ReadInt(bytes, ref pos);
                    stage_info[i].Respawn_Time_m = ReadInt(bytes, ref pos);
                    stage_info[i].Respawn_Time_s = ReadInt(bytes, ref pos);

                    stage_info[i].Object_state = ReadInt(bytes, ref pos);
                    stage_info[i].Stage_Object_id = ReadInt(bytes, ref pos);

                    stage_info[i].Rebellion_Time_yy = ReadInt(bytes, ref pos);
                    stage_info[i].Rebellion_Time_mm = ReadInt(bytes, ref pos);
                    stage_info[i].Rebellion_Time_dd = ReadInt(bytes, ref pos);

                    stage_info[i].Rebellion_Time_h = ReadInt(bytes, ref pos);
                    stage_info[i].Rebellion_Time_m = ReadInt(bytes, ref pos);
                    stage_info[i].Rebellion_Time_s = ReadInt(bytes, ref pos);
                }
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _ATTENDANCE_CHECK_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.ATTENDANCE_CHECK_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int step;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                step = ReadInt(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _ATTENDANCE_GET_ANS : NetMsgHeadInterface
        {
            public struct _ATTENDANCE_CHECK_INFO
            {
                public int step;
                public int step_check;
            }

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.ATTENDANCE_GET_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int package_type;
            public int crystal;
            public int cardid;
            public int guid;
            public int stack_count;
            public int ticket;
            public int luckey_ticket;
            public int step_check;

            public int check_count;
            public _ATTENDANCE_CHECK_INFO[] AttendanceCheckInfo;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                package_type = ReadInt(bytes, ref pos);
                crystal = ReadInt(bytes, ref pos);
                cardid = ReadInt(bytes, ref pos);
                guid = ReadInt(bytes, ref pos);
                stack_count = ReadInt(bytes, ref pos);
                ticket = ReadInt(bytes, ref pos);
                luckey_ticket = ReadInt(bytes, ref pos);
                step_check = ReadInt(bytes, ref pos);

                check_count = ReadInt(bytes, ref pos);
                AttendanceCheckInfo = new _ATTENDANCE_CHECK_INFO[Define.ATTENDANCE_MAX_COUNT];

                for (int i = 0; i < check_count; i++)
                {
                    AttendanceCheckInfo[i].step = ReadInt(bytes, ref pos);
                    AttendanceCheckInfo[i].step_check = ReadInt(bytes, ref pos);
                }
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _CHAT_MSG_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.CHAT_MSG_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int nick_len;
            public byte[] NickName;
            public int msg_len;
            public byte[] Msg;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                nick_len = ReadInt(bytes, ref pos);
                NickName = new byte[nick_len];
                NickName = ReadStringW(bytes, ref pos, nick_len, Define.MAX_NICKLEN);

                msg_len = ReadInt(bytes, ref pos);
                Msg = new byte[msg_len];
                Msg = ReadStringW(bytes, ref pos, msg_len, Define.MAX_MSGLEN);

            }
        };
        #endregion
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _TUTORIAL_DRAGON_EXPEDION_ANS : NetMsgHeadInterface
        {
            public struct _REWARD_INFO
            {
                public int item_id;
                public int count;
                public int type;
                public int guid;
                public int stackcount;
            };
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.TUTORIAL_DRAGON_EXPEDION_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int win_lose;
            public int reward_count;
            public int result_food;

            public _REWARD_INFO reward_info;

            public _BATTLE_REPORT_ANS BattleReportData;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                win_lose = ReadInt(bytes, ref pos);
                reward_count = ReadInt(bytes, ref pos);

                result_food = ReadInt(bytes, ref pos);

                reward_info = new _REWARD_INFO();
                reward_info.item_id = ReadInt(bytes, ref pos);
                reward_info.count = ReadInt(bytes, ref pos);
                reward_info.type = ReadInt(bytes, ref pos);
                reward_info.guid = ReadInt(bytes, ref pos);
                reward_info.stackcount = ReadInt(bytes, ref pos);

                BattleReportData.ReadRecvBuffer(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _TUTORIAL_DRAGON_COMPLETE_ANS : NetMsgHeadInterface
        {
            public struct _REWARD_INFO
            {
                public int item_id;
                public int count;
                public int type;
                public int guid;
                public int stack_count;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.TUTORIAL_DRAGON_COMPLETE_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public _REWARD_INFO reward_info;

            public int tutorial_bitmask;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                reward_info = new _REWARD_INFO();
                reward_info.item_id = ReadInt(bytes, ref pos);
                reward_info.count = ReadInt(bytes, ref pos);
                reward_info.type = ReadInt(bytes, ref pos);
                reward_info.guid = ReadInt(bytes, ref pos);
                reward_info.stack_count = ReadInt(bytes, ref pos);

                tutorial_bitmask = ReadInt(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _ALL_USER_GOODS_GET_CHECK_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.ALL_USER_GOODS_GET_CHECK_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int flag;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                flag = ReadInt(bytes, ref pos);
            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _BATTLE_ACTION_ANS : NetMsgHeadInterface
        {

            public struct _POSITION_DATA
			{
                public int PosX;
                public int PosY;
			};

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.BATTLE_ACTION_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int ActionUnitID;
            public int NextTurnUnitID;
            public int CharacterActionStateMent;

            public _POSITION_DATA StartPos;
            public _POSITION_DATA EndPos;

            public int ActivateSkillID;

            public int TargetTileX;
            public int TargetTileY;

            public BATTLE_CHARACTER_DATA ActiveCharacterData;

            public int TargetCount;
            public BATTLE_CHARACTER_DATA[] TargetData;

            public bool IsTurnEndData;

            public bool IsBattleEnd;

            public bool WinLose;

            public BATTLE_CHARACTER_DATA[] ChangeAllyData;
            public BATTLE_CHARACTER_DATA[] ChangeEnemyData;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                ActionUnitID = ReadInt(bytes, ref pos);
                NextTurnUnitID = ReadInt(bytes, ref pos);
                CharacterActionStateMent = ReadInt(bytes, ref pos);

                StartPos.PosX = ReadInt(bytes, ref pos);
                StartPos.PosY = ReadInt(bytes, ref pos);

                EndPos.PosX = ReadInt(bytes, ref pos);
                EndPos.PosY = ReadInt(bytes, ref pos);

                ActivateSkillID = ReadInt(bytes, ref pos);

                TargetTileX = ReadInt(bytes, ref pos);
                TargetTileY = ReadInt(bytes, ref pos);

                ActiveCharacterData.ReadRecvBuffer(bytes, ref pos);

                TargetCount = ReadInt(bytes, ref pos);

                TargetData = new BATTLE_CHARACTER_DATA[Define.BATTLE_ALL_MAX * 2];
                for(int i=0;i<TargetData.Length;i++)
				{
                    TargetData[i].ReadRecvBuffer(bytes, ref pos);
                }

                IsTurnEndData = Readboolean(bytes, ref pos);

                IsBattleEnd = Readboolean(bytes, ref pos);

                WinLose = Readboolean(bytes, ref pos);

                ChangeAllyData = new BATTLE_CHARACTER_DATA[Define.BATTLE_ALL_MAX];
                for (int i = 0; i < ChangeAllyData.Length; i++)
                {
                    ChangeAllyData[i].ReadRecvBuffer(bytes, ref pos);
                }
                ChangeEnemyData = new BATTLE_CHARACTER_DATA[Define.BATTLE_ALL_MAX];
                for (int i = 0; i < ChangeEnemyData.Length; i++)
                {
                    ChangeEnemyData[i].ReadRecvBuffer(bytes, ref pos);
                }
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _BATTLE_DELAY_TURN_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.BATTLE_DELAY_TURN_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int CurrentUnitKey;
            public int NextActionKeyUnit;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                CurrentUnitKey = ReadInt(bytes, ref pos);
                NextActionKeyUnit = ReadInt(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _BATTLE_UNIT_ATTACK_ANS : NetMsgHeadInterface
        {

            public struct _POSITION_DATA
            {
                public int PosX;
                public int PosY;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.BATTLE_UNIT_ATTACK_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int ActionUnitID;
            public int ActivateSkillID;

            public int TargetTileX;
            public int TargetTileY;

            public BATTLE_CHARACTER_DATA ActiveCharacterData;

            public int TargetCount;
            public BATTLE_CHARACTER_DATA[] TargetData;

            public bool IsTurnEndData;

            public bool IsBattleEnd;

            public bool WinLose;

            public int NextTurnUnitID;

            public BATTLE_CHARACTER_DATA[] ChangeAllyData;
            public BATTLE_CHARACTER_DATA[] ChangeEnemyData;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                ActionUnitID = ReadInt(bytes, ref pos);
                ActivateSkillID = ReadInt(bytes, ref pos);

                TargetTileX = ReadInt(bytes, ref pos);
                TargetTileY = ReadInt(bytes, ref pos);

                ActiveCharacterData.ReadRecvBuffer(bytes, ref pos);

                TargetCount = ReadInt(bytes, ref pos);

                TargetData = new BATTLE_CHARACTER_DATA[Define.BATTLE_ALL_MAX * 2];
                for (int i = 0; i < TargetData.Length; i++)
                {
                    TargetData[i].ReadRecvBuffer(bytes, ref pos);
                }

                IsTurnEndData = Readboolean(bytes, ref pos);

                IsBattleEnd = Readboolean(bytes, ref pos);

                WinLose = Readboolean(bytes, ref pos);

                NextTurnUnitID = ReadInt(bytes, ref pos);

                ChangeAllyData = new BATTLE_CHARACTER_DATA[Define.BATTLE_ALL_MAX];
                for (int i = 0; i < ChangeAllyData.Length; i++)
                {
                    ChangeAllyData[i].ReadRecvBuffer(bytes, ref pos);
                }
                ChangeEnemyData = new BATTLE_CHARACTER_DATA[Define.BATTLE_ALL_MAX];
                for (int i = 0; i < ChangeEnemyData.Length; i++)
                {
                    ChangeEnemyData[i].ReadRecvBuffer(bytes, ref pos);
                }
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _BATTLE_UNIT_MOVE_ANS : NetMsgHeadInterface
        {

            public struct _POSITION_DATA
            {
                public int PosX;
                public int PosY;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.BATTLE_UNIT_MOVE_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int ActionUnitID;

            public _POSITION_DATA StartPos;
            public _POSITION_DATA EndPos;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                ActionUnitID = ReadInt(bytes, ref pos);

                StartPos.PosX = ReadInt(bytes, ref pos);
                StartPos.PosY = ReadInt(bytes, ref pos);

                EndPos.PosX = ReadInt(bytes, ref pos);
                EndPos.PosY = ReadInt(bytes, ref pos);

                
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _BATTLE_END_TURN_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.BATTLE_END_TURN_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int EndBattleUnitID;

            public int NextActionKeyUnit;

            public bool IsRefreshTurnList;

            public bool WinLose;

            public BATTLE_CHARACTER_DATA[] ChangeAllyData;
            public BATTLE_CHARACTER_DATA[] ChangeEnemyData;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                EndBattleUnitID = ReadInt(bytes, ref pos);
                NextActionKeyUnit = ReadInt(bytes, ref pos);

                IsRefreshTurnList = Readboolean(bytes, ref pos);

                WinLose = Readboolean(bytes, ref pos);

                ChangeAllyData = new BATTLE_CHARACTER_DATA[Define.BATTLE_ALL_MAX];
                for (int i = 0; i < ChangeAllyData.Length; i++)
                {
                    ChangeAllyData[i].ReadRecvBuffer(bytes, ref pos);
                }
                ChangeEnemyData = new BATTLE_CHARACTER_DATA[Define.BATTLE_ALL_MAX];
                for (int i = 0; i < ChangeEnemyData.Length; i++)
                {
                    ChangeEnemyData[i].ReadRecvBuffer(bytes, ref pos);
                }
            }
        };

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _MAIL_LIST_ANS : NetMsgHeadInterface
        {
            public struct _MAILINFO
            {
                public int item_id;
                public int guid;
                public int type;
                public int msg_id;
                public int item_num;
            }

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.MAIL_LIST_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int count;
            public _MAILINFO[] mailinfo;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                count = ReadInt(bytes, ref pos);
                //MAIL_MAX_COUNT ?
                mailinfo = new _MAILINFO[count];
                for (int i = 0; i < count; ++i)
                {
                    mailinfo[i].item_id = ReadInt(bytes, ref pos);
                    mailinfo[i].guid = ReadInt(bytes, ref pos);
                    mailinfo[i].type = ReadInt(bytes, ref pos);
                    mailinfo[i].msg_id = ReadInt(bytes, ref pos);
                    mailinfo[i].item_num = ReadInt(bytes, ref pos);
                }
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _MAIL_ITEM_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.MAIL_ITEM_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int item_id;
            public int item_guid;
            public int mail_guid;
            public int item_type;
            public int item_num;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                item_id = ReadInt(bytes, ref pos);
                item_guid = ReadInt(bytes, ref pos);
                mail_guid = ReadInt(bytes, ref pos);
                item_type = ReadInt(bytes, ref pos);
                item_num = ReadInt(bytes, ref pos);

            }
        };

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _BATTLE_GIVE_UP_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.BATTLE_GIVE_UP_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _BATTLE_UNIT_DEFENSE_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.BATTLE_UNIT_DEFENSE_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;


            public int ActionCharacterKey;

            public int NextTurnUnitID;

            public BATTLE_CHARACTER_DATA ActiveUnitData;

            public BATTLE_CHARACTER_DATA[] ChangeAllyData;
            public BATTLE_CHARACTER_DATA[] ChangeEnemyData;

            public bool IsTurnEndData;

            public bool WinLose;

            public bool BattleEndFlag;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                ActionCharacterKey = ReadInt(bytes, ref pos);

                NextTurnUnitID = ReadInt(bytes, ref pos);

                ActiveUnitData.ReadRecvBuffer(bytes, ref pos);

                ChangeAllyData = new BATTLE_CHARACTER_DATA[Define.BATTLE_ALL_MAX];
                for (int i = 0; i < ChangeAllyData.Length; i++)
                {
                    ChangeAllyData[i].ReadRecvBuffer(bytes, ref pos);
                }
                ChangeEnemyData = new BATTLE_CHARACTER_DATA[Define.BATTLE_ALL_MAX];
                for (int i = 0; i < ChangeEnemyData.Length; i++)
                {
                    ChangeEnemyData[i].ReadRecvBuffer(bytes, ref pos);
                }

                IsTurnEndData = Readboolean(bytes, ref pos);

                WinLose = Readboolean(bytes, ref pos);

                BattleEndFlag = Readboolean(bytes, ref pos);



            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _GUILD_CREATE_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GUILD_CREATE_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;
            
            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _GUILD_JOIN_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GUILD_JOIN_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);
            }
        };

        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _GUILD_JOINCANCEL_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GUILD_JOINCANCEL_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _GUILD_GROUPLIST_ANS : NetMsgHeadInterface
        {
            public struct _GUILDGROUP_INFO
            {
                public int guildname_len;
                public string guildName;
                public int icn_idx;
                public int intro_len;
                public string intro;
                public int guild_score;
            }

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GUILD_GROUPLIST_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int guild_count;
            public _GUILDGROUP_INFO[] info;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                guild_count = ReadInt(bytes, ref pos);
                info = new _GUILDGROUP_INFO[guild_count];
                for(int i = 0; i < guild_count; ++i)
                {
                    info[i].guildname_len = ReadInt(bytes, ref pos);
                    info[i].guildName = ReadStringWEx(bytes, ref pos, info[i].guildname_len, Define.MAX_NICKLEN);
                    info[i].icn_idx = ReadInt(bytes, ref pos);
                    info[i].intro_len = ReadInt(bytes, ref pos);
                    info[i].intro = ReadStringWEx(bytes, ref pos, info[i].intro_len, Define.MAX_LONGMSGLEN);

                    ReadInt(bytes, ref pos); //win
                    ReadInt(bytes, ref pos); //defeat
                    ReadInt(bytes, ref pos); //draw
                    info[i].guild_score = ReadInt(bytes, ref pos); //score
                }
            }
        };

        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _GUILD_BATTLEINFO_ANS : NetMsgHeadInterface
        {
            public struct _GUILDGROUP_INFO
            {
                public int guildname_len;
                public string guildName;
                public int icn_idx;
                public int intro_len;
                public string intro;

                public int win;
                public int defeat;
                public int draw;
                public int score;
            }

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GUILD_BATTLEINFO_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int guild_count;
            public _GUILDGROUP_INFO[] info;

            public int enemy_guildname_len;
            public string enemy_guildname;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                enemy_guildname_len = ReadInt(bytes, ref pos);
                enemy_guildname = ReadStringWEx(bytes, ref pos, enemy_guildname_len, Define.MAX_NICKLEN);

                guild_count = ReadInt(bytes, ref pos);
                info = new _GUILDGROUP_INFO[guild_count];
                for (int i = 0; i < guild_count; ++i)
                {
                    info[i].guildname_len = ReadInt(bytes, ref pos);
                    info[i].guildName = ReadStringWEx(bytes, ref pos, info[i].guildname_len, Define.MAX_NICKLEN);
                    info[i].icn_idx = ReadInt(bytes, ref pos);
                    info[i].intro_len = ReadInt(bytes, ref pos);
                    info[i].intro = ReadStringWEx(bytes, ref pos, info[i].intro_len, Define.MAX_LONGMSGLEN);

                    info[i].win = ReadInt(bytes, ref pos);
                    info[i].defeat = ReadInt(bytes, ref pos);
                    info[i].draw = ReadInt(bytes, ref pos);
                    info[i].score = ReadInt(bytes, ref pos);
                }

                
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _GUILD_BATTLEREADY_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GUILD_BATTLEREADY_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int state;
            
            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                state = ReadInt(bytes, ref pos);
             }
        };

        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _GUILD_BATTLEMATCHING_ANS : NetMsgHeadInterface
        {
            public struct _GUILD_BATTLEUSER_INFO
            {
                public int nick_len;
                public string Nick;
                public int battle_start;
                public int battle_defeat;
            }

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GUILD_BATTLEMATCHING_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int my_guild_user_count;
            public _GUILD_BATTLEUSER_INFO[] my_user;

            public int other_guild_user_count;
            public _GUILD_BATTLEUSER_INFO[] other_user;

            public int my_victory;
            public int enemy_victory;

            

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                my_guild_user_count = ReadInt(bytes, ref pos);
                my_user = new _GUILD_BATTLEUSER_INFO[Define.MAX_GUILDBATTLE_USER];
                for (int i = 0; i < Define.MAX_GUILDBATTLE_USER; ++i)
                {
                    my_user[i].nick_len = ReadInt(bytes, ref pos);
                    if (my_user[i].nick_len != 0)
                        my_user[i].Nick = ReadStringWEx(bytes, ref pos, my_user[i].nick_len, Define.MAX_NICKLEN);
                    else
                    {
                        my_user[i].Nick = string.Empty;
                        ReadStringWEx(bytes, ref pos, Define.MAX_NICKLEN, Define.MAX_NICKLEN);
                    }

                    my_user[i].battle_start = ReadInt(bytes, ref pos);
                    my_user[i].battle_defeat = ReadInt(bytes, ref pos);
                }

                other_guild_user_count = ReadInt(bytes, ref pos);
                other_user = new _GUILD_BATTLEUSER_INFO[Define.MAX_GUILDBATTLE_USER];
                for (int i = 0; i < Define.MAX_GUILDBATTLE_USER; ++i)
                {
                    other_user[i].nick_len = ReadInt(bytes, ref pos);
                    if (other_user[i].nick_len != 0)
                        other_user[i].Nick = ReadStringWEx(bytes, ref pos, other_user[i].nick_len, Define.MAX_NICKLEN);
                    else
                    {
                        other_user[i].Nick = string.Empty;
                        ReadStringWEx(bytes, ref pos, Define.MAX_NICKLEN, Define.MAX_NICKLEN);
                    }

                    other_user[i].battle_start = ReadInt(bytes, ref pos);
                    other_user[i].battle_defeat = ReadInt(bytes, ref pos);
                }

                my_victory = ReadInt(bytes, ref pos);
                enemy_victory = ReadInt(bytes, ref pos);

                
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _GUILD_ENEMYDECK_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GUILD_ENEMYDECK_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int enemy_IDlen;
            public string enemy_userID;

            public int deck_count;
            public int[] deck;
            public int battle_power;
            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                enemy_IDlen = ReadInt(bytes, ref pos);
                enemy_userID = ReadStringWEx(bytes, ref pos, enemy_IDlen, Define.MAX_NICKLEN);

                deck_count = ReadInt(bytes, ref pos);
                deck = new int[8];
                for(int i = 0; i < 8; ++i)
                {
                    deck[i] = ReadInt(bytes, ref pos);
                }
                battle_power = ReadInt(bytes, ref pos);
                
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _GUILD_BATTLE_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GUILD_BATTLE_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int win_lose; 
            
            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                win_lose = ReadInt(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _GUILD_MEMBERLIST_ANS : NetMsgHeadInterface
        {
            public struct _GUILDMEMBER_INFO
            {
                public int nick_len;
                //public byte[] userID;
                public string userNick;
                public int isMaster;
                public int battle_point;
            }

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GUILD_MEMBERLIST_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int guildname_len;
            public string guildname;
            
            public int guild_icn_idx;

            public int intro_len;
            public string intro;

            public int notice_len;
            public string notice;

            public int guild_score;

            public int member_count;
            public _GUILDMEMBER_INFO[] info;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                guildname_len = ReadInt(bytes, ref pos);
                guildname = ReadStringWEx(bytes, ref pos, guildname_len, Define.MAX_NICKLEN);

                guild_icn_idx = ReadInt(bytes, ref pos);

                intro_len = ReadInt(bytes, ref pos);
                intro = ReadStringWEx(bytes, ref pos, intro_len, Define.MAX_LONGMSGLEN);

                notice_len = ReadInt(bytes, ref pos);
                notice = ReadStringWEx(bytes, ref pos, notice_len, Define.MAX_LONGMSGLEN);

                guild_score = ReadInt(bytes, ref pos);

                member_count = ReadInt(bytes, ref pos);
                info = new _GUILDMEMBER_INFO[member_count];
                for(int i = 0; i < member_count; ++i)
                {
                    info[i].nick_len = ReadInt(bytes, ref pos);
                    info[i].userNick = ReadStringWEx(bytes, ref pos, info[i].nick_len, Define.MAX_NICKLEN);
                    info[i].isMaster = ReadInt(bytes, ref pos);
                    info[i].battle_point = ReadInt(bytes, ref pos);
                }

            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _GUILD_JOINLIST_ANS : NetMsgHeadInterface
        {
            public struct _GUILDJOINWAIT_INFO
            {
                public int nick_len;
                //public byte[] userID;
                public string userNick;
               
            }

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GUILD_MEMBERJOINLIST_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            

            public int joinwait_count;
            public _GUILDJOINWAIT_INFO[] info;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);



                joinwait_count = ReadInt(bytes, ref pos);
                info = new _GUILDJOINWAIT_INFO[joinwait_count];
                for (int i = 0; i < joinwait_count; ++i)
                {
                    info[i].nick_len = ReadInt(bytes, ref pos);
                    info[i].userNick = ReadStringWEx(bytes, ref pos, info[i].nick_len, Define.MAX_NICKLEN);
                   
                }

            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _GUILD_JOINPERMIT_ANS : NetMsgHeadInterface
        {
            public struct _GUILDJOINEDUSER_INFO
            {
                public int len;
                public string Nick;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GUILD_MEMBERJOINPERMIT_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public _GUILDJOINEDUSER_INFO joined_userNick;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                joined_userNick.len = ReadInt(bytes, ref pos);
                joined_userNick.Nick = ReadStringWEx(bytes, ref pos, joined_userNick.len, Define.MAX_NICKLEN);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _GUILD_JOINREJECT_ANS : NetMsgHeadInterface
        {
            public struct _GUILDJOINEDUSER_INFO
            {
                public int len;
                public string Nick;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GUILD_MEMBERJOINREJECT_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public _GUILDJOINEDUSER_INFO joined_userNick;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                joined_userNick.len = ReadInt(bytes, ref pos);
                joined_userNick.Nick = ReadStringWEx(bytes, ref pos, joined_userNick.len, Define.MAX_NICKLEN);
            }
        };

        
         [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _GUILD_MEMBEREXILE_ANS : NetMsgHeadInterface
        {
            public struct _GUILDJOINEDUSER_INFO
            {
                public int len;
                public string Nick;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GUILD_MEMBEREXILE_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public _GUILDJOINEDUSER_INFO exile_userNick;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                exile_userNick.len = ReadInt(bytes, ref pos);
                exile_userNick.Nick = ReadStringWEx(bytes, ref pos, exile_userNick.len, Define.MAX_NICKLEN);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _GUILD_LEAVE_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GUILD_LEAVE_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);
            }
        };
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _GUILD_INTRODUCE_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GUILD_INTRODUCE_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _GUILD_NOTICE_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.GUILD_NOTICE_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);
            }
        };

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _HERO_ACTION_ANS : NetMsgHeadInterface
        {

            public struct HERO_DATA
            {
                public int Attack;
                public int Stength;
                public int Defence;
                public int Speed;
                public int MaxHealth;
                public int CurrentHealth;
                public int MaxMana;
                public int CurrentMana;
            };

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.HERO_ACTION_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;


            public HERO_DATA HeroData;

            public bool IsBattleEnd;

            public bool WinLose;

            public int ActiveSkillID;

            public int TargetTileX;
            public int TargetTileY;

            public int TargetCount;
            public BATTLE_CHARACTER_DATA[] TargetData;

            

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);


                HeroData.Attack = ReadInt(bytes, ref pos);
                HeroData.Stength = ReadInt(bytes, ref pos);
                HeroData.Defence = ReadInt(bytes, ref pos);
                HeroData.Speed = ReadInt(bytes, ref pos);
                HeroData.MaxHealth = ReadInt(bytes, ref pos);
                HeroData.CurrentHealth = ReadInt(bytes, ref pos);
                HeroData.MaxMana = ReadInt(bytes, ref pos);
                HeroData.CurrentMana = ReadInt(bytes, ref pos);

                IsBattleEnd = Readboolean(bytes, ref pos);

                WinLose = Readboolean(bytes, ref pos);

                ActiveSkillID = ReadInt(bytes, ref pos);

                TargetTileX = ReadInt(bytes, ref pos);
                TargetTileY = ReadInt(bytes, ref pos);

                TargetCount = ReadInt(bytes, ref pos);

                TargetData = new BATTLE_CHARACTER_DATA[Define.BATTLE_ALL_MAX * 2];
                for (int i = 0; i < TargetCount; i++)
                {
                    TargetData[i].ReadRecvBuffer(bytes, ref pos);
                }

                
                
            }
        };


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _BATTLE_ERROR_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.BATTLE_ERROR_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int ErrorCode;
            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);
                
                ErrorCode = ReadInt(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _AUTO_BATTLE_ATTACK_ANS : NetMsgHeadInterface
        {

            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.AUTO_BATTLE_ATTACK_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int ActionUnitID;
            public int ActivateSkillID;

            public int TargetTileX;
            public int TargetTileY;

            public BATTLE_CHARACTER_DATA ActiveCharacterData;

            public int TargetCount;
            public BATTLE_CHARACTER_DATA[] TargetData;

            public bool IsTurnEndData;

            public bool IsBattleEnd;

            public bool WinLose;

            public int NextTurnUnitID;

            public BATTLE_CHARACTER_DATA[] ChangeAllyData;
            public BATTLE_CHARACTER_DATA[] ChangeEnemyData;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                ActionUnitID = ReadInt(bytes, ref pos);
                ActivateSkillID = ReadInt(bytes, ref pos);

                TargetTileX = ReadInt(bytes, ref pos);
                TargetTileY = ReadInt(bytes, ref pos);

                ActiveCharacterData.ReadRecvBuffer(bytes, ref pos);

                TargetCount = ReadInt(bytes, ref pos);

                TargetData = new BATTLE_CHARACTER_DATA[Define.BATTLE_ALL_MAX * 2];
                for (int i = 0; i < TargetData.Length; i++)
                {
                    TargetData[i].ReadRecvBuffer(bytes, ref pos);
                }

                IsTurnEndData = Readboolean(bytes, ref pos);

                IsBattleEnd = Readboolean(bytes, ref pos);

                WinLose = Readboolean(bytes, ref pos);

                NextTurnUnitID = ReadInt(bytes, ref pos);

                ChangeAllyData = new BATTLE_CHARACTER_DATA[Define.BATTLE_ALL_MAX];
                for (int i = 0; i < ChangeAllyData.Length; i++)
                {
                    ChangeAllyData[i].ReadRecvBuffer(bytes, ref pos);
                }
                ChangeEnemyData = new BATTLE_CHARACTER_DATA[Define.BATTLE_ALL_MAX];
                for (int i = 0; i < ChangeEnemyData.Length; i++)
                {
                    ChangeEnemyData[i].ReadRecvBuffer(bytes, ref pos);
                }
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _ITEM_SELL_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.ITEM_SELL_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int gold;
            public int GUID;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                gold = ReadInt(bytes, ref pos);
                GUID = ReadInt(bytes, ref pos);
            }
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _CHARACTER_INVENTORY_ANS : NetMsgHeadInterface
        {
            public void initNetHead()
            {
                head.size = (ushort)(sizeof(int));
                head.type = (ushort)MsgType.CHARACTER_INVENTORY_ANS;
            }

            public void WriteSendBuffer(byte[] buf, int offset) { }
            public byte[] toBytesArray() { return null; }

            public NetMsgHead head;

            public int helmets_id;
            public int sword_id;
            public int armors_id;
            public int boots_id;

            public void ReadRecvBuffer(byte[] bytes)
            {
                int pos = 0;
                head.size = ReaduShort(bytes, ref pos);
                head.type = ReaduShort(bytes, ref pos);

                helmets_id = ReadInt(bytes, ref pos);
                sword_id = ReadInt(bytes, ref pos);
                armors_id = ReadInt(bytes, ref pos);
                boots_id = ReadInt(bytes, ref pos);
            }
        };

    }
}

