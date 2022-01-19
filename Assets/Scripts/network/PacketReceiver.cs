using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
//using EnhancedUI;
using FoxSDK;

public class PacketReceiver : MonoBehaviour
{



    void Start()
    {
        Debug.Log("<color=#00ff00>PacketReceiver create</color>");

        GameSocketManager.instance.regeditMsg((int)GameNetMessage.MsgType.ACCEPT_AUTH_ANS,
            AcceptAuthAns,
            typeof(GameNetMessage._ACCEPT_AUTH_ANS),
            new GameNetMessage._ACCEPT_AUTH_ANS());

        GameSocketManager.instance.regeditMsg((int)GameNetMessage.MsgType.MEMBERJOIN_ANS,
            MemberJoinAns,
            typeof(GameNetMessage._MEMBERJOIN_ANS),
            new GameNetMessage._MEMBERJOIN_ANS());

        GameSocketManager.instance.regeditMsg((int)GameNetMessage.MsgType.REMEMBERJOIN_ANS,
            ReMemberJoinAns,
            typeof(GameNetMessage._REMEMBERJOIN_ANS),
            new GameNetMessage._REMEMBERJOIN_ANS());

        GameSocketManager.instance.regeditMsg((int)GameNetMessage.MsgType.LOGIN_ANS,
            LoginAns,
            typeof(GameNetMessage._LOGIN_ANS),
            new GameNetMessage._LOGIN_ANS());

        GameSocketManager.instance.regeditMsg((int)GameNetMessage.MsgType.SERVERLIST_ANS,
            ServerListAns,
            typeof(GameNetMessage._SERVERLIST_ANS),
            new GameNetMessage._SERVERLIST_ANS());

        GameSocketManager.instance.regeditMsg((int)GameNetMessage.MsgType.ACCEPT_GAME_ANS,
            AcceptGameAns,
            typeof(GameNetMessage._ACCEPT_GAME_ANS),
            new GameNetMessage._ACCEPT_GAME_ANS());

        GameSocketManager.instance.regeditMsg((int)GameNetMessage.MsgType.USER_IDENTIFIER_ANS,
            UserIdentifierAns,
            typeof(GameNetMessage._USER_IDENTIFIER_ANS),
            new GameNetMessage._USER_IDENTIFIER_ANS());

        

    }

    void VesitonCheckAns(GameNetMessage.NetMsgHeadInterface head)
    {
        GameNetMessage._VESITON_CHECK_ANS msg = (GameNetMessage._VESITON_CHECK_ANS)head;

        Debug.LogError("VesitonCheckAns : Do check error");

        //Application.OpenURL("https://play.google.com/store/apps/details?id=com.Fantajoy.Dragon2M");
    }

    void AcceptAuthAns(GameNetMessage.NetMsgHeadInterface head)
    {
        GameNetMessage._ACCEPT_AUTH_ANS msg = (GameNetMessage._ACCEPT_AUTH_ANS)head;

        Debug.Log("AcceptAuthAns");

        // 계정 검사
        string filepath = "";

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {

        }
        else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {

        }

        // 계정 ID가 있으면 
        if (PlayerPrefs.HasKey("Name"))
        {
            //DataSaveManager.instance.Save();
            //string strUserID = DataSaveManager.instance.userID;
            /*string strUserID = DataController.Instance.userdata.userID;
            DataController.Instance.SaveGameData();*/
            //Debug.Log("user ID : " + strUserID);

            //if (strUserID == null || strUserID == "")
            {
                //Debug.Log("User ID Invalid");
            }
            //else
            {
                //UserInfoEx.Instance.m_strUserID = strUserID;

                //float fX = (float)NativeToolkit.GetLongitude();
                //float fY = (float)NativeToolkit.GetLatitude();
                //Debug.Log("fX =" + fX.ToString() + "fY =" + fY.ToString());
                //FantaJoyNetworkManager.Instance.GetComponent<PacketSender>().SendLogin(strUserID, 0.0f, 0.0f);

                //Debug.Log(strUserID);

                //Debug.Log("strUserID : " + strUserID);
                //GetComponent<PacketSender>().SendLogin(strUserID, UserInfoEx.Instance.version);

                //New_NetworkManager.Instance.GetComponent<PacketSender>().SendLogin(strUserID, 0, 0);
            }
        }
        else
        {
            PlayerPrefs.DeleteAll();
            // 계정이 없으면 계정생성 UI 활성화 
            //GameManager.Instance.m_accountCreateUI.gameObject.SetActive(true);
            Debug.Log("UserID Reset : " + UserInfoEx.Instance.m_strUserID);
            //GameObject.Find("LoginUI").GetComponent<MainLogin>().popup.SetActive(true);
            //GetComponent<PacketSender>().SendMemberJoin("FanTaJoy_User", 0, 0);
        }


        if (PlayerPrefs.HasKey("Name"))
        {
            float vesion = (float.Parse(UserInfoEx.Instance.version));
            //LoginLodingPopup.instance.OpenPopup();

            PacketSender.Instance.SendMemberJoin(PlayerPrefs.GetString("Name"), 0, 0, vesion);

            //GameObject.Find("LoginUI/bg/touch").GetComponent<AlphaTrans>().GeustLoginContoll();
        }
        else
        {

#if UNITY_ANDROID
            //GameObject.Find("LoginUI").GetComponent<MainLogin>().OpenPlatformPopup();
#elif UNITY_EDITOR
            //GameObject.Find("LoginUI/touch").GetComponent<AlphaTrans>().GeustLoginContoll();
#endif
            //GameObject.Find("LoginUI").GetComponent<MainLogin>().popup.SetActive(true);
        }

    }

