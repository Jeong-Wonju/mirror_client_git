using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FoxSDK;

public class PacketSender : Singleton<PacketSender>
{
    byte[] buf = new byte[1024];

    GameNetMessage.NetMsgHeadInterface submsg;

    void Start()
    {

    }


    void MakeBuf(GameNetMessage.NetMsgHeadInterface msg, int offset)
    {
        Debug.Log("(S -->)" + msg.GetType());

        msg.WriteSendBuffer(buf, offset);

        GameSocketManager.instance.sendMsg(msg);
    }

    void MakeSubBuf(GameNetMessage.NetMsgHeadInterface msg, int offset)
    {
        submsg = msg;
        submsg.WriteSendBuffer(buf, offset);
    }

    public void SendSubBufmsg()
    {
        GameSocketManager.instance.sendMsg(submsg);
    }

    //-------------------------------------------------------------------------
    // 데이터 관련 정보를 바이트 배열로 
    //-------------------------------------------------------------------------
    void reverseIntToByte4(byte[] data, int value, int offset)
    {
        data[offset++] = (byte)(value & 0xFF);
        data[offset++] = (byte)((value & 0xFF00) >> 8);
        data[offset++] = (byte)((value & 0xFF0000) >> 16);
        data[offset] = (byte)((value & 0xFF000000) >> 24);
    }

    //-------------------------------------------------------------------------
    void reverseIntToByte2(byte[] data, int value, int offset)
    {
        data[offset++] = (byte)(value & 0xFF);
        data[offset] = (byte)((value & 0xFF00) >> 8);
    }

    void WriteString(string v, ref int offset, int max_len)
    {
        int len = v.Length * 2;
        System.Buffer.BlockCopy(v.ToCharArray(), 0, buf, offset, len);
        offset += (max_len * 2);
    }

    void WriteStringA(string v, ref int offset, int max_len)
    {
        int len = v.Length;
        System.Buffer.BlockCopy(v.ToCharArray(), 0, buf, offset, len);
        offset += max_len;
    }

    void WriteFloat(float v, ref int offset)
    {
        byte[] byte_v = System.BitConverter.GetBytes(v);
        System.Buffer.BlockCopy(byte_v, 0, buf, offset, byte_v.Length);
        offset += 4;
    }

    void WriteShort(ushort v, ref int offset)
    {
        reverseIntToByte2(buf, v, offset);
        offset += 2;
    }

    void WriteInt(int v, ref int offset)
    {
        reverseIntToByte4(buf, v, offset);
        offset += 4;
    }

