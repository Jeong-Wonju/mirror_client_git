using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class packet_test : MonoBehaviour
{
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMyPlayerPacket()
    {
        Debug.Log("OnMyPlayerPacket + (NetID)" + GameObjectManager.instance.my_player.netId);
        //GameObjectManager.instance.my_player.CmdTest();
    }
}
