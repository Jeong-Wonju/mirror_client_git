using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ipconfigmanager : SingletonMonoManager<ipconfigmanager>
{
    public string loginIp;
    public string ip;
    public int auth_port;
    public int game_port;
}