    public void SendCreateUserId(string nickName, float vesion)
    {
        try
        {
            int offset = 0;

            WriteInt(nickName.Length, ref offset);
            WriteString(nickName, ref offset, Define.MAX_NICKLEN);

            WriteFloat(vesion, ref offset);

            GameNetMessage._CREATE_GAME_ID_REQ CreateUserIdMsg = new GameNetMessage._CREATE_GAME_ID_REQ();

            MakeBuf(CreateUserIdMsg, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }

    }

    public void SendMemberJoin(string ID, float fX, float fY, float version)
    {
        try
        {
            int offset = 0;

            Debug.Log("SendMemberJoin userID + " + ID);

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);
            WriteFloat(version, ref offset);

            GameNetMessage._MEMBERJOIN_REQ member_join = new GameNetMessage._MEMBERJOIN_REQ();

            MakeBuf(member_join, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendGoogleLogin(string GoogleKey, string NickName, float version)
    {
        try
        {
            int offset = 0;

            Debug.Log("SendGoogleLogin0");

            Debug.Log("GoogleKey : " + GoogleKey);

            Debug.Log("GoogleKeyLength : " + GoogleKey.Length);

            Debug.Log("NickName : " + NickName);

            Debug.Log("NickName.Length : " + NickName.Length);

            Debug.Log("SendGoogleLogin1");

            WriteInt(GoogleKey.Length, ref offset);

            Debug.Log("SendGoogleLogin1.5");

            WriteString(GoogleKey, ref offset, Define.MAX_GOOGLE_KEY_LEN);

            Debug.Log("SendGoogleLogin2");

            WriteInt(NickName.Length, ref offset);
            WriteString(NickName, ref offset, Define.MAX_NICKLEN);

            Debug.Log("SendGoogleLogin3");

            WriteFloat(version, ref offset);

            Debug.Log("Version :" + version);

            GameNetMessage._GOOGLE_PLAY_LOGIN_REQ CreateUserIdMsg = new GameNetMessage._GOOGLE_PLAY_LOGIN_REQ();

            Debug.Log("SendGoogleLogin4");

            MakeBuf(CreateUserIdMsg, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }


    public void SendGoogleAccountLink(string ID, string GoogleKey, string NickName)
    {
        try
        {
            int offset = 0;

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            WriteInt(GoogleKey.Length, ref offset);
            WriteString(GoogleKey, ref offset, Define.MAX_GOOGLE_KEY_LEN);

            WriteInt(NickName.Length, ref offset);
            WriteString(NickName, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._GOOGLE_ACCOUNT_LINK_REQ CreateUserIdMsg = new GameNetMessage._GOOGLE_ACCOUNT_LINK_REQ();

            MakeBuf(CreateUserIdMsg, offset);
        }

        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendCheckGoogleKeyExist(string GoogleKey)
    {
        try
        {
            int offset = 0;

            WriteInt(GoogleKey.Length, ref offset);
            WriteString(GoogleKey, ref offset, Define.MAX_GOOGLE_KEY_LEN);

            GameNetMessage._GOOGLE_KEY_CHECK_REQ CreateUserIdMsg = new GameNetMessage._GOOGLE_KEY_CHECK_REQ();

            MakeBuf(CreateUserIdMsg, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendLogin(string userID, float version)
    {
        try
        {
            int offset = 0;

            Debug.Log("Send userID + " + userID);

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);
            WriteFloat(version, ref offset);
            ///WriteString(version, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._LOGIN_REQ send_login = new GameNetMessage._LOGIN_REQ();

            MakeBuf(send_login, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendServerList()
    {
        try
        {
            int offset = 0;

            GameNetMessage._SERVERLIST_REQ send_svr_list = new GameNetMessage._SERVERLIST_REQ();

            MakeBuf(send_svr_list, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendUserCreate(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._USER_IDENTIFIER_REQ user_identifier = new GameNetMessage._USER_IDENTIFIER_REQ();

            MakeBuf(user_identifier, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendHeartBeat(string userID)
    {
        try
        {
            int offset = 0;

            GameNetMessage.SEND_RECV_MSG_HEART heart = new GameNetMessage.SEND_RECV_MSG_HEART();

            MakeBuf(heart, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void StageConQuestReq(string userID, int worldidx, int cur_stageidx, int stageidx)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);
            WriteInt(worldidx, ref offset);
            WriteInt(cur_stageidx, ref offset);
            WriteInt(stageidx, ref offset);

            GameNetMessage._REQ_CONQUEST conquest = new GameNetMessage._REQ_CONQUEST();

            MakeSubBuf(conquest, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void StageSkipConquestReq(string userID, int worldidx, int cur_stageidx, int stageidx)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);
            WriteInt(worldidx, ref offset);
            WriteInt(cur_stageidx, ref offset);
            WriteInt(stageidx, ref offset);

            GameNetMessage._SKIP_CONQUEST_REQ conquest = new GameNetMessage._SKIP_CONQUEST_REQ();

            MakeBuf(conquest, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void StageClear(string userID, int worldidx, int stageidx, int next_obj_id)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);
            WriteInt(worldidx, ref offset);
            WriteInt(stageidx, ref offset);
            WriteInt(next_obj_id, ref offset);

            GameNetMessage._STAGE_CLEAR_REQ stage_clear = new GameNetMessage._STAGE_CLEAR_REQ();

            MakeBuf(stage_clear, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendUserGoodsUpgrade(string userID, int cur_world_idx, int cur_stage_idx, int cur_stage_obj)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(cur_world_idx, ref offset);
            WriteInt(cur_stage_idx, ref offset);
            WriteInt(cur_stage_obj, ref offset);

            GameNetMessage._USER_GOODS_UPGRADE_REQ user_goods_update = new GameNetMessage._USER_GOODS_UPGRADE_REQ();

            MakeBuf(user_goods_update, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendUserGoodsGet(string userID, int world_idx, int stage_idx, int object_idx)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(world_idx, ref offset);
            WriteInt(stage_idx, ref offset);
            WriteInt(object_idx, ref offset);

            GameNetMessage._USER_GOODS_GET_REQ user_goods_update = new GameNetMessage._USER_GOODS_GET_REQ();

            MakeBuf(user_goods_update, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }


    public void SendHavestCard(string userID, int card_id)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(card_id, ref offset);

            GameNetMessage._GET_CARD_REQ get_card_req = new GameNetMessage._GET_CARD_REQ();

            MakeBuf(get_card_req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }


    

    public void SendCastleConQuest(string userID, int world_idx, int stage_idx, int object_idx, int StageClearFlag)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(world_idx, ref offset);
            WriteInt(stage_idx, ref offset);
            WriteInt(object_idx, ref offset);
            WriteInt(StageClearFlag, ref offset);

            GameNetMessage._CASTLE_CONQUEST_REQ castle_conquest = new GameNetMessage._CASTLE_CONQUEST_REQ();

            MakeBuf(castle_conquest, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    

    public void SendBattleConnect(string userID)
    {
        try
        {
            GameNetMessage._BATTLE_CONNECT_REQ battle_connect = new GameNetMessage._BATTLE_CONNECT_REQ();

            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(UserInfoEx.Instance.BattleType, ref offset);

            WriteInt(UserInfoEx.Instance.MonsterGroupId, ref offset);
            WriteInt(UserInfoEx.Instance.DropId, ref offset);

            WriteInt(UserInfoEx.Instance.DragonGroupId, ref offset);
            WriteInt(UserInfoEx.Instance.DragonDropId, ref offset);

            WriteInt(UserInfoEx.Instance.PvPUserId.Length, ref offset);
            WriteString(UserInfoEx.Instance.PvPUserId, ref offset, Define.MAX_NICKLEN);

            WriteInt(UserInfoEx.Instance.GuildBattleId.Length, ref offset);
            WriteString(UserInfoEx.Instance.GuildBattleId, ref offset, Define.MAX_NICKLEN);

            MakeBuf(battle_connect, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendSkipBattleConnect(string userID)
    {
        try
        {
            GameNetMessage._SKIP_BATTLE_CONNECT_REQ battle_connect = new GameNetMessage._SKIP_BATTLE_CONNECT_REQ();

            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(UserInfoEx.Instance.BattleType, ref offset);

            WriteInt(UserInfoEx.Instance.MonsterGroupId, ref offset);
            WriteInt(UserInfoEx.Instance.DropId, ref offset);

            WriteInt(UserInfoEx.Instance.DragonGroupId, ref offset);
            WriteInt(UserInfoEx.Instance.DragonDropId, ref offset);

            WriteInt(UserInfoEx.Instance.PvPUserId.Length, ref offset);
            WriteString(UserInfoEx.Instance.PvPUserId, ref offset, Define.MAX_NICKLEN);

            MakeBuf(battle_connect, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }


    

    

    public void SendDropItem(string userID, int m_group)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(m_group, ref offset);

            GameNetMessage._SELECT_DROP_ID_REQ Select_Drop_ID = new GameNetMessage._SELECT_DROP_ID_REQ();

            MakeBuf(Select_Drop_ID, offset);

        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    

    public void SendCastleDeploy(string userID, int worldId, int stageId, int stageIndex, int DeployIndex)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(worldId, ref offset);
            WriteInt(stageId, ref offset);
            WriteInt(stageIndex, ref offset);
            WriteInt(DeployIndex, ref offset);

            GameNetMessage._CASTLE_DEPLOY_REQ Select_Drop_ID = new GameNetMessage._CASTLE_DEPLOY_REQ();

            MakeBuf(Select_Drop_ID, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendRebellion(string userID, int world_idx, int stage_idx, int object_idx)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(world_idx, ref offset);
            WriteInt(stage_idx, ref offset);
            WriteInt(object_idx, ref offset);

            GameNetMessage._REBELLION_REQ Rebellion = new GameNetMessage._REBELLION_REQ();

            MakeSubBuf(Rebellion, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendSkipRebellion(string userID, int world_idx, int stage_idx, int object_idx)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(world_idx, ref offset);
            WriteInt(stage_idx, ref offset);
            WriteInt(object_idx, ref offset);

            GameNetMessage._SKIP_REBELLION_REQ Rebellion = new GameNetMessage._SKIP_REBELLION_REQ();

            MakeBuf(Rebellion, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendGacha(string userID, int gacha_num, int g_id)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(gacha_num, ref offset);
            WriteInt(g_id, ref offset);

            GameNetMessage._GACHA_REQ Gacha = new GameNetMessage._GACHA_REQ();
            MakeBuf(Gacha, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendCrystal(string userID, string ProductID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(ProductID.Length, ref offset);
            WriteString(ProductID, ref offset, Define.SHOP_KEY_MAX_LENGTH);

            GameNetMessage._CRYSTAL_REQ Crystal = new GameNetMessage._CRYSTAL_REQ();
            MakeBuf(Crystal, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendDragonExpedition(string userID, int Refresh)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(Refresh, ref offset);

            GameNetMessage._DRAGON_EXPEDITION_REQ Dragon = new GameNetMessage._DRAGON_EXPEDITION_REQ();
            MakeBuf(Dragon, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendPvPDummyUserReg(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._PVP_DUMMYUSER_REG dummy_reg = new GameNetMessage._PVP_DUMMYUSER_REG();
            MakeBuf(dummy_reg, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendContinueBattleReportRequest(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._CONTINUE_BATTLE_REPORT_REQ ContinueBattleReport = new GameNetMessage._CONTINUE_BATTLE_REPORT_REQ();
            MakeBuf(ContinueBattleReport, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendBattleEndReq(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._BATTLE_END_REQ ContinueBattleReport = new GameNetMessage._BATTLE_END_REQ();
            MakeBuf(ContinueBattleReport, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendPvPUserListReq(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._PVP_USERLIST_REQ PvpUserList = new GameNetMessage._PVP_USERLIST_REQ();
            MakeBuf(PvpUserList, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendPvP(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._PVP_REQ Pvp_req = new GameNetMessage._PVP_REQ();
            MakeSubBuf(Pvp_req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendGuildBattle(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(UserInfoEx.Instance.GuildBattleId.Length, ref offset);
            WriteString(UserInfoEx.Instance.GuildBattleId, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._GUILDBATTLE_REQ guildbattle_req = new GameNetMessage._GUILDBATTLE_REQ();
            MakeSubBuf(guildbattle_req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendSkipPvP(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._SKIP_PVP_REQ Pvp_req = new GameNetMessage._SKIP_PVP_REQ();
            MakeBuf(Pvp_req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    


    public void SendOpenShopReq(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._OPEN_SHOP_REQ ContinueBattleReport = new GameNetMessage._OPEN_SHOP_REQ();
            MakeBuf(ContinueBattleReport, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    

    public void SendDivideCardReq(string userID, int GUID, int Count)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(GUID, ref offset);
            WriteInt(Count, ref offset);

            GameNetMessage._CARD_DIVISION_REQ CardDivisionReq = new GameNetMessage._CARD_DIVISION_REQ();
            MakeBuf(CardDivisionReq, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }


    public void SendBattleDragon(string userID, int group_id, int drop_id)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(group_id, ref offset);
            WriteInt(drop_id, ref offset);

            GameNetMessage._BATTLE_DRAGON_EXPEDITION_REQ BattleDragon = new GameNetMessage._BATTLE_DRAGON_EXPEDITION_REQ();
            MakeSubBuf(BattleDragon, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendSkipDragon(string userID, int group_id, int drop_id)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(group_id, ref offset);
            WriteInt(drop_id, ref offset);

            GameNetMessage._SKIP_DRAGON_EXPEDION_REQ BattleDragon = new GameNetMessage._SKIP_DRAGON_EXPEDION_REQ();
            MakeBuf(BattleDragon, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }


    public void SendOpenShop(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._OPEN_SHOP_REQ CardDivisionReq = new GameNetMessage._OPEN_SHOP_REQ();
            MakeBuf(CardDivisionReq, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendBuyShopProduct(string userID, string ProductId)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(ProductId.Length, ref offset);
            WriteString(ProductId, ref offset, Define.SHOP_KEY_MAX_LENGTH);

            GameNetMessage._BUY_SHOP_PRODUCT_REQ BuyShopProductReq = new GameNetMessage._BUY_SHOP_PRODUCT_REQ();
            MakeBuf(BuyShopProductReq, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendPvpStageInfo(string pvp_userID, string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(pvp_userID.Length, ref offset);
            WriteString(pvp_userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._PVP_STAGEINFO_REQ pvpStageInfo = new GameNetMessage._PVP_STAGEINFO_REQ();
            MakeBuf(pvpStageInfo, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }
    public void SendPvpUserGoodGet(string userID, string pvp_userID, int world_idx, int stage_idx, int stage_obj)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(pvp_userID.Length, ref offset);
            WriteString(pvp_userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(world_idx, ref offset);
            WriteInt(stage_idx, ref offset);
            WriteInt(stage_obj, ref offset);

            GameNetMessage._PVP_GOODSGET_REQ pvpGoodsGet = new GameNetMessage._PVP_GOODSGET_REQ();
            MakeBuf(pvpGoodsGet, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendPvpEnd(string userID, string pvp_userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(pvp_userID.Length, ref offset);
            WriteString(pvp_userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._PVP_END_REQ PvpEnd = new GameNetMessage._PVP_END_REQ();
            MakeBuf(PvpEnd, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SentRefreshCardInfo(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._REFRESH_CARD_LIST_REQ RefreshCardListReq = new GameNetMessage._REFRESH_CARD_LIST_REQ();
            MakeBuf(RefreshCardListReq, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendMissionListReq(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._GET_MISSION_LIST_REQ RefreshCardListReq = new GameNetMessage._GET_MISSION_LIST_REQ();
            MakeBuf(RefreshCardListReq, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendMissionClearReq(string userID, int missionId)
    {
        try
        {
            if (GameManager.instance.MissionFlag == true)
            {
                return;
            }

            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(missionId, ref offset);

            GameNetMessage._QUEST_CLEAR_REQ RefreshCardListReq = new GameNetMessage._QUEST_CLEAR_REQ();
            MakeBuf(RefreshCardListReq, offset);

            GameManager.instance.MissionFlag = true;
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendTutorialComplete(string userID, string TutorialKey)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(TutorialKey.Length, ref offset);
            WriteString(TutorialKey, ref offset, Define.SHOP_KEY_MAX_LENGTH);

            GameNetMessage._TUTORIAL_COMPLETE_REQ RefreshCardListReq = new GameNetMessage._TUTORIAL_COMPLETE_REQ();
            MakeBuf(RefreshCardListReq, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendDragonCountAddReq(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._DRAGON_EXPEDITION_ADD_COUNT_REQ Dragon_add = new GameNetMessage._DRAGON_EXPEDITION_ADD_COUNT_REQ();
            MakeBuf(Dragon_add, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendPvpCountAddReq(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._PVP_COUNT_ADD_REQ Pvp_add = new GameNetMessage._PVP_COUNT_ADD_REQ();
            MakeBuf(Pvp_add, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendPvpLoginCheckLosePopupReq(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._PVP_LOGIN_LOSE_NOTI_REQ Pvp_login = new GameNetMessage._PVP_LOGIN_LOSE_NOTI_REQ();
            MakeBuf(Pvp_login, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendSellCard(string ID, int guid)
    {
        try
        {
            int offset = 0;

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            WriteInt(guid, ref offset);

            GameNetMessage._SELL_CARD_REQ CardSellReq = new GameNetMessage._SELL_CARD_REQ();
            MakeBuf(CardSellReq, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }
    public void SendMatchingUserListReq(string ID)
    {
        try
        {
            int offset = 0;

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._PVP_MATCHING_LIST_REQ CardSellReq = new GameNetMessage._PVP_MATCHING_LIST_REQ();
            MakeBuf(CardSellReq, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendTutorialGachaReq(string ID)
    {
        try
        {
            int offset = 0;

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._TUTORIAL_GACHA_REQ TutoGacha = new GameNetMessage._TUTORIAL_GACHA_REQ();
            MakeBuf(TutoGacha, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendTutorialGoodset(string ID)
    {
        try
        {
            int offset = 0;

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._TUTORIAL_GOODSGET_REQ TutoGacha = new GameNetMessage._TUTORIAL_GOODSGET_REQ();
            MakeBuf(TutoGacha, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendStageRebellionCheck(string ID, int world_idx, int stage_idx)
    {
        try
        {
            int offset = 0;

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            WriteInt(world_idx, ref offset);
            WriteInt(stage_idx, ref offset);

            GameNetMessage._STAGE_REBELLION_CHECK_REQ Rebellion_Check = new GameNetMessage._STAGE_REBELLION_CHECK_REQ();
            MakeBuf(Rebellion_Check, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendGamble(string ID, int gamble_count)
    {
        try
        {
            int offset = 0;

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            WriteInt(gamble_count, ref offset);

            GameNetMessage._GAMBLE_REQ Gamble = new GameNetMessage._GAMBLE_REQ();
            MakeBuf(Gamble, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendAllGoods(string ID)
    {
        try
        {
            int offset = 0;

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._ALL_USER_GOODS_GET_REQ AllGoodsGet = new GameNetMessage._ALL_USER_GOODS_GET_REQ();
            MakeBuf(AllGoodsGet, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendAttendanceCheck(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._ATTENDANCE_CHECK_REQ check = new GameNetMessage._ATTENDANCE_CHECK_REQ();
            MakeBuf(check, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendAttendanceGet(string userID, int package_index)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);
            WriteInt(package_index, ref offset);

            GameNetMessage._ATTENDANCE_GET_REQ get = new GameNetMessage._ATTENDANCE_GET_REQ();
            MakeBuf(get, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendChatMsg(string ID, string Msg)
    {
        int offset = 0;

        WriteInt(ID.Length, ref offset);
        WriteString(ID, ref offset, Define.MAX_NICKLEN);

        WriteInt(Msg.Length, ref offset);
        WriteString(Msg, ref offset, Define.MAX_MSGLEN);

        //int type = 0;
        //switch(UserInfoEx.Instance.chat_type)
        {
            //case Chat_Type.Global: type = 1; break;
            //case Chat_Type.Guild: type = 3; break;
        }

        WriteInt((int)UserInfoEx.Instance.chat_type, ref offset);

        GameNetMessage._CHAT_MSG_REQ Gamble = new GameNetMessage._CHAT_MSG_REQ();
        MakeBuf(Gamble, offset);
    }

    public void SendTutorialDragonExpedionMsg(string userID)
    {
        try
        {
            int offset = 0;
            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);
            GameNetMessage._TUTORIAL_DRAGON_EXPEDION_REQ req = new GameNetMessage._TUTORIAL_DRAGON_EXPEDION_REQ();
            MakeBuf(req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
            return;
        }
    }

    public void SendTutorialDragonCompleteMsg(string userID)
    {
        try
        {
            int offset = 0;
            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);
            GameNetMessage._TUTORIAL_DRAGON_COMPLETE_REQ req = new GameNetMessage._TUTORIAL_DRAGON_COMPLETE_REQ();
            MakeBuf(req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendAllUserGoodsCheck(string ID)
    {
        try
        {
            int offset = 0;

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._ALL_USER_GOODS_GET_CHECK_REQ Check = new GameNetMessage._ALL_USER_GOODS_GET_CHECK_REQ();
            MakeBuf(Check, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }
    public void SendMailList(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._MAIL_LIST_REQ req = new GameNetMessage._MAIL_LIST_REQ();
            MakeBuf(req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendGetMailItem(string userID, int guid)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(guid, ref offset);

            GameNetMessage._MAIL_ITEM_REQ req = new GameNetMessage._MAIL_ITEM_REQ();
            MakeBuf(req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendGuildCreate(string userID, string guild_name, int icn_idx)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(guild_name.Length, ref offset);
            WriteString(guild_name, ref offset, Define.MAX_NICKLEN);

            WriteInt(icn_idx, ref offset);

            GameNetMessage._GUILD_CREATE_REQ req = new GameNetMessage._GUILD_CREATE_REQ();
            MakeBuf(req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendGuildJoin(string userID, string guild_name)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(guild_name.Length, ref offset);
            WriteString(guild_name, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._GUILD_JOIN_REQ req = new GameNetMessage._GUILD_JOIN_REQ();
            MakeBuf(req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendGuildLeave(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._GUILD_LEAVE_REQ req = new GameNetMessage._GUILD_LEAVE_REQ();
            MakeBuf(req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendGuildSetIntroduce(string userID, string msg)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(msg.Length, ref offset);
            WriteString(msg, ref offset, Define.MAX_LONGMSGLEN);

            GameNetMessage._GUILD_INTRODUCE_REQ req = new GameNetMessage._GUILD_INTRODUCE_REQ();
            MakeBuf(req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendGuildSetNotice(string userID, string msg)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(msg.Length, ref offset);
            WriteString(msg, ref offset, Define.MAX_LONGMSGLEN);

            GameNetMessage._GUILD_NOTICE_REQ req = new GameNetMessage._GUILD_NOTICE_REQ();
            MakeBuf(req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendGuildGroupListReq(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);
            
            GameNetMessage._GUILD_GROUPLIST_REQ req = new GameNetMessage._GUILD_GROUPLIST_REQ();
            MakeBuf(req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendGuildBattleInfoReq(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._GUILD_BATTLEINFO_REQ req = new GameNetMessage._GUILD_BATTLEINFO_REQ();
            MakeBuf(req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendGuildMemberListReq(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._GUILD_MEMBERLIST_REQ req = new GameNetMessage._GUILD_MEMBERLIST_REQ();
            MakeBuf(req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendGuildJoinListReq(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._GUILD_JOINLIST_REQ req = new GameNetMessage._GUILD_JOINLIST_REQ();
            MakeBuf(req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendGuildJoinAgreeReq(string userID, string JoinUserNick)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(JoinUserNick.Length, ref offset);
            WriteString(JoinUserNick, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._GUILD_JOINAGREE_REQ req = new GameNetMessage._GUILD_JOINAGREE_REQ();
            MakeBuf(req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendGuildJoinRejectReq(string userID, string JoinUserNick)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(JoinUserNick.Length, ref offset);
            WriteString(JoinUserNick, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._GUILD_JOINREJECT_REQ req = new GameNetMessage._GUILD_JOINREJECT_REQ();
            MakeBuf(req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendGuildMemberExileReq(string userID, string ExileUserNick)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(ExileUserNick.Length, ref offset);
            WriteString(ExileUserNick, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._GUILD_MEMBEREXILE_REQ req = new GameNetMessage._GUILD_MEMBEREXILE_REQ();
            MakeBuf(req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendGuildBattleReadyReq(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._GUILD_BATTLEREADY_REQ req = new GameNetMessage._GUILD_BATTLEREADY_REQ();
            MakeBuf(req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendGuildBattleMatchingReq(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);
            
            GameNetMessage._GUILD_BATTLEMATCHING_REQ req = new GameNetMessage._GUILD_BATTLEMATCHING_REQ();
            MakeBuf(req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void sendJoinCancel(string userID)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._GUILD_JOINCANCEL_REQ req = new GameNetMessage._GUILD_JOINCANCEL_REQ();
            MakeBuf(req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendEnemyGuildDeckReq(string userID, string enemyNick)
    {
        try
        {
            int offset = 0;

            WriteInt(userID.Length, ref offset);
            WriteString(userID, ref offset, Define.MAX_NICKLEN);

            WriteInt(enemyNick.Length, ref offset);
            WriteString(enemyNick, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._GUILD_ENEMYDECK_REQ req = new GameNetMessage._GUILD_ENEMYDECK_REQ();
            MakeBuf(req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendBattleCharacterAction(string ID, int ActionType, int CurrentBattleUnitID, int SkillNumber, int CharacterStartX, int CharacterStartY, int CharacterEndX, int CharacterEndY, int TargetUnitID, int TargetPosX, int TargetPosY)
    {

        try
        {
            int offset = 0;

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            WriteInt(ActionType, ref offset);
            WriteInt(CurrentBattleUnitID, ref offset);

            WriteInt(CharacterStartX, ref offset);
            WriteInt(CharacterStartY, ref offset);

            WriteInt(CharacterEndX, ref offset);
            WriteInt(CharacterEndY, ref offset);

            WriteInt(SkillNumber, ref offset);

            WriteInt(TargetUnitID, ref offset);
            WriteInt(TargetPosX, ref offset);
            WriteInt(TargetPosY, ref offset);



            GameNetMessage._BATTLE_ACTION_REQ Check = new GameNetMessage._BATTLE_ACTION_REQ();
            MakeBuf(Check, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }


    }

    public void SendBattleCharacterDelayAction(string ID, int BattleUnitID)
    {
        try
        {
            int offset = 0;

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            WriteInt(BattleUnitID, ref offset);

            GameNetMessage._BATTLE_DELAY_TURN_REQ Check = new GameNetMessage._BATTLE_DELAY_TURN_REQ();
            MakeBuf(Check, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendBattleCharacterSkipAction(string ID)
    {

    }

    public void SendBattleMoveCharacter(string ID, int BattleUnitID, int StartPosX, int StartPosY, int DestPosX, int DestPosY)
    {
        try
        {
            int offset = 0;

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            WriteInt(BattleUnitID, ref offset);

            WriteInt(StartPosX, ref offset);
            WriteInt(StartPosY, ref offset);
            WriteInt(DestPosX, ref offset);
            WriteInt(DestPosY, ref offset);

            GameNetMessage._BATTLE_UNIT_MOVE_REQ Check = new GameNetMessage._BATTLE_UNIT_MOVE_REQ();
            MakeBuf(Check, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendBattleAttackCharacter(string ID, int BattleUnitID, int ActiveSkillID, int TargetUnitID, int TargetTileX, int TargetTileY)
    {
        try
        {
            int offset = 0;

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            WriteInt(BattleUnitID, ref offset);
            WriteInt(ActiveSkillID, ref offset);
            WriteInt(TargetUnitID, ref offset);
            WriteInt(TargetTileX, ref offset);
            WriteInt(TargetTileY, ref offset);

            GameNetMessage._BATTLE_UNIT_ATTACK_REQ Check = new GameNetMessage._BATTLE_UNIT_ATTACK_REQ();
            MakeBuf(Check, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendBattleEndTurnReq(string ID, int BattleUnitID)
    {
        try
        {
            int offset = 0;

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            WriteInt(BattleUnitID, ref offset);

            GameNetMessage._BATTLE_END_TURN_REQ Check = new GameNetMessage._BATTLE_END_TURN_REQ();
            MakeBuf(Check, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendBattleGiveUpReq(string ID)
    {
        try
        {
            int offset = 0;

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage.BATTLE_GIVE_UP_REQ Req = new GameNetMessage.BATTLE_GIVE_UP_REQ();
            MakeBuf(Req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendUnitDefenseReq(string ID, int currentActionUnitID)
    {
        try
        {
            int offset = 0;

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            WriteInt(currentActionUnitID, ref offset);

            GameNetMessage.BATTLE_UNIT_DEFENSE_REQ Req = new GameNetMessage.BATTLE_UNIT_DEFENSE_REQ();
            MakeBuf(Req, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendHeroAttackReq(string ID, int ActiveSkillID, int TargetUnitID, int TargetTileX, int TargetTileY)
	{
        try
        {
            int offset = 0;

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            WriteInt(TargetUnitID, ref offset);
            WriteInt(ActiveSkillID, ref offset);
            WriteInt(TargetTileX, ref offset);
            WriteInt(TargetTileY, ref offset);

            GameNetMessage._HERO_ACTION_REQ Check = new GameNetMessage._HERO_ACTION_REQ();
            MakeBuf(Check, offset);

        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }

        
    }

    public void SendAutoBattleAttackCharacter(string ID)
    {
        try
        {
            int offset = 0;

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            GameNetMessage._AUTO_BATTLE_ATTACK_REQ Check = new GameNetMessage._AUTO_BATTLE_ATTACK_REQ();
            MakeBuf(Check, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendCharacterInventory(string ID, int helmet, int sword, int armors, int boots)
    {
        try
        {
            int offset = 0;

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            WriteInt(helmet, ref offset);
            WriteInt(sword, ref offset);
            WriteInt(armors, ref offset);
            WriteInt(boots, ref offset);

            GameNetMessage._CHARACTER_INVENTORY_REQ inven = new GameNetMessage._CHARACTER_INVENTORY_REQ();
            MakeBuf(inven, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }

    public void SendItemSell(string ID, int guid)
    {
        try
        {
            int offset = 0;

            WriteInt(ID.Length, ref offset);
            WriteString(ID, ref offset, Define.MAX_NICKLEN);

            WriteInt(guid, ref offset);

            GameNetMessage._ITEM_SELL_REQ sell = new GameNetMessage._ITEM_SELL_REQ();
            MakeBuf(sell, offset);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Exception: {e}");
        }
    }
}