    void MemberJoinAns(GameNetMessage.NetMsgHeadInterface head)
    {
        GameNetMessage._MEMBERJOIN_ANS msg = (GameNetMessage._MEMBERJOIN_ANS)head;

        // 2019-02-19
        Debug.Log("ans member join");

        // 계정생성 UI 비활성화
        //GameManager.Instance.m_accountCreateUI.gameObject.SetActive(false);

        // 응답
        int Answer = msg.Answer;
        ushort UserIDLen = msg.szNickLen;

        string Packet_User_ID = System.Text.Encoding.Unicode.GetString(msg.szNick);
        Debug.Log("packet User ID : " + Packet_User_ID);

        //GameManager.Instance.m_UserID = ReadStringW(buffer, ref offset, UserIDLen, Define.MAX_NICKLEN);
        //UserInfoEx.Instance.m_strUserID.text = GameManager.Instance.m_UserID;


        //Debug.Log(GameManager.Instance.m_UserID);

        //if (GameManager.Instance.m_UserID == "") return;

        string filepath = "";

        // 계정을 폰에 저장 
        if (Application.platform == RuntimePlatform.Android)
        {
            // AingTalk : 폰 폴더생성 해서 저장함 
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
        }

        //DataSaveManager.instance.userID = Packet_User_ID;
        //DataSaveManager.instance.Save();
        /*DataController.Instance.userdata.userID = Packet_User_ID;
        DataController.Instance.SaveGameData();*/
        Debug.Log("file save ID : " + Packet_User_ID);

        Debug.Log("Member Join Ans");


        // 로그인
        //float fX = (float)NativeToolkit.GetLongitude();
        //float fY = (float)NativeToolkit.GetLatitude();

        float vesion = (float.Parse(UserInfoEx.Instance.version));
        GetComponent<PacketSender>().SendLogin(Packet_User_ID, vesion);
        //GetComponent<PacketSender>().SendLogin("0000000000100005", vesion);
    }

    void ReMemberJoinAns(GameNetMessage.NetMsgHeadInterface head)
    {
        PlayerPrefs.DeleteAll();
        Util.LogColorYellow("ReMemberJoinAns --> popup show");
        //GameObject.Find("LoginUI").GetComponent<MainLogin>().popup.SetActive(true);
    }

    void UserFileCheck(string UserID)
    {
        // 계정 검사
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
        }

