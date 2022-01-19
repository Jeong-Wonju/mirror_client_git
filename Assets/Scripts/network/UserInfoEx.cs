//==========================================================
// File : UserInfo.CS
// Related Header File : 
// Original Author : Sin Hyun Seok
// Creation Date : 2019년 03월 22일
// Unity Connect : 
// Remarks 
// -  유저 정보
//==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

using EventShopInfo = FoxSDK.GameNetMessage._EVENT_SHOP_INFO;
using BattleInitData = FoxSDK.GameNetMessage._BATTLE_CONNECT_ANS;

public enum Contents_Type
{
    Mail = 1,
}

public enum Chat_Type
{
    Global = 1,
    Guild = 3,
}

public class UserInfoEx : MonoBehaviour
{
    //public Text m_strUserID;

    public string m_strUserID;
    public string NickName;
    public string PvpNickName;
    public string GuildBattleEnemyName;
    public int gold;
    public int food;
    public int crystal;
    public int nomal_ticket;
    public int premium_ticket;
    public List<int> gacha_list;
    public List<int> gacha_stack_list;
    
    public List<DragonCardInfo> dragon_card_list;
    public List<DragonInfo> dragon_list;
    public List<PvPUserInfo> pvp_list;
    public int card_count;
    public int combat_point;
    public int my_power;
    public int my_rank_point;
    public int pvp_rank_point;
    public int my_rank;
    public int tutorial_check;
    public int tutorial_step;
    public int lucket_ticket;
    public int tutorial_bitmask;

    public int PvpTime;
    public int BattleType;
    public int MonsterGroupId;
    public int DropId;
    public int DragonGroupId;
    public int DragonDropId;

    public int DragonChallengeCount;
    public int Day_count;
    public int Pvp_challenge_count;
    public int Pvp_day_count;
    public int isGuildJoined;
    public int isGuildMaster;
    public Chat_Type chat_type;
    public int guild_icn_idx;
    public int mail_count;

    public int sword_id;
    public int armors_id;
    public int helmets_id;
    public int boots_id;

    public Dictionary<Contents_Type, int> contents_onoff = new Dictionary<Contents_Type, int>();

    //public List<DragonDropInfo> dragon_drop_list;
    public string PvPUserId;
    public string GuildBattleId;
    public string my_guild_name;
    public string enemy_guild_name;
    public string version;

    public List<GambleInfo> GambleList;

    public List<EventShopInfo> EventShopList;

    public List<PvpHistoryInfo> PvpHistoryList;

    public List<AttendanceInfo> AttendanceList;

    public List<AttendanceCheckInfo> AttendanceCheckList;
                
    public List<UserEquipInfo> EquipInfoList;

    public GameObject LoginUi;

    public Dictionary<int, HeroInfo> HeroDataDictonary;

    public BattleInitData BattleData;

    static public UserInfoEx Instance;

    //전투 관련 UI 복귀 패킷
    public int SelectedStageIdx;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        chat_type = Chat_Type.Global;
    }

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        gacha_list = new List<int>();
        dragon_card_list = new List<DragonCardInfo>();
        pvp_list = new List<PvPUserInfo>();
        dragon_list = new List<DragonInfo>();
        EventShopList = new List<EventShopInfo>();
        PvpHistoryList = new List<PvpHistoryInfo>();
        GambleList = new List<GambleInfo>();
        AttendanceList = new List<AttendanceInfo>();
        AttendanceCheckList = new List<AttendanceCheckInfo>();
        EquipInfoList = new List<UserEquipInfo>();
        gacha_stack_list = new List<int>();
        HeroDataDictonary = new Dictionary<int, HeroInfo>();
        string app_version = Application.version;
        version = app_version;
        Debug.Log("Version : " + version);
    }

    public void RefreshEventShopData()
    {
        long CurrentUnixTime = Util.GetUnixTime();
        for (int i = 0; i < EventShopList.Count; i++)
        {
            if (EventShopList[i].UnixTime <= CurrentUnixTime)
            {
                EventShopList.RemoveAt(i);
                i--;
            }
        }

        //LobbyBtn.instatce.RefreshEventshopButton();
        //EventShopUI.instance.RefreshEventshopUI();
    }
    
    public void AddHeroData(int HeroID,int HeroLevel,int HeroExp)
	{
        if(HeroDataDictonary == null)
		{
            HeroDataDictonary = new Dictionary<int, HeroInfo>();
        }

        HeroInfo InputHeroData = new HeroInfo();

        InputHeroData.heroID = HeroID;
        InputHeroData.Level = HeroLevel;
        InputHeroData.Exp = HeroExp;

        HeroDataDictonary.Add(HeroID, InputHeroData);
    }

    public void UpdateHeroData(int HeroID, int HeroLevel, int HeroExp)
	{
        HeroInfo InputHeroData = HeroDataDictonary[HeroID];

        if(InputHeroData == null)
		{
            Debug.LogError("Update Hero Data Is NULL");
		}

        InputHeroData.Level = HeroLevel;
        InputHeroData.Exp = HeroExp;
    }

}

public class AttendanceCheckInfo
{
    public int step;
    public int step_check;
}

public class AttendanceInfo
{
    public int step;
    public int step_check;
    public int attendance_check;
}

public class GambleInfo
{
    public int item_type;
    public int card_id;
    public int item_amount;
    public int guid;
    public int card_stack;
}

public class PvPUserInfo
{
    public string pvp_user_name;
    public string pvp_user_Nick; //pvp 유저 닉네임
    public int rank_point; // 랭킹 점수
    public int combat_point; //전투력
    public int rank; // 랭킹
    //public string portrait; //초상화
}

public class DragonCardInfo
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
}

public class DragonInfo
{
    public int challenge_count;
    public int drop_id1;
    public int drop_id2;
    public int drop_id3;
    public int drop_id4;
    public int drop_id5;
    public int drop_id6;
    public int day_count;
}

public class PvpHistoryInfo
{
    public string user_Nick;

    public int my_score;
    public int my_rank_point;

    public int pvp_user_score;
    public int pvp_rank_point;
}

public class UserEquipInfo
{
    public int characterID;
    public int helmets;
    public int sword;
    public int armors;
    public int boots;
}

public class HeroInfo
{
    public int heroID;
    public int Level;
    public int Exp;
}