        // 계정 ID가 있으면 
        //if (DataSaveManager.instance.userID == null)
        {
            return;
        }
        //else if (UserID != string.Empty)
        {
            //DataSaveManager.instance.userID = UserID;
            //DataSaveManager.instance.Save();
            /*DataController.Instance.userdata.userID = UserID;
            DataController.Instance.SaveGameData();*/

            //Debug.Log("<color=#ffff00> userID Restore </color>" + UserID);
            Util.LogColorYellow("userID Restore : " + UserID);


        }
        //else
        {
            //Debug.Log("<color=#ff0000> Invalid UserID</color>");
            Util.LogColorRed("Invalid UserID");
        }
        //GetComponent<PacketSender>().SendUserCreate(UserInfoEx.Instance.m_strUserID);
    }

    void LoginAns(GameNetMessage.NetMsgHeadInterface head)
    {
        GameNetMessage._LOGIN_ANS msg = (GameNetMessage._LOGIN_ANS)head;
        //LoginLodingPopup.instance.OpenPopup();

        string UserIdstr = System.Text.Encoding.Unicode.GetString(msg.UserID);
        string NickStr = System.Text.Encoding.Unicode.GetString(msg.UserNick);

        UserInfoEx.Instance.m_strUserID = UserIdstr;
        UserInfoEx.Instance.NickName = NickStr;

        UserFileCheck(UserIdstr);

        int gold = msg.Gold;
        int food = msg.Food;
        int crystal = msg.Crystal;

        int nomal_ticket = msg.nomal_ticket;
        int premium_ticket = msg.premium_ticket;

        int cur_world_idx = msg.cur_world_idx;
        int cur_stage_idx = msg.cur_stage_idx;
        int cur_stage_clear = msg.cur_stage_clear;

        //StageInfo.Instance.Cur_Stage_Idx = cur_stage_idx;
        //StageInfo.Instance.End_Stage_Clear = cur_stage_clear;
        Debug.Log("Cur_World_idx =" + cur_world_idx);
        Debug.Log("Cur_Stage_idx =" + cur_stage_idx);
        Debug.Log("Cur_Stage_clear =" + cur_stage_clear);

        int pvp_rank_point = msg.pvp_rank_point;

        if (msg.EventShopSlotCount > 0)
        {
            for (int i = 0; i < msg.EventShopSlotCount; i++)
            {
                UserInfoEx.Instance.EventShopList.Add(msg.EventShopInfo[i]);
            }

            //EventShopUI.instance.SetData(msg.EventShopInfo, msg.EventShopSlotCount);
        }

        int my_rank_point = msg.pvp_rank_point; //나의 랭크 포인트

        UserInfoEx.Instance.nomal_ticket = nomal_ticket;
        UserInfoEx.Instance.premium_ticket = premium_ticket;
        UserInfoEx.Instance.my_rank_point = my_rank_point;

        Debug.Log("pvp_rank_point : " + my_rank_point);
        Debug.Log("nomal_ticket : " + nomal_ticket);
        Debug.Log("premium_ticket : " + premium_ticket);

        Debug.Log("Gold : " + gold);
        Debug.Log("Food : " + food);
        Debug.Log("Crystal : " + crystal);

        UserInfoEx.Instance.gold = gold;
        UserInfoEx.Instance.food = food;
        UserInfoEx.Instance.crystal = crystal;


        // 서버 목록 요청
        PacketSender.Instance.SendServerList();
    }

    void ServerListAns(GameNetMessage.NetMsgHeadInterface head)
    {
        GameNetMessage._SERVERLIST_ANS msg = (GameNetMessage._SERVERLIST_ANS)head;


        int svr_cnt = msg.server_count;
        for (int i = 0; i < svr_cnt; ++i)
        {
            string ip = System.Text.Encoding.UTF8.GetString(msg.server_list[i].server_ip);
            int port = msg.server_list[i].port;
            Util.LogColorYellow("server_ip : " + ip + " port : " + port);
            //GameObject.Find("LoginUI").GetComponent<MainLogin>().Server_Select.transform.GetChild(i).GetComponent<ServerInfo>().ip = ip;
            //GameObject.Find("LoginUI").GetComponent<MainLogin>().Server_Select.transform.GetChild(i).GetComponent<ServerInfo>().port = port;

            switch (i)
            {
                //case 0: GameObject.Find("LoginUI").GetComponent<MainLogin>().Server_Select.GetComponent<ServerSelect>().game_server_port1 = port; break;
                //case 1: GameObject.Find("LoginUI").GetComponent<MainLogin>().Server_Select.GetComponent<ServerSelect>().game_server_port2 = port; break;
                //default: GameObject.Find("LoginUI").GetComponent<MainLogin>().Server_Select.GetComponent<ServerSelect>().game_server_port1 = port; break;
            }

        }

        {
            // 서버 선택일 경우
            //LoginLodingPopup.instance.ClosePopup();
            //GameObject.Find("LoginUI").GetComponent<MainLogin>().Server_Select.SetActive(true);
            // 서버 선택을 건너뛰고 바로 로그인

        }

        {

            GameManager.GameConnect = true;
            GameSocketManager.instance.close();

        }

        Debug.Log("ServerListAns");
    }

    void AcceptGameAns(GameNetMessage.NetMsgHeadInterface head)
    {
        Debug.Log("AcceptGameAns");


        GetComponent<PacketSender>().SendUserCreate(UserInfoEx.Instance.m_strUserID);

    }

    public void UserIdentifierAns(GameNetMessage.NetMsgHeadInterface head)
    {
        GameNetMessage._USER_IDENTIFIER_ANS msg = (GameNetMessage._USER_IDENTIFIER_ANS)head;

        Debug.Log("UserCreateAns");

        


    }

    

    

}



